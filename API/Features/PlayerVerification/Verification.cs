using Exiled.API.Features;
using KeyBindingServiceMeow.KeyApplications.PlayerVerification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingServiceMeow.API.Features.PlayerVerification
{
    public static class Verification
    {
        /// <summary>
        /// Check whether a Player can use Key Binding Service
        /// </summary>
        public static bool IsVerified(Player player)
        {
            if (!Config.instance.UsePlayerVerification)
                throw new Exception("Player verification is disabled in the config file");

            return Verifier.IsSetup(player);
        }

        /// <summary>
        /// Check whether a Player is timeout for verification
        /// </summary>
        public static bool IsTimeout(Player player)
        {
            if (!Config.instance.UsePlayerVerification)
                throw new Exception("Player verification is disabled in the config file");

            return Verifier.IsTimeout(player);
        }

        /// <summary>
        /// Check whether the verification is enabled
        /// </summary>
        /// <returns></returns>
        public static bool IsVerificationEnabled()
        {
            return Config.instance.UsePlayerVerification;
        }
    }
}
