//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using System.Composition;

namespace Http.Service.Provider.RequestParsers
{

    /// <summary>
    /// the request body parser
    /// </summary>
    /// <seealso cref="IParseRequestBody" />
    [Shared]
    [Export(typeof(IParseRequestBody))]
    public class RequestBodyParser : IParseRequestBody
    {

        /// <summary>
        /// Handles...
        /// </summary>
        /// <param name="thisPart">this part</param>
        /// <param name="usingWriter">using writer.</param>
        public void Handle(string thisPart, IWriteHttpRequest usingWriter)
        {
            usingWriter.SetContent(thisPart);
        }
    }
}
