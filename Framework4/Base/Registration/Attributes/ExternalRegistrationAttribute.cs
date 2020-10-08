using System;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the external targeting registration attribute.
    /// for those things you want to bring in that don't 'support' generic host registration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class ExternalRegistrationAttribute :
        ContainerRegistrationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="contractType">the contract type.</param>
        /// <param name="implementationType">the implementation type.</param>
        /// <param name="scope">the life container scope.</param>
        public ExternalRegistrationAttribute(Type contractType, Type implementationType, TypeOfRegistrationScope scope)
            : base(contractType, implementationType, scope)
        {
        }
    }
}
