using KeyBindingServiceMeow.API.Event.EventArgs;
using KeyBindingServiceMeow.KeyHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API.Features.Keys
{
    public static class KeyBinder
    {
        public delegate void KeyHandler(KeyPressedEventArg ev);

        /// <summary>
        /// Bind a key to a handler method
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="handler"></param>
        public static void BindKey(KeyCode keyCode, KeyHandler handler)
        {
            // Bind the key
            EventKeyHandler.instance.RegisterKey(keyCode, handler);
        }

        /// <summary>
        /// Bind keys to a handler method
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="handler"></param>
        public static void BindKeys(KeyCode[] keyCode, KeyHandler handler)
        {
            // Bind the keys
            EventKeyHandler.instance.RegisterKey(keyCode, handler);
        }

        /// <summary>
        /// Unbind a key from a handler method
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="handler"></param>
        public static void UnbindKey(KeyCode keyCode, KeyHandler handler)
        {
            // Unbind the key
            EventKeyHandler.instance.UnregisterKey(keyCode, handler);
        }

        /// <summary>
        /// Unbind keys from a handler method
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="handler"></param>
        public static void UnbindKeys(KeyCode[] keyCode, KeyHandler handler)
        {
            // Unbind the keys
            EventKeyHandler.instance.UnregisterKey(keyCode, handler);
        }
    }
}
