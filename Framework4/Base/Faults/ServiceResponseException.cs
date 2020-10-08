using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Faults
{
    /// <summary>
    /// Service response exception.
    /// Constructors and decorators are here to satisfy the static analysis tool
    /// as a consequence, excluded from coverage as they can't be tested properly.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ServiceResponseException :
        Exception,
        IServiceResponseFault,
        ISupportFaultRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponseException"/> class.
        /// </summary>
        public ServiceResponseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponseException "/> class.
        /// </summary>
        /// <param name="message">message.</param>
        public ServiceResponseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponseException"/> class.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="innerException">inner exception.</param>
        public ServiceResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponseException"/> class.
        /// </summary>
        /// <param name="info">info.</param>
        /// <param name="context">context.</param>
        protected ServiceResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <inheritdoc/>
        public Type ExceptionType => GetType();

        /// <inheritdoc/>
        public string ExceptionName => ExceptionType.Name;
    }
}