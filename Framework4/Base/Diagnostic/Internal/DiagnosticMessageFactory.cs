//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Composition;
using Tiny.Framework.Registration;

namespace Tiny.Framework.Diagnostic.Internal
{
    /// <summary>
    /// Diagnostic message factory.
    /// </summary>
    [Shared]
    [Export(typeof(ICreateDiagnosticMessages))]
    internal sealed class DiagnosticMessageFactory :
        ICreateDiagnosticMessages,
        ISupportServiceRegistration
    {
        /// <inheritdoc/>
        public IDiagnosticMessage Create(string message, DiagnosticLevel level = DiagnosticLevel.Trace) =>
            new DiagnosticMessage { Payload = new DiagnosticDetail { Level = level, Message = message } };
    }
}
