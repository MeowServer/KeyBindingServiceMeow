using Exiled.Events.Features;
using KeyBindingServiceMeow.API.Event.EventArgs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.API.Event
{
    public class Events
    {
        public delegate void KeyServiceReadyEventHandler(KeyServiceReadyEventArg ev);

        /// <summary>
        /// Called when the service is fully ready for a specific Player.
        /// </summary>
        public static event KeyServiceReadyEventHandler KeyServiceReady;

        internal static void InvokeKeyServiceReady(KeyServiceReadyEventArg ev)
        {
            KeyServiceReady?.Invoke(ev);
        }

        public delegate void KeyPressedEventHandler(KeyPressedEventArg ev);

        /// <summary>
        /// Called when a player pressed the key that is registered.
        /// </summary>
        public static event KeyPressedEventHandler KeyPressed;

        internal static void InvokeKeyPressed(KeyPressedEventArg ev)
        {
            KeyPressed?.Invoke(ev);
        }

        /// <summary>
        /// Register a key to the event. Only the keys registered to the event will passed to the event
        /// </summary>
        /// <param name="key">The key to register</param>
        public static void RegisterKeyToEvent(KeyCode key)
        {
            KeyHandlers.EventKeyHandler.instance.RegisterKey(key, InvokeKeyPressed);
        }

        /// <summary>
        /// Register keys to the event. Only the keys registered to the event will passed to the event
        /// </summary>
        /// <param name="key">The keys to register</param>
        public static void RegisterKeyToEvent(KeyCode[] key)
        {
            KeyHandlers.EventKeyHandler.instance.RegisterKey(key, InvokeKeyPressed);
        }
    }
}
