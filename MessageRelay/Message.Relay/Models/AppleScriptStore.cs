//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Runtime.Serialization;

namespace MessageRelay.Models
{
    /// <summary>
    /// Apple script store.
    /// </summary>
    [DataContract(Namespace = Relay.Namespace)]
    public sealed class AppleScriptStore
    {
        /// <summary>
        /// Gets or sets the apple scripts.
        /// </summary>
        [DataMember]
        public AppleScripts AppleScripts { get; set; }
    }
}
