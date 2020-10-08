// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
namespace Tiny.Framework.Models
{
    /// <summary>
    /// the 'tweet' feed item contract
    /// </summary>
    public interface ITweetFeedItem :
        IRssFeedItem
    {
        /// <summary>
        /// the status id of the tweet
        /// </summary>
        ulong StatusID { get; }
    }
}
