using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.API
{
    public class Events
    {
        public delegate void KeyBindReady(KeyBindReadyEventArg ev);
        /// <summary>
        /// Called when the system is ready to bind key for a specific Player.
        /// </summary>
        public static event KeyBindReady OnKeyBindReady;

        internal static void InvokeKeyBindReady(KeyBindReadyEventArg ev)
        {
            OnKeyBindReady?.Invoke(ev);
        }
    }

    public class KeyBindReadyEventArg
    {
        public Player Player;

        internal KeyBindReadyEventArg(Player player)
        {
            Player = player;
        }
    }
}
