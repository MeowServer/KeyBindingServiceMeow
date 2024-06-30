using Exiled.API.Features;
using Exiled.Loader;
using KeyBindingServiceMeow.KeyHandlers;
using KeyBindingServiceMeow.KeyApplications.HotKeys.Setting;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;
using KeyBindingServiceMeow.API.Features;
using KeyBindingServiceMeow.API.Features.HotKey;
using Utils.NonAllocLINQ;
using KeyBindingServiceMeow.API.Event.EventArgs;
using static UnityStandardAssets.CinematicEffects.Bloom;

namespace KeyBindingServiceMeow.KeyApplications.HotKeys
{
    /// <summary>
    /// This class manage all the hotkeys for a player.
    /// </summary>
    internal class HotKeyManager
    {
        private static List<HotKeyManager> hotKeyManagers = new List<HotKeyManager>();

        private Player player;

        private List<HotKey> hotKeys = new List<HotKey>();

        public HotKeyManager(Player player)
        {
            this.player = player;

            hotKeyManagers.Add(this);
        }

        public static void Create(Player player)
        {
            if (hotKeyManagers.Any(x => x.player == player))
                throw new Exception("A hot key manager is already created for " + player.UserId);

            new HotKeyManager(player);

            Log.Debug("HotKeyManager Created for " + player.UserId);
        }

        public static void Destruct(Player player)
        {
            HotKeyManager instance = Get(player);

            instance?.UnregisterAllKeys();

            hotKeyManagers.Remove(instance);

            Log.Debug("HotKeyManager Destructed for " + player.UserId);
        }

        public static HotKeyManager Get(Player player)
        {
             return hotKeyManagers.Find(x => x.player == player);
        }

        //Add Methods
        public void RegisterKey(HotKey hotKey)
        {
            if (hotKeys.Any(x => x.id == hotKey.id))
                throw new Exception("A hot key with same id is already registered. Key id: " + hotKey.id);

            hotKeys.Add(new HotKey(hotKey));

            UpdateKeySetting(hotKey);

            EventKeyHandler.instance.RegisterKey(hotKey.currentKey, OnKeyPressed);
        }

        //Remove Methods
        public void UnregisterKey(HotKey hotKey)
        {
            hotKeys.RemoveAll(x => x.id == hotKey.id);

            if (!hotKeys.Any(x => x.currentKey == hotKey.currentKey))
                EventKeyHandler.instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);
        }

        public void UnregisterKey(string id)
        {
            hotKeys.RemoveAll(x => x.id == id);
        }

        //Update Methods
        public void SetKey(HotKeySetting setting)
        {
            SettingManager.instance.ChangeSettings(player.UserId, setting);

            UpdateKeySetting(setting.id);
        }

        public void SetKey(string id, KeyCode newKey)
        {
            SetKey(new HotKeySetting(id, newKey));
        }

        public void ResetKey(HotKey hotKey)
        {
            ResetKey(hotKey.id);
        }

        public void ResetKey(string id)
        {
            if(!TryGetKey(id, out HotKey hotKey))
            {
                return;
            }

            SetKey(id, hotKey.defaultKey);
        }

        public void ResetAllKeys()
        {
            foreach (var hotKey in hotKeys)
            {
                ResetKey(hotKey);
            }
        }

        //Search Methods
        public IReadOnlyList<HotKey> GetKeys()
        {
            return hotKeys.AsReadOnly();
        }

        public bool HasKey(string id)
        {
            return GetKeys().Any(x => x.id == id);
        }

        public HotKey GetKey(string id)
        {
            return hotKeys.Find(x => x.id == id);
        }

        public bool TryGetKey(string id, out HotKey hotKey)
        {
            return hotKeys.TryGetFirst(x => x.id == id, out hotKey);
        }

        //Internal Setting Update Methods
        private void UpdateAllKeySetting()
        {
            foreach(var hotKey in hotKeys)
            {
                UpdateKeySetting(hotKey);
            }
        }

        private void UpdateKeySetting(HotKey hotKey)
        {
            UpdateKeySetting(hotKey.id);
        }

        private void UpdateKeySetting(string id)
        {
            var hotKey = hotKeys.Find(x => x.id == id);

            var setting = SettingManager.instance.GetSettings(player.UserId)
                .FirstOrDefault(x => x.id == id);

            if (hotKey == null || setting == null)
                return;

            if (hotKey.currentKey != setting.keyCode)
            {
                KeyCode oldKey = hotKey.currentKey;
                KeyCode newKey = setting.keyCode;

                hotKey.currentKey = newKey;

                if (!hotKeys.Any(x => x.currentKey == oldKey))
                    EventKeyHandler.instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);

                EventKeyHandler.instance.RegisterKey(hotKey.currentKey, OnKeyPressed);
            }
        }

        //Internal Unregister Methods
        private void UnregisterAllKeys()
        {
            foreach (var hotKey in hotKeys)
            {
                EventKeyHandler.instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);
            }

            hotKeys.Clear();
        }

        //Listener interface implementation
        internal void OnKeyPressed(KeyPressedEventArg ev)
        {
            if (!hotKeys.Any(x => x.currentKey == ev.Key))
                return;

            foreach (var hotKey in hotKeys)
            {
                if (hotKey.currentKey == ev.Key)
                {
                    hotKey.OnPressed(new HotKeyPressedEventArg(hotKey, ev.Player));
                }
            }
        }

    }
}
