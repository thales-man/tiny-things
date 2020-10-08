// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Composition;
using System.Threading.Tasks;
using MediaManager;
using Tiny.Framework.Models;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Audio playback provider.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideMediaPlayback))]
    internal sealed class MediaPlaybackProvider :
        IProvideMediaPlayback
    {
        /// <summary>
        /// Play this Item.
        /// </summary>
        /// <param name="thisItem">This item.</param>
        public void Play(IContentFeedItem thisItem)
        {
            Task.Run(async () =>
            {
                CrossMediaManager.Current.ClearQueueOnPlay = true;
                if (It.Has(thisItem))
                {
                    await CrossMediaManager.Current.Play(thisItem.ContentPath);
                }
            });
        }
    }
}
