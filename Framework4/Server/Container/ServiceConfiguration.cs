//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Net;
using System.Runtime.Serialization;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the service configuration
    /// </summary>
    [DataContract(Namespace = TinyBooter.Namespace)]
    public sealed class ServiceConfiguration :
        IServiceConfiguration
    {
        const string defaultAddress = "127.0.0.1";
        const int defaultPort = 80;

        public ServiceConfiguration()
        {
            ListeningAddress = defaultAddress;
            ListeningPort = defaultPort;
        }

        [DataMember]
        public string ListeningAddress { get; set; }

        /// <summary>
        /// Gets or sets the listening port.
        /// </summary>
        [DataMember]
        public ushort ListeningPort { get; set; }

        /// <summary>
        /// Gets or sets the service prefix.
        /// </summary>
        [DataMember]
        public string ServicePrefix { get; set; }

        /// <summary>
        /// get the listening address
        /// </summary>
        /// <returns>an ip address</returns>
        public IPAddress GetListeningAddress() =>
            IPAddress.Parse(ListeningAddress);
    }
}
