//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using Tiny.Framework.IO.Internal;

namespace Tiny.Framework.IO
{
    /// <summary>
    /// the asset manager class.
    /// </summary>
    public static class Asset
    {
        /// <summary>
        /// the asset _manager.
        /// </summary>
        private static IManageAssets _manager;

        /// <summary>
        /// gets the asseet manager.
        /// </summary>
        public static IManageAssets Manager =>
            _manager ??= new AssetManager();
    }
}
