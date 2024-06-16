using Exiled.API.Features;
using Exiled.API.Features.Core.Generic;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;

namespace KeyBindingServiceMeow.KeyBindingManager
{
    public class KeyBindingManager
    {
        public static KeyBindingManager instance = new KeyBindingManager();

        private Dictionary<KeyCode, List<Action>> KeyActionPair = new Dictionary<KeyCode, List<Action>>();

        public void RegisterKey(KeyCode key, Action action)
        {
            if (!KeyActionPair.ContainsKey(key))
                KeyActionPair.Add(key, new List<Action>());

            KeyActionPair[key].Add(action);

            if(!CmdBinding.Bindings.Any(x => x.key == key))
            {
                CmdBinding.KeyBind(key, ".CommandHandler " + key.ToString());
                SyncKeys();
            }
        }

        public void UnregisterKey(KeyCode key, Action action)
        {
            if (!KeyActionPair.ContainsKey(key))
                return;

            KeyActionPair[key].Remove(action);
        }

        internal void HandleKey(KeyCode key)
        {
            if (!KeyActionPair.ContainsKey(key))
                return;

            foreach (var action in KeyActionPair[key])
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error("An error had occured while handling the action for key: " + ex.ToString() + "\n" + ex);
                }
            }
                
        }

        private static void SyncKeys()
        {
            foreach(Player player in Player.List)
            {
                CharacterClassManager ccm = player.GameObject.GetComponent<CharacterClassManager>();
                ccm.SyncServerCmdBinding();
            }
        }
    }
}
