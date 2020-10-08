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
    /// Conflicting resource exception.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ConflictingResourceException :
        ServiceResponseException
    {
        /// <summary>
        /// the exception message.
        /// </summary>
        public const string ExceptionMessage = "Resource already exists";

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictingResourceException"/> class.
        /// </summary>
        public ConflictingResourceException()
            : base(ExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictingResourceException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="innerException">inner exception.</param>
        public ConflictingResourceException(string message, Exception innerException)
            : base(ExceptionMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictingResourceException"/> class.
        /// </summary>
        /// <param name="info">info.</param>
        /// <param name="context">context.</param>
        protected ConflictingResourceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}