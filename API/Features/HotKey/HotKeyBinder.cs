using Exiled.API.Features;
using KeyBindingServiceMeow.KeyApplications.HotKeys;
using KeyBindingServiceMeow.BindingManager;
using KeyBindingServiceMeow.KeyHandlers;
using Mirror;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.API.Features.HotKey
{
    public static class HotKeyBinder
    {
        /// <summary>
        /// Add a hot key to the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hotKey"></param>
        public static void RegisterKey(Player player, HotKey hotKey)
        {
            HotKeyManager.Get(player).RegisterKey(hotKey);
        }

        /// <summary>
        /// Add hot keys to the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hotKeys"></param>
        public static void RegisterKey(Player player, HotKey[] hotKeys)
        {
            foreach (HotKey hotKey in hotKeys)
            {
                HotKeyManager.Get(player).RegisterKey(hotKey);
            }
        }

        /// <summary>
        /// Remove a hot key from a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hotKey"></param>
        public static void UnregisterKey(Player player, HotKey hotKey)
        {
            HotKeyManager.Get(player).UnregisterKey(hotKey);
        }

        /// <summary>
        /// Remove hot keys from a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hotKeys"></param>
        public static void UnregisterKey(Player player, HotKey[] hotKeys)
        {
            var manager = HotKeyManager.Get(player);

            foreach (HotKey hotKey in hotKeys)
            {
                manager.UnregisterKey(hotKey);
            }
        }

        /// <summary>
        /// Set a player's hot key to a specific setting
        /// </summary>
        /// <param name="player"></param>
        /// <param name="setting"></param>
        public static void SetHotkey(Player player, HotKeySetting setting)
        {
            HotKeyManager.Get(player).SetKey(setting);
        }

        /// <summary>
        /// Set a player's hot keys to specific settings
        /// </summary>
        /// <param name="player"></param>
        /// <param name="settings"></param>
        public static void SetHotkey(Player player, HotKeySetting[] settings)
        {
            var manager = HotKeyManager.Get(player);
            foreach(HotKeySetting setting in settings)
            {
                manager.SetKey(setting);
            }
        }
    }
}
