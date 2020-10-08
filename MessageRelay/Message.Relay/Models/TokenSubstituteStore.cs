//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace MessageRelay.Models
{
    /// <summary>
    /// Token substitute store.
    /// </summary>
    [DataContract(Namespace = Relay.Namespace)]
    public class TokenSubstituteStore
    {
        /// <summary>
        /// Gets or sets the token substitutes.
        /// </summary>
        /// <value>The token substitutes.</value>
        [DataMember]
        public TokenSubstitutes TokenSubstitutes { get; set; }
    }
}
