// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Utility;
using Tiny.Framework.Web.Shell.Configuration;

namespace Tiny.Framework.Web.Shell.Registration
{
    /// <summary>
    /// service collection extensions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "delegate mechanism")]
    internal static class StartupRegistration
    {
        /// <summary>
        /// add core mvc routine.
        /// </summary>
        public const string AddCoreMvcRoutine = nameof(AddCoreMvc);

        /// <summary>
        /// add cors.
        /// </summary>
        public const string AddCorsRoutine = nameof(AddCors);

        /// <summary>
        /// add controllers as services routine.
        /// </summary>
        public const string AddControllersAsServicesRoutine = nameof(AddControllersAsServices);

        /// <summary>
        /// add authorization.
        /// to use authorization you need to add authorization.
        /// </summary>
        public const string AddAuthorizationRoutine = nameof(AddAuthorization);

        /// <summary>
        /// add the 'slugify' transformation token model convention routine.
        /// </summary>
        public const string AddSlugifyTransformationRoutine = nameof(AddTransformationTokenModelConvention);

        /// <summary>
        /// use forwarded headers.
        /// </summary>
        public const string UseForwardedHeadersRoutine = nameof(UseForwardedHeaders);

        /// <summary>
        /// use cors.
        /// </summary>
        public const string UseCorsRoutine = nameof(UseCors);

        /// <summary>
        /// use https redirection.
        /// </summary>
        public const string UseHttpsRedirectionRoutine = nameof(UseHttpsRedirection);

        /// <summary>
        /// use static files.
        /// this is a vanilla initialisation, for something more specialised; you can write your own.
        /// </summary>
        public const string UseStaticFilesRoutine = nameof(UseStaticFiles);

        /// <summary>
        /// use authorization.
        /// to use authorization you need to add authorization.
        /// </summary>
        public const string UseAuthorizationRoutine = nameof(UseAuthorization);

        /// <summary>
        /// use authentication.
        /// </summary>
        public const string UseAuthenticationRoutine = nameof(UseAuthentication);

        /// <summary>
        /// use routing.
        /// </summary>
        public const string UseRoutingRoutine = nameof(UseRouting);

        /// <summary>
        /// use mappings and if development, exception page.
        /// </summary>
        public const string UseBasicDeploymentRoutine = nameof(UseBasicDeployment);

        /// <summary>
        /// add core mvc services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddCoreMvc(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            ColorConsole.Startup("adding core mvc");
            services.AddMvcCore();
        }

        /// <summary>
        /// add cross origin services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddCors(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<CrossOriginConfiguration>(CrossOriginConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup($"adding cors policy");
                services.AddCors(options => options.AddDefaultPolicy(service.GetDefaultPolicy()));
            }
        }

        /// <summary>
        /// add controllers as services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddControllersAsServices(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            ColorConsole.Startup("adding controllers as services");
            services
                .AddMvcCore()
                .AddControllersAsServices();
        }

        /// <summary>
        /// add authorization.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddAuthorization(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<AuthorizationConfiguration>(AuthorizationConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup("adding authorization");
                services.AddAuthorization();
            }
        }

        /// <summary>
        /// add the 'slugify' transformation token model convention.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddTransformationTokenModelConvention(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<SlugifyTransformerConfiguration>(SlugifyTransformerConfiguration.AppSettingsKey);
            if (service.IsActivated)
            {
                ColorConsole.Startup("adding the 'slugify' token transformation model");
                services
                    .AddMvcCore()
                    .AddMvcOptions(options => options.Conventions.Add(TransformationTokenModelConvention()));
            }
        }

        /// <summary>
        /// use https redirection.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseCors(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<CrossOriginConfiguration>(CrossOriginConfiguration.AppSettingsKey);
            if (service.IsActivated)
            {
                ColorConsole.Startup($"enabling cors with default policy");
                builder.UseCors();
            }
        }

        /// <summary>
        /// use https redirection.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseHttpsRedirection(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<HttpsRedirectionConfiguration>(HttpsRedirectionConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup("enabling https redirection");
                builder.UseHttpsRedirection();
            }
        }

        /// <summary>
        /// use forwarded headers.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseForwardedHeaders(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<ForwardedHeadersConfiguration>(ForwardedHeadersConfiguration.AppSettingsKey);
            if (service.IsActivated)
            {
                ColorConsole.Startup("enabling forwarded headers");
                builder.UseForwardedHeaders(service.BuildOptions());
            }
        }

        /// <summary>
        /// use static files.
        /// this is a vanilla initialisation, for something more specialised; you can write your own.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseStaticFiles(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<StaticFilesConfiguration>(StaticFilesConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup("enabling static files");
                builder.UseStaticFiles();
            }
        }

        /// <summary>
        /// use authorization.
        /// to use authorization you need to add authorization.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseAuthorization(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<AuthorizationConfiguration>(AuthorizationConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup("enabling authorization");
                builder.UseAuthorization();
            }
        }

        /// <summary>
        /// use authentication.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseAuthentication(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            var service = conf.LoadSection<AuthenticationConfiguration>(AuthenticationConfiguration.AppSettingsKey);

            if (service.IsActivated)
            {
                ColorConsole.Startup("enabling authentication");
                builder.UseAuthentication();
            }
        }

        /// <summary>
        /// use basic deployment, developer excepton page, routing and controller mapping..
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseRouting(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            ColorConsole.Startup("enabling routing");
            builder.UseRouting();
        }

        /// <summary>
        /// use basic deployment, developer excepton page, controller mapping..
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseBasicDeployment(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ColorConsole.Startup("enabling developer exception page");
                builder.UseDeveloperExceptionPage();
            }

            ColorConsole.Startup("enabling mapping controller endpoints");
            builder.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        /// <summary>
        /// the 'slugify' transformation token model convention.
        /// </summary>
        /// <returns>an action mode convention.</returns>
        internal static IActionModelConvention TransformationTokenModelConvention() =>
            new RouteTokenTransformerConvention(new SlugifyParameterTransformer());
    }
}