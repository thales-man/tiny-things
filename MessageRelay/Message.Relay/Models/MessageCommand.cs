//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace MessageRelay.Models
{
    /// <summary>
    /// Command.
    /// </summary>
    [DataContract(Namespace = Relay.Namespace)]
    public enum MessageCommand
    {
        [EnumMember]
        none,

        [EnumMember]
        enter,

        [EnumMember]
        exit,

        [EnumMember]
        start_media_serving,

        [EnumMember]
        stop_media_serving,

        [EnumMember]
        text_message
    };
}
