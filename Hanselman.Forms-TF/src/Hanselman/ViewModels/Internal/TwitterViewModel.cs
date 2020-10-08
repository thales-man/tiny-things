using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Models;
using Hanselman.Services;
using QuickType;
using Tiny.Framework.Models;
using Tiny.Framework.Scaffolding;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.ViewModels.Internal
{
    /// <summary>
    /// Twitter view model.
    /// </summary>
    [Shared]
    [Export(typeof(ITwitterViewModel))]
    sealed class TwitterViewModel :
        RssFeedViewModel<Tweet, ITweetFeedItem>,
        ITwitterViewModel
    {
        const string feedPath = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=100&screen_name=shanselman&trim_user=0&exclude_replies=1";

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterViewModel"/> class.
        /// </summary>
        public TwitterViewModel()
            : base(feedPath)
        {
            Title = "Twitter";
            Icon = "twitternav.png";
        }

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>The store.</value>
        [Import]
        public ITweetStore Store { get; set; }

        /// <summary>
        /// Gets or sets the launch.
        /// </summary>
        /// <value>The launch.</value>
        [Import]
        public ILaunchTwitter Launch { get; set; }

        /// <summary>
        /// Gets or Sets the I Get Json Token Values service
        /// </summary>
        [Import]
        public IGetJsonTokenValues Tokens { get; set; }

        /// <summary>
        /// Loads the feed item.
        /// </summary>
        /// <param name="thisItem">This item.</param>
        protected override async Task LoadFeedItem(ITweetFeedItem thisItem)
        {
            if (!Launch.OpenItemFor($"{thisItem.StatusID}"))
            {
                await Launcher.OpenAsync($"http://m.twitter.com/shanselman/status/{thisItem.StatusID}");
            }
        }

        /// <summary>
        /// Parses the feed.
        /// </summary>
        /// <returns>The feed.</returns>
        /// <param name="usingResponse">Using response.</param>
        protected override async Task<IEnumerable<ITweetFeedItem>> ParseFeed(string usingResponse) =>
            await Task.Run(() =>
            {
                var tweetsRaw = TweetRaw.FromJson(usingResponse);
                var tweets = tweetsRaw.Select(ExtractTweet);

                Store.Save(tweets.ToList());

                return tweets;
            });

        /// <summary>
        /// Extracts the tweet.
        /// </summary>
        /// <returns>The tweet.</returns>
        /// <param name="raw">Raw.</param>
        public Tweet ExtractTweet(TweetRaw raw) => new Tweet
        {
            StatusID = (ulong)raw.Id,
            Title = raw.User.ScreenName,
            Text = raw.Text,
            CurrentUserRetweet = (ulong)raw.RetweetCount,
            CreatedAt = GetDate(raw.CreatedAt, DateTime.MinValue),
            ImagePath = ExtractTweetImage(raw)
        };

        /// <summary>
        /// Extracts the tweet image.
        /// </summary>
        /// <returns>The tweet image.</returns>
        /// <param name="raw">Raw.</param>
        public string ExtractTweetImage(TweetRaw raw) =>
            raw.RetweetedStatus != null && raw.RetweetedStatus.User != null
                ? raw.RetweetedStatus.User.ProfileImageUrlHttps.OriginalString
                : (raw.User.ScreenName == "shanselman"
                    ? "scott159.png"
                    : raw.User.ProfileImageUrlHttps.OriginalString);

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token.</returns>
        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes("ZTmEODUCChOhLXO4lnUCEbH2I:Y8z2Wouc5ckFb1a0wjUDT9KAI6DUat5tFNdmIkPLl8T4Nyaa2J"));

            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            return await Tokens.GetValueFor("access_token", json);
        }

        /// <summary>
        /// Gets the feed response.
        /// </summary>
        /// <returns>The feed response.</returns>
        /// <param name="usingPath">Using path.</param>
        public override async Task<string> GetFeedResponse(string usingPath)
        {
            var accessToken = await GetAccessToken();

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, usingPath);

            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            var responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            return await responseUserTimeLine.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// The date formats.
        /// </summary>
        public static readonly string[] DateFormats =
        {
            "ddd MMM dd HH:mm:ss %zzzz yyyy",
            "yyyy-MM-dd\\THH:mm:ss\\Z",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd HH:mm"
        };

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <returns>The date.</returns>
        /// <param name="date">Date.</param>
        /// <param name="defaultValue">Default value.</param>
        public static DateTime GetDate(string date, DateTime defaultValue) =>
            string.IsNullOrWhiteSpace(date)
            || !DateTime.TryParseExact(
                    date,
                    DateFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var result)
                        ? defaultValue
                        : result;
    }
}
