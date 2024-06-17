using CommandSystem;
using Exiled.API.Features;
using Exiled.Events;
using KeyBindingServiceMeow.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KeyBindingServiceMeow.KeyBindingManager
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BindingReceiverCommand : ICommand
    {
        public string Command { get; } = "CommandHandler";

        public string[] Aliases { get; } = new string[0];

        public string Description { get; } = "Used for KeyBindingMeow. Do not use this command if you're unsure what this is.";

        public bool SanitizeResponse { get; } = true;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            List<string> args = new List<string>(arguments);

            if (args.Count < 1)
            {
                Log.Error("No enough arguments passed to BindingReceiverCommand.");

                response = "An internal error had occured while handling the binding key";
                return false;
            }
            else if(args.Count > 1)
            {
                Log.Error("Too many arguments passed to BindingReceiverCommand.");

                response = "An internal error had occured while handling the binding key";
                return false;
            }

            KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), args[0]);

            //Invoke event if the key was registered in event
            try
            {
                if (EventKeysManager.IsBinded(keyCode))
                {
                    API.Events.InvokeKeyPressed(new KeyPressedEventArg(Player.Get(sender), keyCode));
                }
            }
            catch
            {
                Log.Error("Failed to invoke KeyPressed event for key: " + args[0]);
            }

            //Invoke the actions directly registered in KeyBindingManager
            try
            {
                KeyBindingManager.Get(Player.Get(sender)).HandleKey(keyCode);
            }
            catch
            {
                Log.Error("Failed to handle key: " + args[0]);
            }

            response = string.Empty;
            return true;
        }
    }
}
