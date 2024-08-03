using Exiled.API.Features;
using UnityEngine;
using KeyBindingServiceMeow.API.Event.EventArgs;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeyHandler = KeyBindingServiceMeow.API.Features.Keys.KeyBinder.KeyHandler;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyHandlers
{
    /// <summary>
    /// This class gather all the keys needed for each plugin, and invoke events when the registered key is pressed
    /// </summary>
    internal class EventKeyHandler : IKeyHandler
    {
        /// <summary>
        /// Singleton Instance of the EventKeyHandler
        /// </summary>
        public static EventKeyHandler Instance;

        /// <summary>
        /// Contain all the keycode and it's corresponding event
        /// </summary>
        private readonly Dictionary<KeyCode, KeyHandler> _keyHandlers = new Dictionary<KeyCode, KeyHandler>();

        internal static void Initialize()
        {
            Instance = new EventKeyHandler();
        }

        internal static void Destruct()
        {
            Instance = null;
        }

        /// <summary>
        /// Bind a method to a specific key
        /// </summary>
        /// <param name="keyCode">The key to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void RegisterKey(KeyCode keyCode, KeyHandler bindMethod)
        {
            KeyBindingManager.KeyBindingManager.Subscribe(keyCode, this);

            if (_keyHandlers.Keys.Contains(keyCode))
            {
                if (_keyHandlers[keyCode].GetInvocationList().Contains(bindMethod))
                    return;

                _keyHandlers[keyCode] += bindMethod;
            }
            else
            {
                _keyHandlers.Add(keyCode, bindMethod);
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
        /// Unbind a method to a specific key
        /// </summary>
        /// <param name="keyCode">The key to use</param>
        /// <param name="bindMethod">The method to use</param>
        public void UnregisterKey(KeyCode keyCode, KeyHandler bindMethod)
        {
            if (!_keyHandlers.TryGetValue(keyCode, out KeyHandler handlers))
                return;

            handlers -= bindMethod;

            if (handlers == null || handlers.GetInvocationList().Length == 0)
            {
                _keyHandlers.Remove(keyCode);
                KeyBindingManager.KeyBindingManager.Unsubscribe(keyCode, this);
            }
            else
            {
                _keyHandlers[keyCode] = handlers;
            }
        }

        /// <summary>
        /// Unbind a method to specific keys
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
                _keyHandlers[ev.KeyCode]?.Invoke(new KeyPressedEventArg(ev.Player, ev.KeyCode));
            }
            catch(Exception ex)
            {
                Log.Error("Failed to invoke KeyPressed event for key: " + ev.KeyCode);
                Log.Error(ex);
            }
        }
    }
}
