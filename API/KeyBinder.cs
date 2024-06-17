﻿using Mirror;
using PluginAPI.Core;

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
        /// <returns>Returns the ID of your action</returns>
        public static string RegisterKey(KeyCode key, Action action)
        {
            try
            {
                return KeyBindingManager.KeyBindingManager.instance.RegisterKey(key, action, 0);
            }
            catch(Exception e)
            {
                throw new Exception($"Failed to bind key {key}:\n {e}");
            }
        }

        public static string RegisterKey(KeyCode key, Action action, int priority)
        {
            try
            {
                return KeyBindingManager.KeyBindingManager.instance.RegisterKey(key, action, priority);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to bind key {key}:\n {e}");
            }
        }

        /// <summary>
        /// Unbind a key to an action.
        /// </summary>
        public static void UnregisterKey(KeyCode key, Action action)
        {
            try
            {
                KeyBindingManager.KeyBindingManager.instance.UnregisterKey(key, action);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to unbind key {key}:\n {e}");
            }
        }

        /// <summary>
        /// Unbind a key to an action by ID.
        /// </summary>
        public static void UnregisterKey(KeyCode key, string id)
        {
            try
            {
                KeyBindingManager.KeyBindingManager.instance.UnregisterKey(key, id);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to unbind key {key}:\n {e}");
            }
        }
    }
}