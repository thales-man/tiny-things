using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Shell.Configuration
{
    /// <summary>
    /// cross origin configuration.
    /// </summary>
    internal sealed class ForwardedHeadersConfiguration :
        StartupConfiguration
    {
        /// <summary>
        /// gets the app settings configuration key.
        /// </summary>
        public static string AppSettingsKey => $"{GroupSettingsKey}ForwardedHeaders";

        /// <summary>
        /// gets or sets the forwarded for custom header name.
        /// </summary>
        public string ForwardedForHeaderName { get; set; } = ForwardedHeadersDefaults.XForwardedForHeaderName;

        /// <summary>
        /// gets or sets the forwarded proto header name.
        /// </summary>
        public string ForwardedProtoHeaderName { get; set; } = ForwardedHeadersDefaults.XForwardedProtoHeaderName;

        /// <summary>
        /// gets or sets the forward limit.
        /// </summary>
        public int ForwardLimit { get; set; } = 1;

        /// <summary>
        /// gets or sets the known proxies.
        /// </summary>
        public string[] KnownProxies { get; set; }

        /// <summary>
        /// gets or sets the allowed hosts.
        /// </summary>
        public string[] AllowedHosts { get; set; } = { };

        /// <summary>
        /// build options.
        /// </summary>
        /// <returns>a new forwarded header options.</returns>
        internal ForwardedHeadersOptions BuildOptions()
        {
            (ForwardLimit > 1)
                .ThenDo(() => ColorConsole.Warning($"{AppSettingsKey}: you have increased the forward limit to: {ForwardLimit}"));
            It.IsEmpty(KnownProxies)
                .ThenDo(() =>
                {
                    ColorConsole.Error($"{AppSettingsKey}: know proxies empty");
                    throw new InvalidOperationException("forwarded headers, known proxies not set.");
                });

            var options = new ForwardedHeadersOptions
            {
                AllowedHosts = AllowedHosts,
                ForwardedForHeaderName = ForwardedForHeaderName,
                ForwardedProtoHeaderName = ForwardedProtoHeaderName,
                ForwardLimit = ForwardLimit,
                ForwardedHeaders = BuildForwardHeaders(),
            };

            KnownProxies.ForEach(x => options.KnownProxies.Add(IPAddress.Parse(x)));

            return options;
        }

        /// <summary>
        /// build forward headers.
        /// </summary>
        /// <returns>return the forwarded headers options.</returns>
        internal ForwardedHeaders BuildForwardHeaders() =>
            It.HasValues(AllowedHosts) switch
            {
                true => ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                false => ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
    }
}
