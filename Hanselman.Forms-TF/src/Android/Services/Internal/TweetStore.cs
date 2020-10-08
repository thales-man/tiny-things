using System.Collections.Generic;
using System.Composition;
using Hanselman.Models;

namespace Hanselman.Services.Internal
{
    [Shared]
    [Export(typeof(ITweetStore))]
    internal sealed class TweetStore : ITweetStore
    {
        /// <summary>
        /// Save the specified tweets.
        /// this needs to be replaced with an abstract storage medium
        /// </summary>
        /// <param name="tweets">Tweets.</param>
        public void Save(IReadOnlyCollection<Tweet> tweets)
        {
            // nothing to do, only required for watchOS
        }
    }
}

