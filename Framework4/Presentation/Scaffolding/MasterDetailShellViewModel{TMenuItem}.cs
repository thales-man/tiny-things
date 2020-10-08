// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Composition;
using Tiny.Framework.Flow;
using Tiny.Framework.Models;
using Tiny.Framework.Services;
using Tiny.Framework.Views;
using Tiny.Framework.Utility;
using Xamarin.Forms;

namespace Tiny.Framework.Scaffolding
{
    /// <summary>
    /// Master detail shell view model.
    /// </summary>
    public abstract class MasterDetailShellViewModel<TMenuItem> :
        ObservableViewModel,
        IMasterDetailShellViewModel,
        IHandleMessageMediation<IChangeDetailRequestMessage>
            where TMenuItem : struct, IComparable
    {
        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>The pages.</value>
        IDictionary<TMenuItem, NavigationPage> Pages { get; set; } = new Dictionary<TMenuItem, NavigationPage>();

        /// <summary>
        /// Gets or sets the mediator.
        /// </summary>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Gets or sets the menu page.
        /// </summary>
        [Import]
        public IRootMenuPage MenuPage { get; set; }

        /// <summary>
        /// Gets or sets the root (detail) page.
        /// </summary>
        [Import]
        public IRootDetailPage<TMenuItem> RootPage { get; set; }

        /// <summary>
        /// Gets or sets the context manager.
        /// </summary>
        [Import]
        public IManageNavigationContext ContextManager { get; set; }

        /// <summary>
        /// Gets or sets the shell.
        /// </summary>
        public IMasterDetailShellView Shell { get; set; }

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Initialise()
        {
            RegisterMessageHandler();

            Master = MenuPage as Page;
            HandlePageChange(RootPage);
        }

        /// <summary>
        /// Gets or sets the master.
        /// </summary>
        public Page Master { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        public Page Detail { get; set; }

        /// <summary>
        /// Registers the message handler(s).
        /// </summary>
        public void RegisterMessageHandler()
        {
            Mediator.Register<IChangeDetailRequestMessage>(HandleMessage, true);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void HandleMessage(IChangeDetailRequestMessage message)
        {
            HandlePageChange(message.Payload as IMenuItemPage<TMenuItem>);
        }

        /// <summary>
        /// Handles the page change.
        /// </summary>
        /// <param name="candidate">Candidate.</param>
        public void HandlePageChange(IMenuItemPage<TMenuItem> candidate)
        {
            It.IsNull(candidate)
                .AsGuard<ArgumentNullException>(nameof(candidate));

            if (!Pages.ContainsKey(candidate.MenuID))
            {
                Pages.Add(candidate.MenuID, new NavigationPage(candidate as Page));
            }

            var newNavigationDetail = Pages[candidate.MenuID];

            if (It.Has(newNavigationDetail))
            {
                Detail = newNavigationDetail;
                ContextManager.SetContext(newNavigationDetail.Navigation);
                Shell?.NavigateTo(newNavigationDetail);
            }
        }

        /// <summary>
        /// Sets the shell.
        /// </summary>
        /// <param name="shell">Shell.</param>
        public void SetShell(IMasterDetailShellView shell)
        {
            Shell = shell;
        }
    }
}

