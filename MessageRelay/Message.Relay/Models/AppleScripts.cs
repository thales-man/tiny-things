//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Runtime.Serialization;
using Tiny.Framework.IO;

namespace MessageRelay.Models
{
    /// <summary>
    /// Apple scripts.
    /// </summary>
    [CollectionDataContract(Namespace = Relay.Namespace)]
    public sealed class AppleScripts :
        CleanList<AppleScriptItem>
    {
    }
}
