//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Tiny.Framework.Diagnostic
{
    /// <summary>
    /// the diagnostic detail playload.
    /// </summary>
    public sealed class DiagnosticDetail :
        IDiagnosticPayload
    {
        /// <inheritdoc/>
        public DiagnosticLevel Level { get; set; }

        /// <inheritdoc/>
        public string Message { get; set; }
    }
}
