using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.API.Event.EventArgs
{
    public class KeyServiceReadyEventArg
    {
        public Player Player;

        internal KeyServiceReadyEventArg(Player player)
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

    public class ServiceVerifiedEventArg
    {
        public Player Player;

        internal ServiceVerifiedEventArg(Player player)
        {
            Player = player;
        }
    }

    public class ServiceVerificationTimeoutEventArg
    {
        public Player Player;

        internal ServiceVerificationTimeoutEventArg(Player player)
        {
            Player = player;
        }
    }
}
