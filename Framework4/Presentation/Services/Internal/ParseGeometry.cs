// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Helpers;

namespace Tiny.Framework.Services.Internal
{

    /// <summary>
    /// Parse (geometries for path operations).
    /// </summary>
    internal static class ParseGeometry
    {
        /// <summary>
        /// For path operations.
        /// </summary>
        /// <param name="fromDataPath">From data path.</param>
        /// <param name="usingVisitor">Using visitor.</param>
        internal static void ForPathOperations(string fromDataPath, IVisitPathGenerators usingVisitor)
        {
            var context = GeometryPathOperations.Create(usingVisitor);
            var actionProvider = GeometryParserActionProvider.Create();
            var walker = GeometryPathDataWalker.Create(fromDataPath);
            var responseState = GeometryState.Empty();

            walker.MoveToStart();

            do
            {
                var cmd = (GeometryCommand)walker.CurrentToken;
                var requestState = GeometryState.CreateRequest(cmd, responseState);
                var parseAction = actionProvider.Fetch(cmd);

                responseState = parseAction(requestState, context, walker);

            } while (walker.ReadToken());
        }
    }
}
