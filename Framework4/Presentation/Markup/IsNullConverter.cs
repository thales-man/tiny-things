//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Tiny.Framework.Markup
{
    /// <summary>
    /// a null value converter
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public sealed class IsNullConverter : 
        IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsNullConverter"/> class.
        /// </summary>
        public IsNullConverter()
        {
            IsInverted = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inverted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is inverted; otherwise, <c>false</c>.
        /// </value>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsInverted ? value != null : value == null;
        }

        /// <summary>
        /// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">To be added.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}