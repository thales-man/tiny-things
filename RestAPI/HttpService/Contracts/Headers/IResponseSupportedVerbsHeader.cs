//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Sets;
using System.Collections.Generic;

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i response supported verbs header
    /// </summary>
    /// <seealso cref="Headers.IHttpResponseHeader{IEnumerable{WebMethod}}" />
    public interface IResponseSupportedVerbsHeader : IHttpResponseHeader<IEnumerable<WebMethod>> { }
}
