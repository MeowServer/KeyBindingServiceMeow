using Exiled.API.Features;
using KeyBindingServiceMeow.KeyBindingComponents.KeyHandlers;
using KeyBindingServiceMeow.KeyApplications.HotKeys.Setting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using KeyBindingServiceMeow.API.Features.HotKey;
using Utils.NonAllocLINQ;
using KeyBindingServiceMeow.API.Event.EventArgs;

namespace KeyBindingServiceMeow.KeyApplications.HotKeys
{
    /// <summary>
    /// This class manage all the hotkeys for a _player.
    /// </summary>
    public class HotKeyManager
    {
        private static readonly List<HotKeyManager> ManagerList = new List<HotKeyManager>();

        private readonly Player _player;

        private readonly List<HotKey> _hotKeys = new List<HotKey>();

        public HotKeyManager(Player player)
        {
            this._player = player;

            ManagerList.Add(this);
        }

        public static void Create(Player player)
        {
            if (ManagerList.Any(x => x._player == player))
                throw new Exception("A hot key manager is already created for " + player.UserId);

            new HotKeyManager(player);

            Log.Debug("HotKeyManager Created for " + player.UserId);
        }

        public static void Destruct(Player player)
        {
            HotKeyManager instance = Get(player);

            instance?.UnregisterAllKeys();

            ManagerList.Remove(instance);

            Log.Debug("HotKeyManager Destructed for " + player.UserId);
        }

        public static HotKeyManager Get(Player player)
        {
             return ManagerList.Find(x => x._player == player);
        }

        //Add Methods
        public void RegisterKey(HotKey hotKey)
        {
            if (_hotKeys.Any(x => x.id == hotKey.id))
                throw new Exception("A hot key with same id is already registered. Key id: " + hotKey.id);

            _hotKeys.Add(new HotKey(hotKey));

            UpdateKeySetting(hotKey);

            EventKeyHandler.Instance.RegisterKey(hotKey.currentKey, OnKeyPressed);
        }

        //Remove Methods
        public void UnregisterKey(HotKey hotKey)
        {
            _hotKeys.RemoveAll(x => x.id == hotKey.id);

            if (!_hotKeys.Any(x => x.currentKey == hotKey.currentKey))
                EventKeyHandler.Instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);
        }

        public void UnregisterKey(string id)
        {
            _hotKeys.RemoveAll(x => x.id == id);
        }

        //Update Methods
        public void SetKey(HotKeySetting setting)
        {
            SettingManager.Instance.ChangeSettings(_player.UserId, setting);

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
            foreach (var hotKey in _hotKeys)
            {
                ResetKey(hotKey);
            }
        }

        //Search Methods
        public IReadOnlyList<HotKey> GetKeys()
        {
            return _hotKeys.AsReadOnly();
        }

        public bool HasKey(string id)
        {
            return GetKeys().Any(x => x.id == id);
        }

        public HotKey GetKey(string id)
        {
            return _hotKeys.Find(x => x.id == id);
        }

        public bool TryGetKey(string id, out HotKey hotKey)
        {
            return _hotKeys.TryGetFirst(x => x.id == id, out hotKey);
        }

        //Internal Setting Update Methods
        private void UpdateAllKeySetting()
        {
            foreach(var hotKey in _hotKeys)
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
            var hotKey = _hotKeys.Find(x => x.id == id);

            var setting = SettingManager.Instance.GetSettings(_player.UserId)
                .FirstOrDefault(x => x.id == id);

            if (hotKey == null || setting == null)
                return;

            if (hotKey.currentKey != setting.keyCode)
            {
                KeyCode oldKey = hotKey.currentKey;
                KeyCode newKey = setting.keyCode;

                hotKey.currentKey = newKey;

                if (!_hotKeys.Any(x => x.currentKey == oldKey))
                    EventKeyHandler.Instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);

                EventKeyHandler.Instance.RegisterKey(hotKey.currentKey, OnKeyPressed);
            }
        }

        //Internal Unregister Methods
        private void UnregisterAllKeys()
        {
            foreach (var hotKey in _hotKeys)
            {
                EventKeyHandler.Instance.UnregisterKey(hotKey.currentKey, OnKeyPressed);
            }

            _hotKeys.Clear();
        }

        //Listener interface implementation
        internal void OnKeyPressed(KeyPressedEventArg ev)
        {
            if (!_hotKeys.Any(x => x.currentKey == ev.Key))
                return;

            foreach (var hotKey in _hotKeys)
            {
                if (hotKey.currentKey == ev.Key)
                {
                    hotKey.OnPressed(new HotKeyPressedEventArg(hotKey, ev.Player));
                }
            }
        }

    }
}
