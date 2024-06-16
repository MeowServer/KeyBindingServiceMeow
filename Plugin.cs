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

            Exiled.Events.Handlers.Player.Verified += OnVerified;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            instance = null;

            Exiled.Events.Handlers.Player.Verified -= OnVerified;

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
