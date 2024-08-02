using Exiled.API.Features;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager
{

    /// <summary>
    /// This class manages the key binding. Delete the keys that was not used by any component and registered the keys that was used by the component
    /// </summary>
    internal static class KeyBindingManager
    {
        private static readonly Dictionary<KeyCode, List<IKeyHandler>> keyHandlers = new Dictionary<KeyCode, List<IKeyHandler>>();

        public static void Subscribe(KeyCode keyCode, IKeyHandler listener)
        {
            if (!keyHandlers.ContainsKey(keyCode))
            {
                keyHandlers.Add(keyCode, new List<IKeyHandler>());
                CmdBindingTool.KeyBind(keyCode);
            }

            if(!keyHandlers[keyCode].Contains(listener))
                keyHandlers[keyCode].Add(listener);
        }

        public static void Unsubscribe(KeyCode keyCode, IKeyHandler listener)
        {
            keyHandlers[keyCode].Remove(listener);

            if (keyHandlers[keyCode].Count != 0)
                return;
            
            keyHandlers.Remove(keyCode);
            CmdBindingTool.KeyUnbind(keyCode);
        }

        public static void Notify(Player player, KeyCode keyCode)
        {
            if (!keyHandlers.TryGetValue(keyCode, out var handlerList))
                return;

            foreach (var handler in new List<IKeyHandler>(handlerList))
            {
                var arg = new KeyPressedArg(player, keyCode);
                handler.OnKeyPressed(arg);
            }
        }

        public static bool IsKeySubscribed(KeyCode keyCode, IKeyHandler listener)
        {
            return keyHandlers.ContainsKey(keyCode) && keyHandlers[keyCode].Contains(listener);
        }

        public static bool IsKeySubscribed(KeyCode keyCode)
        {
            return keyHandlers.ContainsKey(keyCode) && keyHandlers[keyCode].Count > 0;
        }
    }
}
