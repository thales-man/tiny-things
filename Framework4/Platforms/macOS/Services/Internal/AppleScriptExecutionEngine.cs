//-----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System.Composition;
using System.Threading.Tasks;
using Tiny.Framework.Services.macOS;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Apple script execution engine.
    /// </summary>
    [Shared]
    [Export(typeof(IRunAppleScripts))]
    internal sealed class AppleScriptExecutionEngine :
        IRunAppleScripts
    {
        /// <summary>
        /// Run the specified Script.
        /// </summary>
        /// <returns>The run response.</returns>
        /// <param name="theScript">The script.</param>
        public async Task<string> Run(string theScript) =>
            await Task.Run(() =>
            {
                var appleScript = new Foundation.NSAppleScript(theScript);
                var result = appleScript.ExecuteAndReturnError(out Foundation.NSDictionary error);

                // TODO: review, as this doesn't work very well
                var errorResult = string.Empty;
                error.ForEach(x => errorResult += $"key: {x.Key}, value: {x.Value}\n");

                return It.Has(result.StringValue) ? result.StringValue : errorResult;
            });
    }
}
