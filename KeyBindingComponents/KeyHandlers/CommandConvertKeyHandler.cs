using CommandSystem;
using Exiled.API.Features;
using HarmonyLib;
using Hints;
using KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager;
using PluginAPI.Commands;
using PluginAPI.Events;
using RemoteAdmin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyHandlers
{
    internal class CommandConvertKeyHandler : IKeyHandler
    {
        private static CommandConvertKeyHandler _instance;

        private CommandConvertKeyHandler()
        {
            foreach(KeyCode keyCode in Config.instance.CommandBinding.Keys)
            {
                KeyBindingManager.KeyBindingManager.Subscribe(keyCode, this);
            }
        }

        internal static void Initialize()
        {
            _instance = new CommandConvertKeyHandler();
        }

        internal static void Destruct()
        {
            _instance = null;
        }

        private void RunCommand(string rawCommandStr, Player player)
        {
            if(rawCommandStr.StartsWith(".") || rawCommandStr.StartsWith("/"))
            {
                rawCommandStr = rawCommandStr.Remove(0, 1);
            }
            else
            {
                throw new Exception("A invalid command is passed into the CommandConvertKeyHandler. Command: " + rawCommandStr + "\n Please make sure that the command is start with . or /");
            }

            player.ReferenceHub.GetComponent<QueryProcessor>()?.ProcessGameConsoleQuery(rawCommandStr);
        }

        public void OnKeyPressed(KeyPressedArg ev)
        {
            RunCommand(Config.instance.CommandBinding[ev.KeyCode], ev.Player);
        }
    }
}
