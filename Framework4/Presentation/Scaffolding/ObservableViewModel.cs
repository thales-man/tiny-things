// -----------------------------------------------------------------------------
// copyright (c) 2019, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------

namespace Tiny.Framework.Scaffolding
{
    /// <summary>
    /// Observable view model.
    /// </summary>
    public abstract class ObservableViewModel :
        ObservableItem
    {
        /// <summary>
        /// The title.
        /// </summary>
        private string _title = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetPropertyValue(ref _title, value);
        }

        /// <summary>
        /// The subtitle.
        /// </summary>
        private string _subtitle = string.Empty;

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        public string Subtitle
        {
            get => _subtitle;
            set => SetPropertyValue(ref _subtitle, value);
        }

        /// <summary>
        /// The icon.
        /// </summary>
        private string _icon = string.Empty;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public string Icon
        {
            get => _icon;
            set => SetPropertyValue(ref _icon, value);
        }

        /// <summary>
        /// is busy.
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetPropertyValue(ref _isBusy, value);
        }

        /// <summary>
        /// has capacity.
        /// </summary>
        private bool _hasCapacity = true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Tiny.Framework.ObservableViewModel"/> has capacity.
        /// </summary>
        /// <value><c>true</c> if has capacity; otherwise, <c>false</c>.</value>
        public bool HasCapacity
        {
            get => _hasCapacity;
            set => SetPropertyValue(ref _hasCapacity, value);
        }

        /// <summary>
        /// The header.
        /// </summary>
        private string _header = string.Empty;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string Header
        {
            get => _header;
            set => SetPropertyValue(ref _header, value);
        }

        /// <summary>
        /// The footer.
        /// </summary>
        private string _footer = string.Empty;

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>The footer.</value>
        public string Footer
        {
            get => _footer;
            set => SetPropertyValue(ref _footer, value);
        }
    }
}