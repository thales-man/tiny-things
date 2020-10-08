//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Sets;
using System;
using System.Collections.Generic;

namespace Tiny.API.Contracts
{

    /// <summary>
    /// the provide controller method metadata
    /// </summary>
    public interface IProvideEndpointMethods
    {

        /// <summary>
        /// Composes the metadata provider using the specified..
        /// </summary>
        /// <param name="servicePrefix">service prefix</param>
        void Compose(string servicePrefix);

        /// <summary>
        /// Gets the method for
        /// </summary>
        /// <param name="thisURI">this URI.</param>
        /// <param name="usingWebMethod">using web method.</param>
        /// <returns>the method metadata</returns>
        IMethodMetadata GetMethodFor(IRequestDetail thisURI, WebMethod usingWebMethod);

        /// <summary>
        /// Gets the supported web methods for.
        /// </summary>
        /// <param name="thisURI">this UR.</param>
        /// <returns>a collection of supported web methods for that uri</returns>
        ICollection<WebMethod> GetSupportedWebMethodsFor(IRequestDetail thisURI);
    }
}
