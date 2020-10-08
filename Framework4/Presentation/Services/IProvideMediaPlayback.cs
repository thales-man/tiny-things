// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Models;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// I provide audio playback contract definition.
    /// </summary>
    public interface IProvideMediaPlayback
    {
        /// <summary>
        /// Play this Item.
        /// </summary>
        /// <param name="thisItem">This item.</param>
        void Play(IContentFeedItem thisItem);
    }
}
