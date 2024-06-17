using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API
{
    public class KeyBindReadyEventArg
    {
        public Player Player;

        internal KeyBindReadyEventArg(Player player)
        {
            Player = player;
        }
    }

    public class KeyPressedEventArg
    {
        public Player Player;
        public KeyCode Key;

        internal KeyPressedEventArg(Player player, KeyCode key)
        {
            Player = player;
            Key = key;
        }
    }
}
