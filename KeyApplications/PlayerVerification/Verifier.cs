using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using KeyBindingServiceMeow.API.Event.EventArgs;
using KeyBindingServiceMeow.API.Features.HotKey;
using KeyBindingServiceMeow.KeyApplications.HotKeys;
using KeyBindingServiceMeow.KeyApplications.PlayerVerification.Broadcaster;
using KeyBindingServiceMeow.KeyBindingComponents.KeyHandlers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.KeyApplications.PlayerVerification
{
    //Not implemented yet
    internal class Verifier
    {
        private static readonly List<Verifier> SetupHelpers = new List<Verifier>();

        private readonly Player _player;
        private bool _isSetup = false;
        private readonly DateTime _timeCreated;

        private KeyCode SetupKey => Config.instance.SetupKey;

        private readonly IBroadcaster _broadcaster;

        public Verifier(Player player)
        {
            this._player = player;
            this._timeCreated = DateTime.Now;

            if (Config.instance.UseHintServiceMeow)
                _broadcaster = new HintServiceBroadcaster();
            else
                _broadcaster = new InternalBroadcaster();

            EventKeyHandler.Instance.RegisterKey(SetupKey, Verify);
            BroadCast(Config.instance.MessageBeforeSetup);

            if(Config.instance.TimeOut == -1)
                Timing.CallDelayed((float)Config.instance.TimeOut + 0.05f, CheckTimeOut);

            SetupHelpers.Add(this);
        }

        public static void Create(Player player)
        {
            if (!Config.instance.UsePlayerVerification)
                return;

            if (SetupHelpers.Any(x => x._player == player))
                throw new Exception("A setup helper is already created for " + player.UserId);

            new Verifier(player);

            Log.Debug("SetupHelper Created for " + player.UserId);
        }

        public static void Destruct(Player player)
        {
            Verifier instance = Get(player);

            SetupHelpers.Remove(instance);

            Log.Debug("SetupHelper Destructed for " + player.UserId);
        }

        public static bool IsSetup(Player player)
        {
            return Get(player)._isSetup;
        }

        public static bool IsTimeout(Player player)
        {
            var helper = Get(player);

            if (helper._isSetup)
                return false;

            return Get(player)._timeCreated + TimeSpan.FromSeconds(Config.instance.TimeOut) > DateTime.Now;
        }

        public static Verifier Get(Player player)
        {
            return SetupHelpers.Find(x => x._player == player);
        }

        public void Verify(KeyPressedEventArg ev)
        {
            EventKeyHandler.Instance.UnregisterKey(SetupKey, Verify);

            this._isSetup = true;
            BroadCast(Config.instance.MessageAfterSetup);

            API.Event.Events.InvokeServiceVerified(new ServiceVerifiedEventArg(this._player));
        }

        private void CheckTimeOut()
        {
            if (_isSetup)
                return;

            BroadCast(Config.instance.ScreenMessageWhenTimeOut);
            _player.SendConsoleMessage(Config.instance.ConsoleMessageWhenTimeOut, "green");

            API.Event.Events.InvokeServiceVerificationTimeout(new ServiceVerificationTimeoutEventArg(this._player));
        }

        private void BroadCast(string message)
        {
            string str = message
                .Replace("{SetupKey}", SetupKey.ToString());
            
            _broadcaster.Broadcast(str, _player);
        }
    }
}
