// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Net.Http;
using System.Threading.Tasks;
using Tiny.Framework.Models;
using Tiny.Framework.Utility;
using Xamarin.Forms;

namespace Tiny.Framework.Scaffolding
{
    /// <summary>
    /// RSS Feed view model.
    /// </summary>
    public abstract class RssFeedViewModel<TFeedItem, TFeedContract> :
        ObservableViewModel
        where TFeedItem : class, TFeedContract
        where TFeedContract : class, IRssFeedItem
    {
        private readonly string _feedPath;

        protected RssFeedViewModel(string feedPath)
        {
            _feedPath = feedPath;
        }

        /// <summary>
        /// gets the feed items
        /// </summary>
        public ObservableCollection<TFeedContract> FeedItems { get; } = new ObservableCollection<TFeedContract>();

        /// <summary>
        /// The selected feed item.
        /// </summary>
        TFeedItem _selectedFeedItem;

        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
        public TFeedItem SelectedFeedItem
        {
            get => _selectedFeedItem;
            set
            {
                SetPropertyValue(ref _selectedFeedItem, value);

                if (It.Has(_selectedFeedItem))
                {
                    LoadFeedItem(value);
                }

                _selectedFeedItem = null;
            }
        }

        /// <summary>
        /// Loads the feed item.
        /// </summary>
        /// <param name="thisItem">This item.</param>
        protected abstract Task LoadFeedItem(TFeedContract thisItem);

        /// <summary>
        /// The load items command.
        /// </summary>
        Command _loadItemsCommand;

        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command LoadItemsCommand =>
            _loadItemsCommand
                ?? (_loadItemsCommand = new Command(async () => await Load()));

        [OnImportsSatisfied]
        public void Compose() => Task.Run(Load);

        /// <summary>
        /// Loads the feed.
        /// </summary>
        /// <returns>The feed.</returns>
        public async Task Load()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                await SafeActions.Try(RefreshFeed, x => ReportProblem(x));

                IsBusy = false;
            }
        }

        /// <summary>
        /// Reports the problem.
        /// </summary>
        /// <returns>The problem.</returns>
        /// <param name="thisProblem">This problem.</param>
        public async Task ReportProblem(Exception thisProblem) =>
            await Application.Current.MainPage.DisplayAlert("Error", $"Unable to load feed.\n{thisProblem.Message}", "OK");

        /// <summary>
        /// Refreshs the feed.
        /// </summary>
        /// <returns>The feed.</returns>
        public async Task RefreshFeed()
        {
            var response = await GetFeedResponse(_feedPath);
            var items = await ParseFeed(response);
            await LoadResults(items);
        }

        /// <summary>
        /// Gets the feed response.
        /// </summary>
        /// <returns>The feed response.</returns>
        /// <param name="usingPath">Using path.</param>
        public virtual async Task<string> GetFeedResponse(string usingPath)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(usingPath);
            }
        }

        /// <summary>
        /// Loads the results.
        /// </summary>
        /// <returns>The results.</returns>
        /// <param name="theseResults">These results.</param>
        public async Task LoadResults(IEnumerable<TFeedContract> theseResults)
        {
            await Task.Run(() =>
            {
                FeedItems.Clear();
                theseResults.ForEach(FeedItems.Add);
            });
        }

        /// <summary>
        /// Parses the feed.
        /// </summary>
        /// <returns>The feed.</returns>
        /// <param name="usingResponse">Using response.</param>
        protected abstract Task<IEnumerable<TFeedContract>> ParseFeed(string usingResponse);
    }
}

