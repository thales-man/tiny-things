using Tiny.Framework.Models;
using Tiny.Framework.Views;

namespace Hanselman.ViewModels
{
    /// <summary>
    /// the video playback page contract
    /// </summary>
    public interface IProvideVideoPlayback :
        IDisplayFeedItems<IContentFeedItem>
    {
    }

    /// <summary>
    /// the audio playback page contract
    /// </summary>
    public interface IProvideAudioPlayback :
        IDisplayFeedItems<IContentFeedItem>
    {
    }
}
