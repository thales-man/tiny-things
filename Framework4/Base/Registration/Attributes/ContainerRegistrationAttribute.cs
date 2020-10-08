using System;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the container targeting registration attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public abstract class ContainerRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="contractType">the contract type.</param>
        /// <param name="implementationType">the implementation type.</param>
        /// <param name="scope">the life container scope.</param>
        protected ContainerRegistrationAttribute(Type contractType, Type implementationType, TypeOfRegistrationScope scope)
        {
            (!contractType.IsAssignableFrom(implementationType))
                .AsGuard<ArgumentException>(implementationType.Name);

            ContractType = contractType;
            ImplementationType = implementationType;
            Scope = scope;
        }

        /// <summary>
        /// gets the contract type.
        /// </summary>
        public Type ContractType { get; }

        /// <summary>
        /// gets the implementation type.
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// gets the (container lifetime) scope.
        /// </summary>
        public TypeOfRegistrationScope Scope { get; }
    }
}
