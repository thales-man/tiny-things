//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using Tiny.Framework.Flow;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Diagnostic.Internal
{
    /// <summary>
    /// this acts a both an injectable and static class (bootstrapping process).
    /// this class propagates messages across loggers.
    /// if you want a file logging process add one.
    /// if you want a console logging process, add one...
    /// </summary>
    /// <seealso cref="IPropagateLogging" />
    /// <seealso cref="IHandleMessageMediation{IDiagnosticMessage}" />
    [Shared]
    [Export(typeof(IPropagateLogging))]
    internal class Logger :
        IPropagateLogging,
        IHandleMessageMediation<IDiagnosticMessage>
    {
        /// <summary>
        /// gets or sets the logging listeners.
        /// </summary>
        [ImportMany]
        public IEnumerable<ISupportLogging> Listeners { get; set; }

        /// <summary>
        /// gets or sets the mediator.
        /// </summary>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// add an entry.
        /// </summary>
        /// <param name="logItem">the log entry.</param>
        public static void AddEntry(string logItem)
        {
            Debug.WriteLine(logItem);
        }

        /// <summary>
        /// I propagate logging, add log entry.
        /// </summary>
        /// <param name="newEntry">New entry.</param>
        void IPropagateLogging.AddLogEntry(IDiagnosticPayload newEntry)
        {
            AddEntry(newEntry.Message);

            Listeners
                .ForEach(x => x.AddLogEntry(newEntry));
        }

        /// <summary>
        /// Registers the message handler.
        /// </summary>
        [OnImportsSatisfied]
        public void RegisterMessageHandler()
        {
            Mediator.Register<IDiagnosticMessage>(HandleMessage);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">the message.</param>
        public void HandleMessage(IDiagnosticMessage message)
        {
            ((IPropagateLogging)this).AddLogEntry(message.Payload);
        }
    }
}
