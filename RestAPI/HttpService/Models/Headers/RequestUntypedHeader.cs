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
    /// 
    /// </summary>
    /// <seealso cref="IRequestUntypedHeader" />
    [Export(typeof(IRequestUntypedHeader))]
    public class RequestUntypedHeader : IRequestUntypedHeader
    {

        /// <summary>
        /// Gets the original value.
        /// </summary>
        public string OriginalValue { get; private set; }

        /// <summary>
        /// Composes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Compose(string value)
        {
            OriginalValue = value;
        }

        /// <summary>
        /// Visits the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Visit(IWriteHttpRequest writer)
        {
            // safe, not implemented...
        }
    }
}
