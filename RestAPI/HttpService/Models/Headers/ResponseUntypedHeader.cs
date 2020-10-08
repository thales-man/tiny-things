//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Headers;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a response untyped header
    /// </summary>
    /// <seealso cref="ResponseHeaderBase" />
    /// <seealso cref="IResponseUntypedHeader" />
    [Export(typeof(IResponseUntypedHeader ))]
    public class ResponseUntypedHeader : ResponseHeaderBase, IResponseUntypedHeader
    {

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string[] value)
        {
            Name = value[0];
            Value = value[1];
        }
    }
}
