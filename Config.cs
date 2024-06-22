using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow
{
    public class Config : IConfig
    {
        public static Config instance { get; private set;}

        public bool IsEnabled { get; set; } = true;

        [Description("================Debut Options================")]
        public bool Debug { get; set; } = false;

        [Description("Activate test cases when server start")]
        public bool UseTestCase { get; set; } = false;

        [Description("=============================================")]

        //[Description("Verify whether the player had correctly set their client for this service.")]
        //public bool VerifyPlayer { get; set; } = false;

        public string HotKeySettingDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EXILED\\Plugins\\HotKeyData");

        public Config()
        {
           instance = this;
        }
    }
}
