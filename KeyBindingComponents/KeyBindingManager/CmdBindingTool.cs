﻿using Exiled.API.Features;
using MEC;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager
{
    internal static class CmdBindingTool
    {
        private static readonly HashSet<Player> SyncingPlayer = new HashSet<Player>();

        public static void KeyBind(KeyCode keyCode)
        {
            Log.Debug("[CmdBindingTool][KeyBind] Binding The Key: " + keyCode);

            string command = ".U^ " + keyCode; 
            CmdBinding.KeyBind(keyCode, command);

            SyncBinding();
        }

        public static void KeyUnbind(KeyCode keyCode)
        {
            Log.Debug("[CmdBindingTool][KeyUnbind] Unbinding The Key: " + keyCode);

            CmdBinding.Bindings.RemoveAll(x => x.key == keyCode);

            CmdBinding.Save();
            SyncBinding();
        }


        public static void ClearKeyBinding()
        {
            Log.Debug("[CmdBindingTool][ClearKeyBinding] Clearing Key Binding");

            CmdBinding.Bindings.Clear();

            CmdBinding.Save();
            SyncBinding();
        }

        public static void SyncBinding()
        {
            Log.Debug("[CmdBindingTool][SyncKeys]Syncing The Keys Bindings To All Players.");

            foreach (Player player in Player.List)
            {
                SyncBinding(player);
            }
        }

        public static void SyncBinding(Player player)
        {
            Log.Debug("[CmdBindingTool][SyncBinding]Syncing Key Binding For Player: " + player.Nickname);

            if (SyncingPlayer.Contains(player))
                return;

            SyncingPlayer.Add(player);
            Timing.CallDelayed(0.5f, () =>
            {
                player.GameObject.GetComponent<CharacterClassManager>()?.SyncServerCmdBinding();

                //Not sure whether we need this or not but it's here just in case
                RefreshRa(player);

                SyncingPlayer.Remove(player);
            }); 
        }

        //V1.3.0 fixing
        public static void RefreshRa(Player player)
        {
            if (Config.instance.UseAutoRaSync)
            {
                Log.Debug("[CmdBindingTool][RefreshRa]Refreshing RA For Player: " + player.Nickname);

                var group = player.Group;
                player.Group = null;
                player.Group = group;
            }
        }
    }
}
