//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using Http.Service.Contract.Parsers;
using System;
using System.Composition;
using Tiny.Framework.Utility;

namespace Http.Service.Provider.RequestParsers
{

    /// <summary>
    /// the request header parser
    /// </summary>
    /// <seealso cref="IParseRequestHeaders" />
    [Shared]
    [Export(typeof(IParseRequestHeaders))]
    public class RequestHeaderParser : IParseRequestHeaders
    {
        static string[] headerSplitter = { ": " };

        /// <summary>
        /// Gets or sets the request header factory.
        /// </summary>
        [Import]
        public ICreateHttpRequestHeaders Header { get; set; }

        /// <summary>
        /// Called when [handle part].
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="usingWriter">The writer.</param>
        public void Handle(string thisPart, IWriteHttpRequest usingWriter)
        {
            var headerParts = thisPart.Split(headerSplitter, StringSplitOptions.RemoveEmptyEntries);

            var faildHeaderCount = !It.HasCountOf(headerParts, 2);
            faildHeaderCount.AsGuard<ArgumentOutOfRangeException>();

            usingWriter.AddHeader(Header.Create(headerParts[0], headerParts[1]));
        }
    }
}
