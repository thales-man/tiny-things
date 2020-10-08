//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Http.Service.Contract.Boundaries;
using Http.Service.Contract.Headers;
using System.Composition;

namespace Http.Service.Model.Headers
{

    /// <summary>
    /// a request content length header
    /// </summary>
    /// <seealso cref="IRequestContentLengthHeader" />
    [Export(typeof(IRequestContentLengthHeader))]
    public class RequestContentLengthHeader : 
        IRequestContentLengthHeader
    {

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            Length = int.Parse(value);
        }

        /// <summary>
        /// Visits the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Visit(IWriteHttpRequest writer)
        {
            writer.SetContentLength(Length);
        }
    }
}
