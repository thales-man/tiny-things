//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Http.Service.Contract.Providers;
using Http.Service.Contract.Sets;
using Tiny.Framework;
using Tiny.Framework.IO;
using Tiny.Framework.Utility;

namespace Http.Service.Provider
{
    /// <summary>
    /// the negotiated content serialiser / deserialiser
    /// </summary>
    /// <seealso cref="INegotiateContentSerialization" />
    [Shared]
    [Export(typeof(INegotiateContentSerialization))]
    public class ContentSerializationNegotiator : 
        INegotiateContentSerialization
    {
        /// <summary>
        /// Gets or sets the XML convert.
        /// </summary>
        [ImportMany]
        public IEnumerable<ISerializeTypes> Converters { get; set; }

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>the rehydrated content object</returns>
        public object FromString(string content, Type contentType, MediaType contentMediaType)
        {
            It.IsEmpty(content)
                .AsGuard<ArgumentNullException>();
            It.IsNull(contentType)
                .AsGuard<ArgumentNullException>();

            var serialiser = GetSerialiser(contentMediaType);
            return serialiser.FromString(content, contentType);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(object content, Type contentType, MediaType contentMediaType)
        {
            It.IsNull(content)
                .AsGuard<ArgumentNullException>();

            var serialiser = GetSerialiser(contentMediaType);
            return serialiser.ToString(content, contentType);
        }

        /// <summary>
        /// Gets the serialiser.
        /// </summary>
        /// <param name="contentMediaType">Type of the content media.</param>
        /// <returns>the selected serialiser</returns>
        public ISerializeTypes GetSerialiser(MediaType contentMediaType)
        {
            It.IsOutOfRange(contentMediaType, MediaType.JSON, MediaType.XML)
                .AsGuard<ArgumentOutOfRangeException>();

            var st = FromSet<SerialiserType>.Get(contentMediaType.ToString());
            return Converters.FirstOrDefault(x => x.SerialiserType == st);
        }
    }
}
