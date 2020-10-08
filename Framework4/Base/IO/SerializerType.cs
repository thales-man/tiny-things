//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework
{
    /// <summary>
    /// the serialiser types.
    /// </summary>
    public enum SerializerType
    {
        /// <summary>
        /// unsupported
        /// </summary>
        Unsupported, // the default(MediaType)

        /// <summary>
        /// json
        /// </summary>
        JSON,

        /// <summary>
        /// xml
        /// </summary>
        XML,

        /// <summary>
        /// binary
        /// </summary>
        Binary,
    }
}
