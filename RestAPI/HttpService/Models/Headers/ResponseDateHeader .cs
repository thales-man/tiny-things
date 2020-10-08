//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract;
using Http.Service.Contract.Headers;
using System;
using System.Composition;

namespace Http.Service.Model.Headers.Response
{

    /// <summary>
    /// a response date header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseDateHeader" />
    [Export(typeof(IResponseDateHeader))]
    public class ResponseDateHeader : ResponseHeaderBase, IResponseDateHeader
    {

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => HttpHeaderNames.Date;

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(DateTime value)
        {
            Value = value.ToString("r");
        }
    }
}
