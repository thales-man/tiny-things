//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.Flow;

namespace Tiny.Framework.Diagnostic.Internal
{
    /// <summary>
    /// the diagnostic message.
    /// </summary>
    internal sealed class DiagnosticMessage :
        MediationMessage<IDiagnosticPayload>,
        IDiagnosticMessage
    {
    }
}
