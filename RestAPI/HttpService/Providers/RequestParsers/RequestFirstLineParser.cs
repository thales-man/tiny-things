//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Utility;
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Parsers;
using Http.Service.Contract.Sets;
using System.Composition;
using System;
using System.Web;
using Http.Service.Models.Boundaries;

namespace Http.Service.Provider.RequestParsers
{
    /// <summary>
    /// the requested web method parser
    /// </summary>
    /// <seealso cref="IParseRequestFirstLine" />
    [Shared]
    [Export(typeof(IParseRequestFirstLine))]
    public class RequestFirstLineParser : IParseRequestFirstLine
    {
        /// <summary>
        /// Handles the specified this part.
        /// </summary>
        /// <param name="thisPart">this part.</param>
        /// <param name="usingWriter">using writer.</param>
        public void Handle(string thisPart, IWriteHttpRequest usingWriter)
        {
            var firstLine = thisPart.Split(' ');

            var failedCountCheck = !It.HasCountOf(firstLine, 3);
            failedCountCheck.AsGuard<ArgumentOutOfRangeException>();

            usingWriter.SetWebMethod(GetWebMethod(firstLine[0]));
            usingWriter.SetURI(GetURI(firstLine[1]));
            usingWriter.SetVersion(GetVersion(firstLine[2]));
        }

        /// <summary>
        /// Gets the web method.
        /// </summary>
        /// <param name="usingRequestVerb">using request verb</param>
        /// <returns>
        /// the web method
        /// </returns>
        public WebMethod GetWebMethod(string usingRequestVerb)
        {
            return FromSet<WebMethod>.Get(usingRequestVerb, WebMethod.Unsupported);
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <param name="usingLocation">using location.</param>
        /// <returns>a new uri for the location</returns>
        public IRequestDetail GetURI(string usingLocation)
        {
            return new RequestDetail(usingLocation);
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <param name="usingHttpVersion">using the HTTP version.</param>
        /// <returns></returns>
        public Version GetVersion(string usingHttpVersion)
        {
            var details = usingHttpVersion.Split('/');

            var faileDetailCount = !It.HasCountOf(details, 2);
            faileDetailCount.AsGuard<ArgumentOutOfRangeException>();

            return new Version(details[1]);
        }
    }
}
