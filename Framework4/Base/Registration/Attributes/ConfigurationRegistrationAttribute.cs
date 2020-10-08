using System;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Registration.Attributes
{
    /// <summary>
    /// the configuration registration attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class ConfigurationRegistrationAttribute :
        ContainerRegistrationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRegistrationAttribute"/> class.
        /// </summary>
        /// <param name="contractType">the contract type.</param>
        /// <param name="implementationType">the implementation type.</param>
        /// <param name="configurationSection">the configuration section.</param>
        public ConfigurationRegistrationAttribute(Type contractType, Type implementationType, string configurationSection)
            : base(contractType, implementationType, TypeOfRegistrationScope.Singleton)
        {
            Section = configurationSection;

            // the implementation should support service registration, rather than the contract type.
            // But we check both.
            (!SupportConfigurationRegistration(implementationType) && !SupportConfigurationRegistration(contractType))
                .AsGuard<ArgumentException>(implementationType.Name);
        }

        /// <summary>
        /// gets the response code.
        /// </summary>
        public string Section { get; }

        /// <summary>
        /// Support configuration registration.
        /// </summary>
        /// <param name="givenType">(The) given type.</param>
        /// <returns>True if supported.</returns>
        public bool SupportConfigurationRegistration(Type givenType) => typeof(ISupportConfigurationRegistration).IsAssignableFrom(givenType);
    }
}
