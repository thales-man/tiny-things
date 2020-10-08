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
    /// No content exception.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class NoContentException :
        ServiceResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        public NoContentException()
            : base(GetMessage())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoContentException"/> class.
        /// </summary>
        /// <param name="info">info.</param>
        /// <param name="context">context.</param>
        protected NoContentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// get message.
        /// </summary>
        /// <param name="parentResourceName">the parent resource name.</param>
        /// <returns>the exception message.</returns>
        public static string GetMessage(string parentResourceName = null) =>
            parentResourceName != null
                ? $"'{parentResourceName}' does not exist"
                : "Resource does not exist";
    }
}