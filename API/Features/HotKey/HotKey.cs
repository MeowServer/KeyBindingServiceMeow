using Exiled.API.Features;
using KeyBindingServiceMeow.API.Event.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API.Features.HotKey
{
    /// <summary>
    /// Representing a hot key. This class is used to create a hotkey that can be customized to a specific key and invoke an event when the key is pressed.
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// The id that indicate thie hotkey
        /// This ID must be unique and should not be change sicne it used for saving the key customization.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// The name of this HotKey, For example: "Open Menu", "SCP Ability" etc.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The description of this HotKey, For example: "Open the menu", "Use SCP's Ability" etc.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The category of this HotKey, For example: "Menu", "SCP-XXX", "GameTool" etc.
        /// </summary>
        public string category { get; set; }

        public delegate void KeyPressedEventHanler(HotKeyPressedEventArg ev);
        /// <summary>
        /// Invoke when the hotkey was being pressed
        /// </summary>
        public event KeyPressedEventHanler KeyPressed;

        /// <summary>
        /// Default key assigned to this hotkey
        /// </summary>
        public KeyCode defaultKey { get; }

        /// <summary>
        /// Current key assigned to this hotkey
        /// </summary>
        public KeyCode currentKey { get; set; }

        public HotKey(KeyCode defaultKey, string id, string name, string description = "", string category = "")
        {
            //info
            this.id = id;
            this.name = name;
            this.description = description;
            this.category = category;
            //Key Binding
            this.defaultKey = defaultKey;
            this.currentKey = defaultKey;
        }

        public HotKey(HotKey hotKey)
        {
            //info
            this.id = hotKey.id;
            this.name = hotKey.name;
            this.description = hotKey.description;
            this.category = hotKey.category;
            //Key Binding
            this.defaultKey = hotKey.defaultKey;
            this.currentKey = hotKey.currentKey;
            //Event
            this.KeyPressed = hotKey.KeyPressed;
        }

        internal void OnPressed(HotKeyPressedEventArg ev)
        {
            Log.Debug("Hot Key " + id + " Invoked for " + ev.player.Nickname + " with key " + ev.hotkey.currentKey);
            KeyPressed?.Invoke(ev);
        }
    }

    public class HotKeyPressedEventArg
    {
        /// <summary>
        /// The hotkey that was being pressed
        /// </summary>
        public HotKey hotkey;

        /// <summary>
        /// The player who pressed the hotkey
        /// </summary>
        public Player player;

        public HotKeyPressedEventArg(HotKey hotkey, Player player)
        {
            this.hotkey = hotkey;
            this.player = player;
        }
    }
}
