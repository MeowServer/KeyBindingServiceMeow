using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public Dictionary<KeyCode, string> CommandBinding { get; set; } = new Dictionary<KeyCode, string>()
        {
            {KeyCode.F4, ".help" }
        };

        [Description("=========================Player Verification Options=========================")]
        public bool UsePlayerVerification { get; set; } = true;
        public KeyCode SetupKey { get; set; } = KeyCode.F8;
        [Description("Use HintServiceMeow, you must install HintServiceMeow to use this feature")]
        public bool UseHintServiceMeow { get; set; } = false;
        public ushort MessageTime = 15;
        public string MessageBeforeSetup { get; set; } = "Press {SetupKey} to verify your setup! Don't worry if there's no reaction after pressing.";
        public string MessageAfterSetup { get; set; } = "Congratulations! Verification completed";
        [Description("Set Timeout to -1 if you do not want to use this feature")]
        public short TimeOut { get; set; } = 60;
        public string ScreenMessageWhenTimeOut { get; set; } = "Your verification is timeout. if you want to use key binding, please follow the instruction on console and press {SetupKey}";
        public string ConsoleMessageWhenTimeOut { get; set; } =
            "\r\n" +
            "1. Setup Instruction\r\n" +
            "2. Add -allow-syncbind in the steam launch option\r\n" +
            "3. Restart your game\r\n" +
            "4. Use the command \"synccmd\" twice in the client console until \"SyncServerCommandBinding has been enabled\" appears on the console.";

        [Description("================================Debug Options================================")]
        public bool Debug { get; set; } = false;

        [Description("Activate test cases when server start")]
        public bool UseTestCase { get; set; } = false;

        [Description("==========================================================================================")]

        public string HotKeySettingDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED\\Plugins\\HotKeyData");

        internal static Config instance { get; private set; }
        public Config()
        {
           instance = this;
        }
    }
}
