using Exiled.API.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.KeyBindingManager
{
    public class KeyAction
    {
        public string ID = Guid.NewGuid().ToString();
        public Action action;

        public int priority;

        public KeyAction(Action action, int priority)
        {
            this.action = action;
            this.priority = priority;
        }
    }

    internal class KeyBindingManager
    {
        public static KeyBindingManager instance = new KeyBindingManager();

        private Dictionary<KeyCode, List<KeyAction>> KeyActionPair = new Dictionary<KeyCode, List<KeyAction>>();

        public string RegisterKey(KeyCode key, Action action, int priority)
        {
            if (!KeyActionPair.ContainsKey(key))
                KeyActionPair.Add(key, new List<KeyAction>());

            var keyAction = new KeyAction(action, priority);
            KeyActionPair[key].Add(keyAction);
            KeyActionPair[key].Sort((x, y) => x.priority.CompareTo(y.priority));

            if (Plugin.instance.Config.Debug)
            {
                Log.Debug("[KeyBindingManager][RegisterKey]Multiple actions are binded to the key: " + key.ToString());
                Log.Debug("Actions:");

                for (int i = 0; i < KeyActionPair[key].Count; i++)
                {
                    Log.Debug(i + ". " + KeyActionPair[key][i].action.Method.Name);
                }
            }

            if (!CmdBinding.Bindings.Any(x => x.key == key))
            {
                string command = ".CommandHandler " + key.ToString();

                Log.Debug("[KeyBindingManager][RegisterKey]Adding the key to system KeyBinding: " + key.ToString() + " binds with the command: " + command);

                CmdBinding.KeyBind(key, command);
                SyncKeys();
            }

            return keyAction.ID;
        }

        public void UnregisterKey(KeyCode key, string id)
        {
            if (!KeyActionPair.ContainsKey(key))
                return;

            KeyActionPair[key].RemoveAll(x => x.ID == id);

            if (KeyActionPair[key].Count == 0)
            {
                KeyActionPair.Remove(key);

                CmdBinding.Bindings.RemoveAll(x => x.key == key);
                CmdBinding.Save();
                return;
            }
        }

        public void UnregisterKey(KeyCode key, Action action)
        {
            if (!KeyActionPair.ContainsKey(key))
                return;

            KeyActionPair[key].RemoveAll(x => x.action == action);

            if(KeyActionPair[key].Count == 0)
            {
                KeyActionPair.Remove(key);

                CmdBinding.Bindings.RemoveAll(x => x.key == key);
                CmdBinding.Save();
                return;
            }
        }

        public void HandleKey(KeyCode key)
        {
            Log.Debug("[KeyBindingManager][HandleKey]Handling Key: " + key);

            if (!KeyActionPair.ContainsKey(key))
            {
                Log.Debug("[KeyBindingManager][HandleKey]No action binded to key: " + key);
                return;
            }

            foreach (var keyAction in KeyActionPair[key])
            {
                try
                {
                    Log.Debug("[KeyBindingManager][HandleKey]Invoking action: " + keyAction.action.Method.Name);
                    keyAction.action.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error("An error had occured while handling the action for key: " + ex.ToString() + "\n" + ex);
                }
            }   
        }

        private static void SyncKeys()
        {
            Log.Debug("[KeyBindingManager][SyncKeys]Syncing the Keys to all players.");

            foreach(Player player in Player.List)
            {
                CharacterClassManager ccm = player.GameObject.GetComponent<CharacterClassManager>();
                ccm?.SyncServerCmdBinding();
            }
        }
    }
}
