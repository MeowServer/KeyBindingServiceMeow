using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.KeyApplications.PlayerVerification.Broadcaster
{
    internal interface IBroadcaster
    {
        void Broadcast(string message, Player player);
    }
}
