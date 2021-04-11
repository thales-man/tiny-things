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
    /// Relay request.
    /// TODO: get the API to build a complex item from an incoming parameter list
    /// </summary>
    [ParameterisedBodyContent(typeof(RelayRequest), typeof(IRelayRequest))]
    public sealed class RelayRequest :
        IRelayRequest
    {
        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        [ParameterMap("device", nameof(Device))]
        public Guid Device { get; set; }

        /// <summary>
        /// Gets or sets the trigger.
        /// </summary>
        [ParameterMap("trigger", nameof(Trigger))]
        public GeoFenceTrigger Trigger { get; set; }
    }
}
