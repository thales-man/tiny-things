//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
namespace Tiny.Framework.Diagnostic
{
    /// <summary>
    /// i propagate logging.
    /// </summary>
    public interface IPropagateLogging
    {
        /// <summary>
        /// add an entry.
        /// </summary>
        /// <param name="newEntry">a new log entry.</param>
        void AddLogEntry(IDiagnosticPayload newEntry);
    }
}
