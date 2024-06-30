using Exiled.API.Features;
using KeyBindingServiceMeow.ClientSetupHelper;
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
        /// Check whether a player can use Key Binding Service
        /// </summary>
        public static bool IsVerified(Player player)
        {
            if (!Config.instance.UsePlayerVerification)
                throw new Exception("Player verification is disabled in the config file");

            return Verificator.IsSetup(player);
        }

        /// <summary>
        /// Check whether a player is timeout for verification
        /// </summary>
        public static bool IsTimeout(Player player)
        {
            if (!Config.instance.UsePlayerVerification)
                throw new Exception("Player verification is disabled in the config file");

            return Verificator.IsTimeout(player);
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
