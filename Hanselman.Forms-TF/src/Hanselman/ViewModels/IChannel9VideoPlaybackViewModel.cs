using System;
using MediaManager;
using Tiny.Framework.Models;

namespace Hanselman.ViewModels
{
    /// <summary>
    /// Channel9 videos view model contract definition.
    /// </summary>
    public interface IChannel9VideoPlaybackViewModel :
        IViewModel,
        IDisposable
    {
        /// <summary>
        /// Sets the manager.
        /// </summary>
        /// <param name="theManager">The manager.</param>
        void SetManager(IMediaManager theManager);

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="theItem">The item.</param>
        void SetModel(IContentFeedItem theItem);
    }
}