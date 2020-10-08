//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Http.Service.Contract.Boundaries
{
    /// <summary>
    /// I stream out.
    /// </summary>
    public interface IStreamOut
    {
        /// <summary>
        /// Write the specified buffer.
        /// </summary>
        /// <returns>The write.</returns>
        /// <param name="buffer">Buffer.</param>
        Task Write(byte[] buffer);

        /// <summary>
        /// Flush this instance.
        /// </summary>
        /// <returns>The flush.</returns>
        Task Flush();
    }
}