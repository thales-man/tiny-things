// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Container;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Swashbuckle.Registration
{
    /// <summary>
    /// service collection extensions.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "delegate mechanism")]
    public static class SwaggerRegistration
    {
        /// <summary>
        /// add swagger routine name.
        /// </summary>
        public const string AddSwaggerRoutine = nameof(AddSwagger);

        /// <summary>
        /// use swagger routine name.
        /// </summary>
        public const string UseSwaggerRoutine = nameof(UseSwagger);

        /// <summary>
        /// Initializes static members of the <see cref="SwaggerRegistration"/> class.
        /// </summary>
        static SwaggerRegistration()
        {
            Swagger = Bootstrap.LoadType<SwaggerInfo>(SwaggerInfo.CommonFileName);
        }

        /// <summary>
        /// gets or sets the swagger information.
        /// </summary>
        public static SwaggerInfo Swagger { get; set; }

        /// <summary>
        /// add swagger services.
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void AddSwagger(IServiceCollection services, IConfiguration conf, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ColorConsole.Startup("extending mvc for swagger");
                services
                    .AddMvc();

                ColorConsole.Startup("adding swashbuckle");
                services
                    .AddSwaggerGen(c => c.SwaggerDoc(Swagger.Version, Swagger));
            }
        }

        /// <summary>
        /// use swagger services.
        /// </summary>
        /// <param name="builder">the <see cref="IApplicationBuilder"/> to use services in.</param>
        /// <param name="conf">the <see cref="IConfiguration"/> to get settings from.</param>
        /// <param name="env">the <see cref="IHostEnvironment"/> to run services on.</param>
        public static void UseSwagger(IApplicationBuilder builder, IConfiguration conf, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ColorConsole.Startup("enabling swagger and swashbuckle");
                builder
                    .UseSwagger()
                    .UseSwaggerUI(c => c.SwaggerEndpoint($"{Swagger.Version}/swagger.json", Swagger.MainAssembly));
            }
        }
    }
}