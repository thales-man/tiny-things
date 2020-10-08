//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tiny.Framework.IO
{
    /// <summary>
    /// i manage assets.
    /// </summary>
    public interface IManageAssets
    {
        /// <summary>
        /// set hte resource callback.
        /// </summary>
        /// <param name="openResource">the callback routine.</param>
        void SetResourceCallback(Func<string, Stream> openResource);

        /// <summary>
        /// gets the asset.
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>
        /// a stream representing the retrieved asset.
        /// </returns>
        Task<Stream> GetAsset(string assetName);

        /// <summary>
        /// gets the text asset.
        /// </summary>
        /// <param name="assetName">Name of the asset file.</param>
        /// <returns>
        /// a string representing the retrieved asset.
        /// </returns>
        Task<string> GetTextAsset(string assetName);
    }
}
