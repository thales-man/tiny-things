//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;

namespace MessageRelay.Models
{
    /// <summary>
    /// Geo fence trigger helper.
    /// </summary>
    public static class GeoFenceTriggerHelper
    {
        /// <summary>
        /// As message command.
        /// </summary>
        /// <returns>The message command.</returns>
        /// <param name="trigger">Trigger.</param>
        public static MessageCommand AsMessageCommand(this GeoFenceTrigger trigger)
        {
            return FromSet<MessageCommand>.Get($"{trigger}");
        }
    }
}
