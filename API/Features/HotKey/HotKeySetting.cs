using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API.Features.HotKey
{
    /// <summary>
    /// A class represent a hot key setting. This setting is used to set a hot key's binded key to a specific key using id.
    /// </summary>
    [Serializable]
    public class HotKeySetting
    {
        public string id { get; set; }

        public KeyCode keyCode { get; set; }

        public HotKeySetting(string id, KeyCode keyCode)
        {
            this.id = id;
            this.keyCode = keyCode;
        }
    }
}
