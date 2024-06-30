using Exiled.API.Features;
using KeyBindingServiceMeow.API.Event.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.TestCase
{
    internal static class VerificationTestCase
    {
        public static void OnEnabled()
        {
            if (!API.Features.PlayerVerification.Verification.IsVerificationEnabled())
                return;

            API.Event.Events.ServiceVerified += OnServiceVerified;
            API.Event.Events.ServiceVerificationTimeout += OnServiceVerificationTimeout;
        }

        public static void OnDisabled()
        {
            API.Event.Events.ServiceVerified -= OnServiceVerified;
            API.Event.Events.ServiceVerificationTimeout -= OnServiceVerificationTimeout;
        }

        private static void OnServiceVerified(ServiceVerifiedEventArg ev)
        {
            Log.Info("Service verified for player: " + ev.Player.Nickname);
            Log.Info("IsVerified: " + API.Features.PlayerVerification.Verification.IsVerified(ev.Player));
            Log.Info("IsTimeout: " + API.Features.PlayerVerification.Verification.IsTimeout(ev.Player));
        }

        private static void OnServiceVerificationTimeout(ServiceVerificationTimeoutEventArg ev)
        {
            Log.Info("Service verification timeout for player: " + ev.Player.Nickname);
            Log.Info("IsVerified: " + API.Features.PlayerVerification.Verification.IsVerified(ev.Player));
            Log.Info("IsTimeout: " + API.Features.PlayerVerification.Verification.IsTimeout(ev.Player));
        }
    }
}
