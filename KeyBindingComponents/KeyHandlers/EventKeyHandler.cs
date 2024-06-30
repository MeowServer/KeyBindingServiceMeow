using Exiled.API.Features;
using KeyBindingServiceMeow.API.Event.EventArgs;
using KeyBindingServiceMeow.API.Features.Keys;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using KeyHandler = KeyBindingServiceMeow.API.Features.Keys.KeyBinder.KeyHandler;

namespace KeyBindingServiceMeow.KeyHandlers
{
    /// <summary>
    /// This class gather all the keys needed for each plugin, and invoke events when the registered key is pressed
    /// </summary>
    internal class EventKeyHandler : IKeyHandler
    {
        /// <summary>
        /// Singleton instance of the EventKeyHandler
        /// </summary>
        public static EventKeyHandler instance;

        /// <summary>
        /// Contain all the keycode and it's corresponding event
        /// </summary>
        private Dictionary<KeyCode, KeyHandler> keyEvents = new Dictionary<KeyCode, KeyHandler>();

        internal static void Initialize()
        {
            instance = new EventKeyHandler();
        }

        internal static void Destruct()
        {
            instance = null;
        }

        /// <summary>
        /// Bind a method to a specific key
        /// </summary>
        /// <param name="keyCode">The key to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void RegisterKey(KeyCode keyCode, KeyHandler bindMethod)
        {
            BindingManager.KeyBindingManager.Subscribe(keyCode, this);

            if (keyEvents.Keys.Contains(keyCode))
            {
                if (keyEvents[keyCode].GetInvocationList().Contains(bindMethod))
                    return;

                keyEvents[keyCode] += bindMethod;
            }
            else
            {
                keyEvents.Add(keyCode, bindMethod);
            }
        }

        /// <summary>
        /// Bind a method to specific keys
        /// </summary>
        /// <param name="keys">The keys to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void RegisterKey(KeyCode[] keys, KeyHandler bindMethod)
        {
            foreach (var key in keys)
            {
                RegisterKey(key, bindMethod);
            }
        }

        /// <summary>
        /// Unnind a method to a specific key
        /// </summary>
        /// <param name="keyCode">The key to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void UnregisterKey(KeyCode keyCode, KeyHandler bindMethod)
        {
            if (!keyEvents.TryGetValue(keyCode, out KeyHandler handlers))
                return;

            handlers -= bindMethod;

            if (handlers == null || handlers.GetInvocationList().Length == 0)
            {
                keyEvents.Remove(keyCode);
                BindingManager.KeyBindingManager.Unsubscribe(keyCode, this);
            }
            else
            {
                keyEvents[keyCode] = handlers;
            }
        }

        /// <summary>
        /// Unnind a method to specific keys
        /// </summary>
        /// <param name="keys">The keys to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void UnregisterKey(KeyCode[] keys, KeyHandler bindMethod)
        {
            foreach (var key in keys)
            {
                UnregisterKey(key, bindMethod);
            }
        }

        public void OnKeyPressed(KeyPressedArg ev)
        {
            try
            {
                keyEvents[ev.keyCode]?.Invoke(new KeyPressedEventArg(ev.player, ev.keyCode));
            }
            catch
            {
                Log.Error("Failed to invoke KeyPressed event for key: " + ev.keyCode);
            }
        }
    }
}
