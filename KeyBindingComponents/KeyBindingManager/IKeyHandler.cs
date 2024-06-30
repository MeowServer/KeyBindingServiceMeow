using Exiled.API.Features;
using KeyBindingServiceMeow.BindingManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.KeyBindingComponents.KeyBindingManager
{
    internal class KeyPressedArg
    {
        public Player player { get; }
        public KeyCode keyCode { get; }

        public KeyPressedArg(Player player, KeyCode keyCode)
        {
            this.player = player;
            this.keyCode = keyCode;
        }
    }

    internal interface IKeyHandler
    {
        void OnKeyPressed(KeyPressedArg ev);
    }
}
