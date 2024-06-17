using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Warns if multiple actions are binded to the same key.")]
        public bool WarnOnMultipleAction { get; set; } = true;

        //[Description("Verify whether the player had correctly set their client for this service.")]
        //public bool VerifyPlayer { get; set; } = false;
    }
}
