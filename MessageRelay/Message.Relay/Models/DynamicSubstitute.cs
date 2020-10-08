//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace MessageRelay.Models
{
    /// <summary>
    /// Dynamic substitute (tokens).
    /// these are target tokens done on the 'fly'
    /// </summary>
    public static class DynamicSubstitute
    {
        /// <summary>
        /// The message token.
        /// </summary>
        public const string MessageToken = "${text.message}";

        /// <summary>
        /// The phone token.
        /// </summary>
        public const string PhoneToken = "${target.phone}";

        /// <summary>
        /// The person token.
        /// </summary>
        public const string PersonToken = "${target.person}";

        /// <summary>
        /// The switch token.
        /// </summary>
        public const string SwitchToken = "${device.switch}";
    }
}
