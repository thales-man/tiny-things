//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;

namespace Http.Service.Contract.Providers
{
    /// <summary>
    /// Read input streams.
    /// </summary>
    public interface IReadInputStreams
    {
        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="requestStream">Request stream.</param>
        Task<string> ReadAll(IStreamIn requestStream);
    }
}
