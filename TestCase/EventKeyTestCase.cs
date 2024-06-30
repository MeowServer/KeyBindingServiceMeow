using Exiled.API.Features;
using KeyBindingServiceMeow.API.Event.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KeyBindingServiceMeow.TestCase
{
    internal static class EventKeyTestCase
    {
        public static List<KeyCode> Keys = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.F8,
            KeyCode.Break
        };

        public static void OnEnabled()
        {
            if(Config.instance.UseTestCase == false)
                return;

            foreach (KeyCode keyCode in Keys) 
            {
                API.Event.Events.RegisterKeyToEvent(keyCode);
            }

            API.Event.Events.KeyPressed += OnKeyPressed;
        }

        public static void OnDisabled()
        {
            API.Event.Events.KeyPressed -= OnKeyPressed;
        }

        private static void OnKeyPressed(KeyPressedEventArg ev)
        {
            Log.Info("A key received by EventKeyTestCase. Key: " + ev.Key + " Player: " + ev.Player.Nickname);
        }
    }
}
