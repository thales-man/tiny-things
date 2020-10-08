//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Reflection;
using System.Threading.Tasks;

namespace Tiny.API.Contracts
{
    /// <summary>
    /// i carry method metadata
    /// </summary>
    public interface IMethodMetadata
    {
        /// <summary>
        /// Gets the verb.
        /// </summary>
        WebMethod Verb { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has content parameter.
        /// </summary>
        bool HasBodyParameter { get; }

        /// <summary>
        /// Gets the type of the content parameter.
        /// </summary>
        Type BodyParameterType { get; }

        /// <summary>
        /// Composes the specified method information.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="exporter">The exporter.</param>
        /// <param name="servicePrefix">The service prefix.</param>
        void Compose(MethodInfo methodInfo, Func<Export<ITinyAPIController>> exporter, string servicePrefix);

        /// <summary>
        /// Matches the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        bool Match(IRequestDetail uri);

        /// <summary>
        /// Gets the parameter values from.
        /// </summary>
        /// <param name="requestURI">The request URI.</param>
        /// <returns></returns>
        ICollection<object> GetParameterValuesFrom(IRequestDetail requestURI);

        /// <summary>
        /// Invokes the using.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IServerResponse> InvokeUsing(ICollection<object> parameters);
    }
}
