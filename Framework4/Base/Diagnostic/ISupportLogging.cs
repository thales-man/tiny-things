//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Diagnostic
{
    /// <summary>
    /// a logger facade (primarily for the debug writeline).
    /// </summary>
    public interface ISupportLogging
    {
        /// <summary>
        /// add an entry.
        /// </summary>
        /// <param name="newEntry">new entry.</param>
        void AddLogEntry(IDiagnosticPayload newEntry);
    }
}
