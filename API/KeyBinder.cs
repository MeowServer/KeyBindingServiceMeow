using Exiled.API.Features;
using Mirror;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.API
{
    public static class KeyBinder
    {
        /// <summary>
        /// Bind a key to an action.
        /// </summary>
        /// <param name="key">The key to bind to the action</param>
        /// <param name="action">The action to perform when player press the key</param>
        /// <returns>The ID of your action</returns>
        public static string RegisterKey(Player player, KeyCode key, Action action)
        {
            try
            {
                Log.Debug("[KeyBinder][RegisterKey]Registering key " + key.ToString() + " to action " + action.Method.Name);

                return KeyBindingManager.KeyBindingManager.Get(player).RegisterKey(key, action, 0);
            }
            catch(Exception e)
            {
                throw new Exception($"Failed to bind key {key}:\n {e}");
            }
        }

        /// <summary>
        /// Bind a key to an action.
        /// </summary>
        /// <param name="key">The key to bind to the action</param>
        /// <param name="action">The action to perform when player press the key</param>
        /// <param name="priority">The priority of your action</param>
        /// <returns>The ID of your action</returns>
        public static string RegisterKey(Player player, KeyCode key, Action action, int priority)
        {
            try
            {
                Log.Debug("[KeyBinder][RegisterKey]Registering key " + key.ToString() + " to action " + action.Method.Name + " with priority " + priority);

                return KeyBindingManager.KeyBindingManager.Get(player).RegisterKey(key, action, priority);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to bind key {key}:\n {e}");
            }
        }

        /// <summary>
        /// Unbind a key to an action.
        /// </summary>
        /// <param name="key">The key to unbind from the action</param>
        /// <param name="action">The action to unbind from</param>
        public static void UnregisterKey(Player player, KeyCode key, Action action)
        {
            try
            {
                Log.Debug("[KeyBinder][UnregisterKey]Unregistering key " + key.ToString() + " from action " + action.Method.Name);

                KeyBindingManager.KeyBindingManager.Get(player).UnregisterKey(key, action);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to unbind key {key}:\n {e}");
            }
        }

        /// <summary>
        /// Unbind a key to an action by ID.
        /// </summary>
        /// <param name="key">The key to unbind from the action</param>
        /// <param name="id">The ID of the action</param>
        public static void UnregisterKey(Player player, KeyCode key, string id)
        {
            try
            {
                Log.Debug("[KeyBinder][UnregisterKey]Unregistering key " + key.ToString() + " from action with ID " + id);

                KeyBindingManager.KeyBindingManager.Get(player).UnregisterKey(key, id);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to unbind key {key}:\n {e}");
            }
        }

        public static void RegisterKeyToEvent(KeyCode key)
        {

        }
    }
}
