//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using Tiny.API.Contracts;

namespace MessageRelay.Models
{
    /// <summary>
    /// I relay request.
    /// </summary>
    [ParameterisedBodyContent(typeof(RelayRequest), typeof(IRelayRequest))]
    public interface IRelayRequest
    {
        /// <summary>
        /// Gets the device.
        /// </summary>
        Guid Device { get; }

        /// <summary>
        /// Gets the trigger.
        /// </summary>
        GeoFenceTrigger Trigger { get; }
    }
}
