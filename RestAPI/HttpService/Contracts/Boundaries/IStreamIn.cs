//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// I stream in.
    /// </summary>
    public interface IStreamIn
    {
        /// <summary>
        /// Read the specified buffer.
        /// </summary>
        /// <returns>The read.</returns>
        /// <param name="buffer">Buffer.</param>
        Task<int> Read(byte[] buffer);
    }
}