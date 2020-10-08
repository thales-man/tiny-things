//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System.Composition;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Http.Service.Contract.Boundaries;

namespace Http.Service.Provider
{
    /// <summary>
    /// Socket stream adapter.
    /// this item is not "shared", it's run from a factory export in the listener
    /// </summary>
    [Export(typeof(IAdaptSocketStreams))]
    internal sealed class SocketStreamAdapter :
            IAdaptSocketStreams
    {
        /// <summary>
        /// The tcp client.
        /// </summary>
        private TcpClient _tcpClient;

        /// <summary>
        /// The stream.
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// Compose this instance.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        public void Compose(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _stream = _tcpClient.GetStream();
        }

        /// <summary>
        /// Read the specified buffer.
        /// </summary>
        /// <returns>The read.</returns>
        /// <param name="buffer">Buffer.</param>
        public async Task<int> Read(byte[] buffer) =>
            await _stream.ReadAsync(buffer, 0, buffer.Length);

        /// <summary>
        /// Write the specified buffer.
        /// </summary>
        /// <returns>The write.</returns>
        /// <param name="buffer">Buffer.</param>
        public async Task Write(byte[] buffer) =>
            await _stream.WriteAsync(buffer, 0, buffer.Length);

        /// <summary>
        /// Flush this instance.
        /// </summary>
        /// <returns>The flush.</returns>
        public async Task Flush() =>
            await _stream.FlushAsync();

        /// <summary>
        /// Releases all resource used by the <see cref="T:Http.Service.Contract.SocketStreamAdapter"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:Http.Service.Contract.SocketStreamAdapter"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:Http.Service.Contract.SocketStreamAdapter"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:Http.Service.Contract.SocketStreamAdapter"/> so the garbage collector can reclaim the memory
        /// that the <see cref="T:Http.Service.Contract.SocketStreamAdapter"/> was occupying.</remarks>
        public void Dispose()
        {
            _stream.Dispose();
            _tcpClient.Dispose();
        }
    }
}