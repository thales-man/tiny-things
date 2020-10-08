//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i response encoding header
    /// </summary>
    /// <seealso cref="Headers.IHttpResponseHeader{string}" />
    public interface IResponseEncodingHeader : IHttpResponseHeader<string> { }
}
