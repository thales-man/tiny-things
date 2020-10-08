//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;

namespace Http.Service.Contract.Parsers
{

    /// <summary>
    /// i parse a response part
    /// </summary>
    public interface IParseAResponsePart
    {

        /// <summary>
        /// To the bytes.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>a byte array</returns>
        byte[] ToBytes(IReadHttpResponse response);
    }
}
