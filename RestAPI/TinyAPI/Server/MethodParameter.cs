//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using Tiny.API.Contracts;

namespace Tiny.API
{
    /// <summary>
    /// the method parameter
    /// </summary>
    /// <seealso cref="IMethodParameter" />
    public class MethodParameter : 
        IMethodParameter
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets a value indicating whether this parameter is the request body.
        /// </summary>
        public bool IsBody { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Tiny.API.MethodParameter"/> is optional.
        /// </summary>
        /// <value><c>true</c> if is optional; otherwise, <c>false</c>.</value>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets the position of the paramter in the call
        /// </summary>
        public int Position { get; set; }
    }
}
