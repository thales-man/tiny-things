//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Composition;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiny.Framework.Registration;

namespace Tiny.Framework.IO.Internal
{
    /// <summary>
    /// the asset manager.
    /// </summary>
    /// <seealso cref="IManageAssets" />
    [Shared]
    [Export(typeof(IManageAssets))]
    internal sealed class AssetManager :
        IManageAssets,
        ISupportServiceRegistration
    {
        private Func<string, Stream> _openResource;

        /// <summary>
        /// gets a value indicating whether has resource (callback) function.
        /// </summary>
        public bool HasResourceFunction =>
            _openResource != null;

        /// <summary>
        /// gets the resource function.
        /// </summary>
        public Func<string, Stream> GetResourceFunction =>
            _openResource ?? File.OpenRead;

        /// <summary>
        /// Sets the resource callback routine.
        /// </summary>
        /// <param name="openResource">the open resource (callback).</param>
        public void SetResourceCallback(Func<string, Stream> openResource) =>
            _openResource = openResource;

        /// <summary>
        /// gets the asset.
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>
        /// a stream representing the retrieved asset.
        /// </returns>
        public async Task<Stream> GetAsset(string assetName) =>
            await Task.Run(() =>
            {
                var path = GetAssetFile(assetName);
                return GetResourceFunction(path);
            });

        /// <summary>
        /// gets the text asset.
        /// </summary>
        /// <param name="assetName">Name of the asset file.</param>
        /// <returns>
        /// a string representing the retrieved asset.
        /// </returns>
        public async Task<string> GetTextAsset(string assetName)
        {
            using (var stream = await GetAsset(assetName))
            {
                return AsString(stream);
            }
        }

        /// <summary>
        /// gets the asset file name.
        /// </summary>
        /// <param name="usingAssetName">Name of the using asset.</param>
        /// <returns>the storage file.</returns>
        public string GetAssetFile(string usingAssetName)
        {
            return HasResourceFunction
                ? usingAssetName
                : RunPathOptions(usingAssetName);
        }

        /// <summary>
        /// Runs the path options.
        /// </summary>
        /// <returns>the path options.</returns>
        /// <param name="usingAssetName">Using asset name.</param>
        public string RunPathOptions(string usingAssetName)
        {
            var storage = AppDomain.CurrentDomain.BaseDirectory
                ?? Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            // raw root path search (IOS)
            var candidatePath = Path.Combine(storage, usingAssetName);
            if (File.Exists(candidatePath))
            {
                return candidatePath;
            }

            // 'traditional' asset path search (desktop, UWP <= even though it's dead)
            candidatePath = Path.Combine(storage, "Assets", usingAssetName);
            if (File.Exists(candidatePath))
            {
                return candidatePath;
            }

            // traverse, mac bundle search
            storage = storage.TrimEnd(Path.DirectorySeparatorChar);
            storage = storage.Substring(0, storage.LastIndexOf(Path.DirectorySeparatorChar));
            candidatePath = Path.Combine(storage, "Resources/Assets", usingAssetName);
            if (File.Exists(candidatePath))
            {
                return candidatePath;
            }

            candidatePath = Path.Combine(storage, "Resources", usingAssetName);
            if (File.Exists(candidatePath))
            {
                return candidatePath;
            }

            // NOTE: there may be more options we need to consider
            return usingAssetName;
        }

        /// <summary>
        /// As string.
        /// </summary>
        /// <returns>the string.</returns>
        /// <param name="stream">Stream.</param>
        public string AsString(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// As stream.
        /// </summary>
        /// <returns>the stream.</returns>
        /// <param name="thisString">This string.</param>
        public Stream AsStream(string thisString) =>
            new MemoryStream(Encoding.UTF8.GetBytes(thisString));
    }
}
