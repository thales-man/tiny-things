// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.IO;

namespace Tiny.Framework.Container
{
    /// <summary>
    /// the service configuration.
    /// </summary>
    public sealed class HostConfiguration :
        IHostConfiguration
    {
        /// <summary>
        /// the common file name.
        /// </summary>
        public const string CommonFileName = @"service_configuration.json";

        /// <summary>
        /// gets or sets the listening address.
        /// </summary>
        public string ListeningProtocol { get; set; } = "http";

        /// <summary>
        /// gets or sets the listening address.
        /// </summary>
        public string ListeningAddress { get; set; } = "*";

        /// <summary>
        /// gets or sets the web root folder name.
        /// </summary>
        public string WebRootFolderName { get; set; } = "wwwroot";

        /// <summary>
        /// gets or sets the listening port.
        /// </summary>
        public ushort ListeningPort { get; set; } = 5000;

        /// <inheritdoc/>
        public string ContentRootPath { get; set; } = Environment.CurrentDirectory;

        /// <inheritdoc/>
        public string StaticFilePath =>
            Path.Combine(ContentRootPath, WebRootFolderName);

        /// <inheritdoc/>
        public string ServerListeningAddress =>
            $"{ListeningProtocol}://{ListeningAddress}:{ListeningPort}";
    }
}
