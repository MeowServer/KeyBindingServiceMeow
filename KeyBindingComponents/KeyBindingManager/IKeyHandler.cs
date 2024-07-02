using Exiled.API.Features;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager
{
    internal class KeyPressedArg
    {
        public Player Player { get; }
        public KeyCode KeyCode { get; }

        public KeyPressedArg(Player player, KeyCode keyCode)
        {
            this.Player = player;
            this.KeyCode = keyCode;
        }
    }

    internal interface IKeyHandler
    {
        void OnKeyPressed(KeyPressedArg ev);
    }
}
