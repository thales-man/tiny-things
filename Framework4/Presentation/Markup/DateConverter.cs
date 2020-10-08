//-----------------------------------------------------------------------------
// copyright (c) 2016, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
//-----------------------------------------------------------------------------

//using System;
//using System.Globalization;
//using System.Windows.Data;

//namespace Tiny.Framework.Markup
//{

//  /// <summary>
//  /// a date to string converter
//  /// </summary>
//  [ValueConversion(typeof(DateTime), typeof(String))]
//  public sealed class DateConverter : IValueConverter {

//    /// <summary>
//    /// Converts a value.
//    /// </summary>
//    /// <param name="value">The value produced by the binding source.</param>
//    /// <param name="targetType">The type of the binding target property.</param>
//    /// <param name="parameter">The converter parameter to use.</param>
//    /// <param name="culture">The culture to use in the converter.</param>
//    /// <returns>
//    /// A converted value. If the method returns null, the valid null value is used.
//    /// </returns>
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
//      try {
//        DateTime date = (DateTime)value;
//        string format = parameter == null ? CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern : (string)parameter;
//        return date.ToString(format);
//      } catch {
//        return string.Empty;
//      }
//    }

//    /// <summary>
//    /// Converts a value.
//    /// </summary>
//    /// <param name="value">The value that is produced by the binding target.</param>
//    /// <param name="targetType">The type to convert to.</param>
//    /// <param name="parameter">The converter parameter to use.</param>
//    /// <param name="culture">The culture to use in the converter.</param>
//    /// <returns>
//    /// A converted value. If the method returns null, the valid null value is used.
//    /// </returns>
//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
//      string strValue = value.ToString();
//      DateTime resultDateTime;
//      if (DateTime.TryParse(strValue, out resultDateTime)) {
//        return resultDateTime;
//      }
//      return value;
//    }
//  }
//}
