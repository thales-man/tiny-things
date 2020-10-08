// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Container
{
    /// <summary>
    /// i host configuration.
    /// </summary>
    public interface IHostConfiguration
    {
        /// <summary>
        /// gets the content root path.
        /// </summary>
        string ContentRootPath { get; }

        /// <summary>
        /// gets the static file path.
        /// </summary>
        string StaticFilePath { get; }

        /// <summary>
        /// gets the server listening address.
        /// </summary>
        string ServerListeningAddress { get; }
    }
}