//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;

namespace Http.Service.Contract.Parsers
{

    /// <summary>
    /// i parse a request part
    /// </summary>
    public interface IParseARequestPart
    {

        /// <summary>
        /// Handles...
        /// </summary>
        /// <param name="thisPart">this part</param>
        /// <param name="usingWriter">using writer</param>
        void Handle(string thisPart, IWriteHttpRequest usingWriter);
    }
}
