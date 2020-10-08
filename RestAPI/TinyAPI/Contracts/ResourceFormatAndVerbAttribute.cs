//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using Http.Service.Contract.Sets;
using System;

namespace Tiny.API.Contracts
{
    /// <summary>
    /// the controller method resource format and verb attribute
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ResourceFormatAndVerbAttribute : Attribute
    {

        /// <summary>
        /// Gets the URI format.
        /// </summary>
        public string URIFormat { get; }

        /// <summary>
        /// Gets the verb.
        /// </summary>
        public WebMethod Verb { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceFormatAndVerbAttribute"/> class.
        /// </summary>
        /// <param name="uriFormat">The URI format.</param>
        /// <param name="verb">The verb.</param>
        public ResourceFormatAndVerbAttribute(string uriFormat, WebMethod verb)
        {
            URIFormat = uriFormat;
            Verb = verb;
        }
    }
}
