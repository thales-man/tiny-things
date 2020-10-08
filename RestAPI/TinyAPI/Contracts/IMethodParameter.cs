//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;

namespace Tiny.API.Contracts
{
    /// <summary>
    /// the method parameter metadata
    /// </summary>
    public interface IMethodParameter
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is content.
        /// </summary>
        bool IsBody { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Tiny.API.Contracts.IMethodParameter"/> is optional.
        /// </summary>
        /// <value><c>true</c> if is optional; otherwise, <c>false</c>.</value>
        bool IsOptional { get; }

        /// <summary>
        /// Gets the position of the paramter in the call
        /// </summary>
        int Position { get; }
    }
}
