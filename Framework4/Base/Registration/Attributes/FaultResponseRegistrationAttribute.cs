using System;
using System.Net;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the fault response registration attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class FaultResponseRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaultResponseRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="exceptionType">the exception type.</param>
        /// <param name="responseCode">the response code.</param>
        public FaultResponseRegistrationAttribute(Type exceptionType, HttpStatusCode responseCode)
        {
            (!typeof(Exception).IsAssignableFrom(exceptionType))
                .AsGuard<ArgumentException>(exceptionType.Name);

            ExceptionType = exceptionType;
            ResponseCode = responseCode;
        }

        /// <summary>
        /// gets the exception type.
        /// </summary>
        public Type ExceptionType { get; }

        /// <summary>
        /// gets the response code.
        /// </summary>
        public HttpStatusCode ResponseCode { get; }
    }
}
