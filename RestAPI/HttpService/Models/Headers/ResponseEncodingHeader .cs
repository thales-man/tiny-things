//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Headers;
using Http.Service.Contract;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a response encoding header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseEncodingHeader" />
    [Export(typeof(IResponseEncodingHeader))]
    public class ResponseEncodingHeader : ResponseHeaderBase, IResponseEncodingHeader
    {
        public override string Name => HttpHeaderNames.ContentType;
        public void Compose(string value)
        {
            Value = value;
        }
    }
}
