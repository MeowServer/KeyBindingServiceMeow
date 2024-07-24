using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using KeyBindingServiceMeow.API.Event.EventArgs;
using KeyBindingServiceMeow.KeyApplications.HotKeys;
using KeyBindingServiceMeow.KeyApplications.HotKeys.Setting;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;
using KeyBindingServiceMeow.TestCase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KeyBindingServiceMeow.KeyBindingComponents.KeyHandlers;
using KeyBindingServiceMeow.KeyApplications.PlayerVerification;

// * V1.0.0
// - Initial release
// * V1.0.1
// - Add Debug Info
// * V1.1.0
// - Bind keys to the specific Player rather than bind keys globally.
// * V1.2.0
// - Add KeyBindReady event
// * V1.3.0
// - Fix the issue that player has to press RA key to use key binding. Thanks for the idea from Ruemena
// * V2.0.0
// - Remake everything......
// * V2.1.0
// - Add Player Verification that allows you to detect whether a Player has been verified.
// - Add Command Binding that allows you to bind a custom command to a key
// * V2.1.1
// - Fix the issue that the plugin does not work properly when HintServieMeow is not installed
// * V2.1.2
// - Standardize code style
//
//                                           Simple Direction(V2.1.1)
//========================================================================================================
//    API: API for other plugins to use
//
//    KeyBindingComponents: Basic Components that directly manage, detect and handle the cmd binding
//        KeyManager: Subject components, manager the keys in CmdBinding and notify the observers when key was pressed
//        KeyHandler: Concrete Observer, Handle the key
//
//    KeyManager: Advanced Components based on KeyBindingComponents to provide more advanced features.
//        PlayerVerification: Manage the verification of a Player. Verify whether a Player had correctly setup their client.
//        HotKeyManager: Manage the hotkeys for a Player. Hotkeys are customizable keys with name, description and other information.

namespace KeyBindingServiceMeow
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "KeyBindingServiceMeow";
        public override string Author => "MeowServer";
        public override Version Version => new Version(2, 1, 2);
        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        public override PluginPriority Priority => PluginPriority.First;

        public static Plugin instance;

        public override void OnEnabled()
        {
            CheckRequirements();

            instance = this;

            Exiled.Events.Handlers.Player.Verified += EventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left += EventHandler.OnLeft;

            //Initialize components
            CmdBindingTool.ClearKeyBinding();

            CommandConvertKeyHandler.Initialize(); //Key Handlers
            EventKeyHandler.Initialize();
            SettingManager.Initialize(); //Hotkey manager component

            if (Config.instance.UseTestCase)
            {
                HotKeyTestCase.OnEnabled();
                EventKeyTestCase.OnEnabled();
                VerificationTestCase.OnEnabled();
            }   

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            instance = null;

            Exiled.Events.Handlers.Player.Verified -= EventHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left -= EventHandler.OnLeft;

            CommandConvertKeyHandler.Destruct(); //Key Handlers
            EventKeyHandler.Destruct();
            SettingManager.Destruct(); //Hotkey manager component

            HotKeyTestCase.OnDisabled();
            EventKeyTestCase.OnDisabled();
            VerificationTestCase.OnDisabled();

            base.OnDisabled();
        }

        private static void CheckRequirements()
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
            HotKeyManager.Create(ev.Player);
            Verifier.Create(ev.Player);

            API.Event.Events.InvokeKeyServiceReady(new KeyServiceReadyEventArg(ev.Player));
            
            //Sync and activate CMD binding on client side
            CmdBindingTool.SyncBinding(ev.Player);
        }

        public static void OnLeft(LeftEventArgs ev)
        {
            HotKeyManager.Destruct(ev.Player);
            Verifier.Destruct(ev.Player);
        }
    }
}
