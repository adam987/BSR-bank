using System;
using System.Globalization;
using System.Windows.Data;
using Common.Utils;

namespace Client.ViewModels
{
    /// <summary>
    ///     Decimal to string converter
    /// </summary>
    public class AmountValueConverter : IValueConverter
    {
        /// <summary>
        ///     Convert service string to display format
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture info</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string) value).ToDecimal().ToString("0.00");
        }

        /// <summary>
        ///     Convert display format to service string
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture info</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((decimal) value).ToServiceString();
        }
    }
}