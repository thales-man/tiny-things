//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace MessageRelay.Models
{
    /// <summary>
    /// Token substitute item.
    /// </summary>
    [DataContract(Namespace = Relay.Namespace)]
    public class TokenSubstituteItem
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [DataMember]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        [DataMember]
        public string Substitute { get; set; }
    }
}
