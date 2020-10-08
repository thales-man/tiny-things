// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using Tiny.Framework.Flow;
using Tiny.Framework.Factories;
using Tiny.Framework.Models;
using Tiny.Framework.Views;
using Tiny.Framework.Utility;

namespace Tiny.Framework.Scaffolding
{
    /// <summary>
    /// Menu page view model.
    /// </summary>
    public abstract class MenuPageViewModel<TMenuItem> :
        ObservableViewModel,
        IMenuPageViewModel
            where TMenuItem : struct, IComparable
    {
        /// <summary>
        /// Gets or sets the mediator.
        /// </summary>
        [Import]
        public IManageMessageMediation Mediator { get; set; }

        /// <summary>
        /// Gets or sets the message factory.
        /// </summary>
        [Import]
        public ICreateChangeDetailPageRequestMessages Message { get; set; }

        /// <summary>
        /// Gets or sets the source (menu) items.
        /// </summary>
        [ImportMany]
        public IEnumerable<IMenuItemPage<TMenuItem>> SourceItems { get; set; }

        /// <summary>
        /// Gets or sets the <typeparamref name="TMenuItem"/> (pages).
        /// </summary>
        public ObservableCollection<IMenuItemPage<TMenuItem>> MenuItems { get; set; } = new ObservableCollection<IMenuItemPage<TMenuItem>>();

        /// <summary>
        /// Initialise this instance.
        /// </summary>
        [OnImportsSatisfied]
        public void Initialise()
        {
            SourceItems.OrderBy(x => x.MenuID).ForEach(MenuItems.Add);
        }

        /// <summary>
        /// The selected page.
        /// </summary>
        private IMenuItemPage<TMenuItem> _selectedPage;

        /// <summary>
        /// Gets or sets the selected page and publishes a page change request.
        /// </summary>
        public IMenuItemPage<TMenuItem> SelectedPage
        {
            get => _selectedPage;
            set
            {
                SetPropertyValue(ref _selectedPage, value);
                if (It.Has(_selectedPage))
                {
                    Mediator.Publish(Message.Create(_selectedPage));
                    SelectedPage = null;
                }
            }
        }
    }
}

