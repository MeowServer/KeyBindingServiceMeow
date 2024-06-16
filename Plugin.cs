using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using KeyBindingServiceMeow.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CmdBinding;

namespace KeyBindingServiceMeow
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "KeyBindingServiceMeow";
        public override string Author => "MeowServerOwner";
        public override Version Version => new Version(1, 0, 0);

        public override PluginPriority Priority => PluginPriority.First;

        public static Plugin instance;

        public override void OnEnabled()
        {
            instance = this;

            KeyRegister.RegisterKey(UnityEngine.KeyCode.B, () =>
            {
                Log.Info("恭喜成功了！！！！！！！按下了B键");
            });

            Log.Info(CmdBinding.Bindings.Count.ToString());
            foreach(var binding in CmdBinding.Bindings)
            {
                Log.Info(binding.key.ToString());
                Log.Info(binding.command);
                Log.Info(binding.command.StartsWith(".") || binding.command.StartsWith("/"));
            }

            Exiled.Events.Handlers.Player.Verified += OnVerified;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            instance = null;

            base.OnDisabled();
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            CharacterClassManager ccm = ev.Player.GameObject.GetComponent<CharacterClassManager>();
            ccm.SyncServerCmdBinding();
        }
    }

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
