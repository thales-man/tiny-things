//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace MessageRelay.Models
{
    /// <summary>
    /// Apple script item.
    /// </summary>
    [DataContract(Namespace = Relay.Namespace)]
    public sealed class AppleScriptItem
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        [DataMember]
        public MessageCommand Command { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        [DataMember]
        public string Script { get; set; }
    }
}
