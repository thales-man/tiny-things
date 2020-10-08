// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tiny.Framework.Registration;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Web.Factories
{
    /// <summary>
    /// the context logging factory (implementation).
    /// </summary>
    internal sealed class LoggingContextFactory :
        ICreateLoggingContexts,
        ISupportServiceRegistration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingContextFactory"/> class.
        /// </summary>
        /// <param name="loggerProvider">the logger provider.</param>
        public LoggingContextFactory(ILoggerProvider loggerProvider)
        {
            It.IsNull(loggerProvider)
                .AsGuard<ArgumentNullException>();

            Factory = loggerProvider;
        }

        /// <summary>
        /// gets the logger provider.
        /// </summary>
        internal ILoggerProvider Factory { get; }

        /// <inheritdoc/>
        public ILoggingContextScope BeginLoggingFor(string scope)
        {
            var logger = Factory.CreateLogger(scope);
            return new LoggingContextScope(logger, scope);
        }

        /// <summary>
        /// the context factory logging scope.
        /// the log routine on the logger is an extension method and therefore cannot be tested.
        /// This class implements the dispose pattern as prescribed by Sonar.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public class LoggingContextScope :
            ILoggingContextScope
        {
            /// <summary>
            /// the logging scope.
            /// </summary>
            private readonly string _scope;

            /// <summary>
            /// the scoped logger.
            /// </summary>
            private readonly ILogger _logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="LoggingContextScope"/> class.
            /// </summary>
            /// <param name="logger">the logger.</param>
            /// <param name="scope">the scope.</param>
            public LoggingContextScope(ILogger logger, string scope)
            {
                _logger = logger;
                _scope = scope;
                _logger.Log(LogLevel.Information, $"commencing logging for: '{_scope}'", null);
            }

            /// <inheritdoc/>
            public async Task Log(string message) =>
                await Task.Run(() => _logger.Log(LogLevel.Information, message, null));

            /// <inheritdoc/>
            public async Task Log(Exception exception) =>
                await Task.Run(() => _logger.Log(LogLevel.Error, exception, null));

            /// <inheritdoc/>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Dispose...
            /// </summary>
            /// <param name="disposing">If disposing.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _logger.Log(LogLevel.Information, $"completed logging for: '{_scope}'", null);
                }
            }
        }
    }
}