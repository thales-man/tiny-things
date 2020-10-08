// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tiny.Framework.Services.Internal
{
    /// <summary>
    /// Navigation context provider.
    /// </summary>
    [Shared]
    [Export(typeof(IProvideNavigationContext))]
    [Export(typeof(IManageNavigationContext))]
    internal sealed class NavigationContextProvider :
        IProvideNavigationContext,
        IManageNavigationContext
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public INavigation Context { get; set; }

        /// <summary>
        /// Gets the modal stack.
        /// </summary>
        /// <value>The modal stack.</value>
        public IReadOnlyList<Page> ModalStack => Context.ModalStack;

        /// <summary>
        /// Gets the navigation stack.
        /// </summary>
        /// <value>The navigation stack.</value>
        public IReadOnlyList<Page> NavigationStack => Context.NavigationStack;

        /// <summary>
        /// Inserts the page before.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="before">Before.</param>
        public void InsertPageBefore(Page page, Page before)
        {
            Context.InsertPageBefore(page, before);
        }

        /// <summary>
        /// Pops the async.
        /// </summary>
        /// <returns>The async task and the popped page.</returns>
        public async Task<Page> PopAsync()
        {
            return await Context.PopAsync(false);
        }

        /// <summary>
        /// Pops the async.
        /// </summary>
        /// <returns>The async task and the popped page.</returns>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public async Task<Page> PopAsync(bool animated)
        {
            return await Context.PopAsync(animated);
        }

        /// <summary>
        /// Pops the modal async.
        /// </summary>
        /// <returns>The async task and the popped page.</returns>
        public async Task<Page> PopModalAsync()
        {
            return await Context.PopModalAsync(false);
        }

        /// <summary>
        /// Pops the modal async.
        /// </summary>
        /// <returns>The async task and the popped page.</returns>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public async Task<Page> PopModalAsync(bool animated)
        {
            return await Context.PopModalAsync(animated);
        }

        /// <summary>
        /// Pops to root async.
        /// </summary>
        /// <returns>The async task.</returns>
        public async Task PopToRootAsync()
        {
            await Context.PopToRootAsync(false);
        }

        /// <summary>
        /// Pops to root async.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public async Task PopToRootAsync(bool animated)
        {
            await Context.PopToRootAsync(animated);
        }

        /// <summary>
        /// Pushs the page async.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="page">Page.</param>
        public async Task PushAsync(Page page)
        {
            await Context.PushAsync(page, false);
        }

        /// <summary>
        /// Pushs the page async.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public async Task PushAsync(Page page, bool animated)
        {
            await Context.PushAsync(page, animated);
        }

        /// <summary>
        /// Pushs the modal page async.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="page">Page.</param>
        public async Task PushModalAsync(Page page)
        {
            await Context.PushModalAsync(page, false);
        }

        /// <summary>
        /// Pushs the modal page async.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public async Task PushModalAsync(Page page, bool animated)
        {
            await Context.PushModalAsync(page, animated);
        }

        /// <summary>
        /// Removes the page.
        /// </summary>
        /// <param name="page">Page.</param>
        public void RemovePage(Page page)
        {
            Context.RemovePage(page);
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="thisContext">This context.</param>
        public void SetContext(INavigation thisContext)
        {
            Context = thisContext;
        }
    }
}
