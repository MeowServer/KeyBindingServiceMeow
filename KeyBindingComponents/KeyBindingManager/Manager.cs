using Exiled.API.Features;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.BindingManager
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

            if (keyHandlers[keyCode].Count == 0)
            {
                keyHandlers.Remove(keyCode);
                CmdBindingTool.KeyUnbind(keyCode);
            }    
        }

        public static void Notify(Player player, KeyCode keyCode)
        {
            if (keyHandlers.ContainsKey(keyCode))
            {
                foreach (var handler in keyHandlers[keyCode])
                {
                    var arg = new KeyPressedArg(player, keyCode);
                    handler.OnKeyPressed(arg);
                }
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
