using Microsoft.AspNetCore.Cors.Infrastructure;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// cross origin configuration.
    /// </summary>
    internal sealed class CrossOriginConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}CrossOrigins";

        /// <summary>
        /// gets or sets the acccepted origins.
        /// </summary>
        public string[] AcceptedOrigins { get; set; } = { };

        /// <summary>
        /// gets or sets the acccepted headers.
        /// </summary>
        public string[] AcceptedHeaders { get; set; } = { };

        /// <summary>
        /// gets or sets the acccepted methods.
        /// </summary>
        public string[] AcceptedMethods { get; set; } = { };

        /// <summary>
        /// gets or sets a value indicating whether to allow any header.
        /// </summary>
        public bool AllowAnyHeader { get; set; } = true;

        /// <summary>
        /// gets or sets a value indicating whether to allow any method.
        /// </summary>
        public bool AllowAnyMethod { get; set; } = true;

        /// <summary>
        /// gets or sets a value indicating whether to allow any origin.
        /// </summary>
        public bool AllowAnyOrigin { get; set; } = true;

        /// <summary>
        /// gets or sets a value indicating whether to allow credentials.
        /// </summary>
        public bool AllowCredentials { get; set; } = false;

        /// <summary>
        /// get's the default policy.
        /// </summary>
        /// <returns>a default cors policy.</returns>
        internal CorsPolicy GetDefaultPolicy()
        {
            var builder = new CorsPolicyBuilder();

            (AllowAnyHeader && It.HasValues(AcceptedHeaders))
                .ThenDo(() => ColorConsole.Warning($"{AppSettingsKey}: you cannot allow any header and have allowed headers, allow any header will be ignored."));

            (AllowAnyHeader && It.IsEmpty(AcceptedHeaders))
                .ThenDo(() => AddAllowAnyHeader(builder));

            (AllowAnyOrigin && It.HasValues(AcceptedOrigins))
                .ThenDo(() => ColorConsole.Warning($"{AppSettingsKey}: you cannot allow any origin and have accepted origins, allow any origin will be ignored."));

            (AllowAnyOrigin && It.IsEmpty(AcceptedOrigins))
                .ThenDo(() => AddAllowAnyOrigin(builder));

            (AllowAnyMethod && It.HasValues(AcceptedMethods))
                .ThenDo(() => ColorConsole.Warning($"{AppSettingsKey}: you cannot allow any method and have accepted methods, allow any method will be ignored."));

            (AllowAnyMethod && It.IsEmpty(AcceptedMethods))
                .ThenDo(() => AddAllowAnyMethod(builder));

            builder = BuildCredentials(builder);

            It.HasValues(AcceptedOrigins)
                .ThenDo(() => AddAcceptedOrigins(builder));

            It.HasValues(AcceptedHeaders)
                .ThenDo(() => AddAcceptedHeaders(builder));

            It.HasValues(AcceptedMethods)
                .ThenDo(() => AddAcceptedMethods(builder));

            return builder.Build();
        }

        /// <summary>
        /// allow any header.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAllowAnyHeader(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing any headers");
            builder.AllowAnyHeader();
        }

        /// <summary>
        /// allow any origin.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAllowAnyOrigin(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing any origins");
            builder.AllowAnyOrigin();
        }

        /// <summary>
        /// allow any method.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAllowAnyMethod(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing any methods");
            builder.AllowAnyMethod();
        }

        /// <summary>
        /// add accepted origins.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAcceptedOrigins(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing accepted origins: {string.Join(", ", AcceptedOrigins)}");
            builder.WithOrigins(AcceptedOrigins);
        }

        /// <summary>
        /// add accepted Headers.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAcceptedHeaders(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing accepted headers: {string.Join(", ", AcceptedHeaders)}");
            builder.WithHeaders(AcceptedHeaders);
        }

        /// <summary>
        /// add accepted methods.
        /// </summary>
        /// <param name="builder">the builder.</param>
        internal void AddAcceptedMethods(CorsPolicyBuilder builder)
        {
            ColorConsole.Startup($"{AppSettingsKey}: allowing accepted methods: {string.Join(", ", AcceptedMethods)}");
            builder.WithMethods(AcceptedMethods);
        }

        /// <summary>
        /// build credentials.
        /// </summary>
        /// <param name="builder">the builder.</param>
        /// <returns>the builder with or without the credentials policy.</returns>
        internal CorsPolicyBuilder BuildCredentials(CorsPolicyBuilder builder) =>
            AllowCredentials switch
            {
                true => builder.AllowCredentials(),
                false => builder.DisallowCredentials()
            };
    }
}
