using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;

namespace KeyBindingServiceMeow.KeyApplications.PlayerVerification.Broadcaster
{
    internal class InternalBroadcaster : IBroadcaster
    {
        public void Broadcast(string message, Player player)
        {
            player.Broadcast(Config.instance.MessageTime, message, global::Broadcast.BroadcastFlags.Normal, true);
        }
    }

    internal class HintServiceBroadcaster : IBroadcaster
    {
        public void Broadcast(string message, Player player)
        {
            return;//Temporarly block this function. Will be solved before next release
            //Wait for hint service to initialize
            //Timing.CallDelayed(0.5f, () =>
            //{
            //    HintServiceMeow.PlayerUI.Get(player).ShowOtherHint(message, Config.instance.MessageTime);
            //});
        }
    }
}
