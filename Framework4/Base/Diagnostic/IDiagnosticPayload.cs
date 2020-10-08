//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

namespace Tiny.Framework.Diagnostic
{
    /// <summary>
    /// i diagnostic payload.
    /// </summary>
    public interface IDiagnosticPayload
    {
        /// <summary>
        /// gets the diagnositc level.
        /// </summary>
        DiagnosticLevel Level { get; }

        /// <summary>
        /// gets the message.
        /// </summary>
        string Message { get; }
    }
}
