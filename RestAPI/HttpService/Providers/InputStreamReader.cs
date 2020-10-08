//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Composition;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Http.Service.Contract;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Providers;

namespace Http.Service.Provider
{
    /// <summary>
    /// an input stream reader
    /// </summary>
    [Shared]
    [Export(typeof(IReadInputStreams))]
    public class InputStreamReader : 
        IReadInputStreams
    {
        /// <summary>
        /// Reads all.
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="requestStream">Request stream.</param>
        public async Task<string> ReadAll(IStreamIn requestStream)
        {
            const int bufferSize = 100;
            var result = string.Empty;
            var data = new byte[bufferSize];
            var count = await requestStream.Read(data);
            while (count > 0)
            {
                result += Encoding.UTF8.GetString(data, 0, count);
                if (count < bufferSize)
                {
                    break;
                }

                count = await requestStream.Read(data);
            }

            return result;
        }
    }
}
