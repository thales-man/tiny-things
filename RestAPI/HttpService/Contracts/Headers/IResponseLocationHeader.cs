//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Http.Service.Contract.Headers
{

    /// <summary>
    /// i response location header
    /// </summary>
    /// <seealso cref="Headers.IHttpResponseHeader{Uri}" />
    public interface IResponseLocationHeader : IHttpResponseHeader<Uri> { }
}
