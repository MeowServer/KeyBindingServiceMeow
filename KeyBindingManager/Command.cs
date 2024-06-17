using CommandSystem;
using Exiled.API.Features;

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

            KeyBindingManager.Get(Player.Get(sender)).HandleKey((KeyCode)Enum.Parse(typeof(KeyCode), args[0]));

            response = string.Empty;
            return true;
        }
    }
}
