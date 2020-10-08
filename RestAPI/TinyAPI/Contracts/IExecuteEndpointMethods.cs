//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using System.Threading.Tasks;

namespace Tiny.API.Contracts
{

    /// <summary>
    /// i execute controller methods
    /// </summary>
    public interface IExecuteEndpointMethods
    {

        /// <summary>
        /// Executes the specified...
        /// </summary>
        /// <param name="method">method.</param>
        /// <param name="usingRequest">using request.</param>
        /// <returns></returns>
        Task<IServerResponse> Execute(IMethodMetadata method, IServerRequest usingRequest);
    }
}
