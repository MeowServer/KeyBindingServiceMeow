using Exiled.API.Features;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KeyBindingServiceMeow.KeyBindingManager
{
    internal static class CmdBindingTool
    {
        public static void KeyBind(KeyCode keyCode)
        {
            string command = ".U^ " + keyCode.ToString();
            CmdBinding.KeyBind(keyCode, command);

            SyncKeys();
        }

        public static void KeyUnbind(KeyCode keyCode)
        {
            CmdBinding.Bindings.RemoveAll(x => x.key == keyCode);
            CmdBinding.Save();

            SyncKeys();
        }

        public static void SyncKeys()
        {
            Log.Debug("[KeyBindingManager][SyncKeys]Syncing the Keys to all players.");

            foreach (Player player in Player.List)
            {
                CharacterClassManager ccm = player.GameObject.GetComponent<CharacterClassManager>();
                ccm?.SyncServerCmdBinding();

                //Not sure whether we need this or not but it's here just in case
                RefreshRA(player);
            }
        }

        //V1.3.0 fixing
        public static void RefreshRA(Player player)
        {
            Log.Debug("[KeyBindingManager][RefreshRA]RegreshingRA for player: " + player.Nickname);

            var group = player.Group;
            player.Group = null;
            player.Group = group;
        }

        public static void SyncBinding(Player player)
        {
            player.GameObject.GetComponent<CharacterClassManager>()?.SyncServerCmdBinding();
        }
    }
}
