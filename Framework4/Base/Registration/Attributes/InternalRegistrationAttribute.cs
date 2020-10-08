using System;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the internal targeting registration attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class InternalRegistrationAttribute :
        ContainerRegistrationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="contractType">the contract type.</param>
        /// <param name="implementationType">the implementation type.</param>
        /// <param name="scope">the life container scope.</param>
        public InternalRegistrationAttribute(Type contractType, Type implementationType, TypeOfRegistrationScope scope)
            : base(contractType, implementationType, scope)
        {
            // the implementation should support service registration, rather than the contract type.
            // But we check both.
            (!SupportServiceRegistration(implementationType) && !SupportServiceRegistration(contractType))
                .AsGuard<ArgumentException>(implementationType.Name);
        }

        /// <summary>
        /// Support service registration.
        /// </summary>
        /// <param name="givenType">(The) given type.</param>
        /// <returns>True if supported.</returns>
        public bool SupportServiceRegistration(Type givenType) => typeof(ISupportServiceRegistration).IsAssignableFrom(givenType);
    }
}
