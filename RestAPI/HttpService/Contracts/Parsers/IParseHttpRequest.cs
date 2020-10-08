//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using System.Threading.Tasks;

namespace Http.Service.Contract.Parsers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParseHttpRequest
    {
        /// <summary>
        /// Parses the specified request stream.
        /// </summary>
        /// <param name="requestStream">The request stream.</param>
        /// <returns>a http request</returns>
        Task<IHttpRequest> Parse(IStreamIn requestStream);
    }
}
