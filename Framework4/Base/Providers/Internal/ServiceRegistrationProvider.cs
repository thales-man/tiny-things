using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Providers.Internal
{
    /// <summary>
    /// the generic host registration provider (implementation).
    /// This is a bootstrapping service that doesn't need container registration.
    /// </summary>
    internal sealed class ServiceRegistrationProvider :
        IProvideRegistrationServices
    {
        private bool isInitialised = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRegistrationProvider"/> class.
        /// </summary>
        /// <param name="details">the registration details provider.</param>
        /// <param name="configuration">the configuration provider.</param>
        /// <param name="environment">the environment provider.</param>
        public ServiceRegistrationProvider(
            IProvideRegistrationDetails details,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            It.IsNull(details)
                .AsGuard<ArgumentNullException>(nameof(details));
            It.IsNull(configuration)
                .AsGuard<ArgumentNullException>(nameof(configuration));

            ActionMap = new RegistrationMap
            {
                [TypeOfRegistrationScope.Scoped] = AddScoped,
                [TypeOfRegistrationScope.Transient] = AddTransient,
                [TypeOfRegistrationScope.Singleton] = AddSingleton,
            };

            Details = details;
            Configuration = configuration;
            Environment = environment;
        }

        /// <summary>
        /// gets the registration details provider.
        /// </summary>
        internal IProvideRegistrationDetails Details { get; }

        /// <summary>
        /// gets the application configuration.
        /// </summary>
        internal IConfiguration Configuration { get; }

        /// <summary>
        /// gets the environment.
        /// </summary>
        internal IHostEnvironment Environment { get; }

        /// <summary>
        /// gets the action map.
        /// </summary>
        internal RegistrationMap ActionMap { get; }

        /// <summary>
        /// gets the builder registrations.
        /// </summary>
        internal ICollection<AddRoutineRegistrationAttribute> Builders { get; }
            = Collection.Empty<AddRoutineRegistrationAttribute>();

        /// <summary>
        /// gets the deployer registrations.
        /// </summary>
        internal ICollection<UseRoutineRegistrationAttribute> Deployers { get; }
            = Collection.Empty<UseRoutineRegistrationAttribute>();

        /// <summary>
        /// gets the configuration registrations.
        /// </summary>
        internal ICollection<ConfigurationRegistrationAttribute> Configurations { get; }
            = Collection.Empty<ConfigurationRegistrationAttribute>();

        /// <summary>
        /// gets the service registrations.
        /// </summary>
        internal ICollection<ContainerRegistrationAttribute> Registrations { get; }
            = Collection.Empty<ContainerRegistrationAttribute>();

        /// <inheritdoc/>
        public void RegisterWith(IServiceCollection containerCollection)
        {
            Initialise();

            Builders.ForEach(registration => AddService(containerCollection, registration));
            Configurations.ForEach(registration => RegisterConfiguration(containerCollection, registration));
            Registrations.ForEach(registration => RegisterService(containerCollection, registration));
        }

        /// <inheritdoc/>
        public void BuildWith(IApplicationBuilder builder)
        {
            Initialise();

            Deployers.ForEach(registration => UseService(builder, registration));
        }

        /// <summary>
        /// initialise.
        /// </summary>
        internal void Initialise()
        {
            if (isInitialised)
            {
                return;
            }

            isInitialised = true;
            var cache = Details.Load().Result;
            cache.ForEach(LoadRegistrations);
        }

        /// <summary>
        /// Load (the) registrations...
        /// </summary>
        /// <param name="registrant">(for the) registrant.</param>
        internal void LoadRegistrations(Assembly registrant)
        {
            It.IsNull(registrant)
                .AsGuard<ArgumentNullException>(nameof(registrant));

            Details.GetAttributeListFor<AddRoutineRegistrationAttribute>(registrant)
                .ForEach(Builders.Add);
            Details.GetAttributeListFor<UseRoutineRegistrationAttribute>(registrant)
                .ForEach(Deployers.Add);
            Details.GetAttributeListFor<ConfigurationRegistrationAttribute>(registrant)
                .ForEach(Configurations.Add);
            Details.GetAttributeListFor<ExternalRegistrationAttribute>(registrant)
                .ForEach(Registrations.Add);
            Details.GetAttributeListFor<InternalRegistrationAttribute>(registrant)
                .ForEach(Registrations.Add);
        }

        /// <summary>
        /// add the service.
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void AddService(IServiceCollection containerCollection, AddRoutineRegistrationAttribute registration) =>
            registration.AddAction(containerCollection, Configuration, Environment);

        /// <summary>
        /// use (deploy) the service.
        /// </summary>
        /// <param name="builder">the application builder.</param>
        /// <param name="registration">the deployment registration.</param>
        internal void UseService(IApplicationBuilder builder, UseRoutineRegistrationAttribute registration) =>
            registration.UseAction(builder, Configuration, Environment);

        /// <summary>
        /// Load configuration.
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void RegisterConfiguration(IServiceCollection containerCollection, ConfigurationRegistrationAttribute registration)
        {
            ColorConsole.Startup($"registering configuration: {registration.ContractType}, {registration.Section}");

            var instance = Activator.CreateInstance(registration.ImplementationType);
            ConfigureItem(registration, instance);
            containerCollection.AddSingleton(registration.ContractType, instance);
        }

        /// <summary>
        /// Configure item...
        /// </summary>
        /// <param name="registration">the registration attribute.</param>
        /// <param name="item">the item.</param>
        internal void ConfigureItem(ConfigurationRegistrationAttribute registration, object item)
        {
            Configuration.GetSection(registration.Section).Bind(item);
        }

        /// <summary>
        /// Register service.
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void RegisterService(IServiceCollection containerCollection, ContainerRegistrationAttribute registration)
        {
            ColorConsole.Startup($"registering service: {registration.ContractType}, {registration.Scope}");
            ActionMap[registration.Scope].Invoke(containerCollection, registration);
        }

        /// <summary>
        /// add (a) scoped (item).
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void AddScoped(IServiceCollection containerCollection, ContainerRegistrationAttribute registration) =>
            containerCollection.AddScoped(registration.ContractType, registration.ImplementationType);

        /// <summary>
        /// add (a) transient (item).
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void AddTransient(IServiceCollection containerCollection, ContainerRegistrationAttribute registration) =>
            containerCollection.AddTransient(registration.ContractType, registration.ImplementationType);

        /// <summary>
        /// add (a) singleton (item).
        /// </summary>
        /// <param name="containerCollection">(using the) container collection.</param>
        /// <param name="registration">(and) registration.</param>
        internal void AddSingleton(IServiceCollection containerCollection, ContainerRegistrationAttribute registration) =>
            containerCollection.AddSingleton(registration.ContractType, registration.ImplementationType);
    }
}
