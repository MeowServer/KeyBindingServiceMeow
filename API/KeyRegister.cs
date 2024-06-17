using PluginAPI.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.API
{
    public static class KeyRegister
    {
        /// <summary>
        /// Bind a key to an action.
        /// </summary>
        /// <returns>Return false an internal error had occured</returns>
        public static bool RegisterKey(KeyCode key, Action action)
        {
            try
            {
                KeyBindingManager.KeyBindingManager.instance.RegisterKey(key, action);
            }
            catch(Exception e)
            {
                Log.Error($"Failed to bind key {key}:\n {e}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Unbind a key to an action.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns>Return false an internal error had occured</returns>
        public static bool UnregisterKey(KeyCode key, Action action)
        {
            try
            {
                KeyBindingManager.KeyBindingManager.instance.UnregisterKey(key, action);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to unbind key {key}:\n {e}");
                return false;
            }

            return true;
        }
    }
}
