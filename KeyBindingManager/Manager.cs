using Exiled.API.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using YamlDotNet.Core.Tokens;

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
        public Player Player { get; set; }

        public static List<KeyBindingManager> List = new List<KeyBindingManager>();

        private Dictionary<KeyCode, List<KeyAction>> KeyActionPair = new Dictionary<KeyCode, List<KeyAction>>();

        public KeyBindingManager(Player player)
        {
            List.Add(this);
            Player = player;
        }

        public static void RemoveManager(Player player)
        {
            List.RemoveAll(x => x.Player == player);
        }

        public static KeyBindingManager Get(Player player)
        {
            return List.Find(x => x.Player == player);
        }

        public string RegisterKey(KeyCode key, Action action, int priority)
        {
            //Initialize the key if it's not in the dictionary
            if (!KeyActionPair.ContainsKey(key))
                KeyActionPair.Add(key, new List<KeyAction>());

            //Add and sort the key action
            var keyAction = new KeyAction(action, priority);
            KeyActionPair[key].Add(keyAction);
            KeyActionPair[key].Sort((x, y) => y.priority.CompareTo(x.priority));

            //Debug info
            if (Plugin.instance.Config.Debug)
            {
                Log.Debug("[KeyBindingManager][RegisterKey]Multiple actions are binded to the key: " + key.ToString());
                Log.Debug("Actions:");

                for (int i = 0; i < KeyActionPair[key].Count; i++)
                {
                    Log.Debug(i + ". " + KeyActionPair[key][i].action.Method.Name);
                }
            }

            //Add the key to the system KeyBinding if it's not already added
            if (!CmdBinding.Bindings.Any(x => x.key == key))
            {
                string command = ".CommandHandler " + key.ToString();

                Log.Debug("[KeyBindingManager][RegisterKey]Adding the key to system KeyBinding: " + key.ToString() + " binds with the command: " + command);

                CmdBinding.KeyBind(key, command);
                CMDBindingTool.SyncKeys();
            }

            return keyAction.ID;
        }

        public void UnregisterKey(KeyCode key, string id)
        {
            //Check if the key is in the dictionary
            if (!KeyActionPair.ContainsKey(key))
                return;

            //Remove the action by ID
            KeyActionPair[key].RemoveAll(x => x.ID == id);

            //If there's no action binded to the key, remove the key from the dictionary and the system KeyBinding
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
            //Check if the key is in the dictionary
            if (!KeyActionPair.ContainsKey(key))
                return;

            //Remove the action
            KeyActionPair[key].RemoveAll(x => x.action == action);

            //If there's no action binded to the key, remove the key from the dictionary and the system KeyBinding
            if(KeyActionPair[key].Count == 0)
            {
                KeyActionPair.Remove(key);

                CmdBinding.Bindings.RemoveAll(x => x.key == key);
                CmdBinding.Save();
                return;
            }
        }

        //Perform the actions that binded to the key
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

        
    }

    internal static class EventKeysManager
    {
        private static List<KeyCode> BindedKeys = new List<KeyCode>();

        public static void AddKey(KeyCode key)
        {
            if (!BindedKeys.Contains(key))
                BindedKeys.Add(key);

            //Add the key to the system KeyBinding if it's not already added
            if (!CmdBinding.Bindings.Any(x => x.key == key))
            {
                string command = ".CommandHandler " + key.ToString();

                Log.Debug("[KeyBindingManager][RegisterKey]Adding the key to system KeyBinding: " + key.ToString() + " binds with the command: " + command);

                CmdBinding.KeyBind(key, command);
                CMDBindingTool.SyncKeys();
            }
        }

        public static void AddKey(KeyCode[] keys)
        {
            foreach (var key in keys)
            {
                AddKey(key);
            }
        }

        public static bool IsBinded(KeyCode key)
        {
            if (BindedKeys.Contains(key))
                return true;

            return false;
        }
    }

    internal static class CMDBindingTool
    {
        public static void SyncKeys()
        {
            Log.Debug("[KeyBindingManager][SyncKeys]Syncing the Keys to all players.");

            foreach (Player player in Player.List)
            {
                CharacterClassManager ccm = player.GameObject.GetComponent<CharacterClassManager>();
                ccm?.SyncServerCmdBinding();
            }
        }
    }
}
