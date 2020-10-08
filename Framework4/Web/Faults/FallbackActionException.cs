// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Tiny.Framework.Faults;

namespace Tiny.Framework.Web.Faults
{
    /// <summary>
    /// Fallback action exception.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FallbackActionException :
        ServiceResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FallbackActionException"/> class.
        /// </summary>
        /// <param name="info">info.</param>
        /// <param name="context">context.</param>
        protected FallbackActionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}