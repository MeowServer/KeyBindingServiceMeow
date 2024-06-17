using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API
{
    public class Events
    {
        public delegate void KeyBindReady(KeyBindReadyEventArg ev);

        /// <summary>
        /// Called when the system is ready to bind key for a specific Player.
        /// </summary>
        public static event KeyBindReady OnKeyBindReady;

        internal static void InvokeKeyBindReady(KeyBindReadyEventArg ev)
        {
            OnKeyBindReady?.Invoke(ev);
        }

        public delegate void KeyPressed(KeyPressedEventArg ev);

        /// <summary>
        /// Called when a player pressed the key that is registered in KeyBinder.
        /// </summary>
        public static event KeyPressed OnKeyPressed;

        internal static void InvokeKeyPressed(KeyPressedEventArg ev)
        {
            OnKeyPressed?.Invoke(ev);
        }
    }
}
