using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Player;
using KeyBindingServiceMeow.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// * V1.0.0
// - Initial release
// * V1.0.1
// - Add Debug Info

namespace KeyBindingServiceMeow
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "KeyBindingServiceMeow";
        public override string Author => "MeowServer";
        public override Version Version => new Version(1, 0, 1);

        public override PluginPriority Priority => PluginPriority.First;

        public static Plugin instance;

        public override void OnEnabled()
        {
            CheckRequirements();

            instance = this;

            Exiled.Events.Handlers.Player.Verified += EventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left += EventHandler.OnLeft;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            instance = null;

            Exiled.Events.Handlers.Player.Verified -= EventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left -= EventHandler.OnLeft;

            base.OnDisabled();
        }

        private void CheckRequirements()
        {
            if (CharacterClassManager.EnableSyncServerCmdBinding == false)
            {
                throw new Exception("This plugin requires the server to enable enable_sync_command_binding option in config_gameplay.txt.");
            }
        }
    }

    internal static class EventHandler
    {
        public static void OnVerified(VerifiedEventArgs ev)
        {
            Log.Debug("Syncing server command binding to " + ev.Player.Nickname);

            CharacterClassManager ccm = ev.Player.GameObject.GetComponent<CharacterClassManager>();
            ccm?.SyncServerCmdBinding();

            new KeyBindingManager.KeyBindingManager(ev.Player);

            KeyBinder.RegisterKey(ev.Player, UnityEngine.KeyCode.Tab, () =>
            {
                Log.Info("Tab key pressed");
            });

            Events.InvokeKeyBindReady(new KeyBindReadyEventArg(ev.Player));
        }

        public static void OnLeft(LeftEventArgs ev)
        {
            KeyBindingManager.KeyBindingManager.RemoveManager(ev.Player);
        }
    }
}
