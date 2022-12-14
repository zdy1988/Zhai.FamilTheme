using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.Famil.Converters
{
    public class TimeSpanToSecondsConverter : ConverterMarkupExtensionBase<TimeSpanToSecondsConverter>, IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is TimeSpan) return ((TimeSpan)value).TotalSeconds;
            if (value is Duration)
                return ((Duration)value).HasTimeSpan ? ((Duration)value).TimeSpan.TotalSeconds : 0d;

            return 0d;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = TimeSpan.FromTicks(System.Convert.ToInt64(TimeSpan.TicksPerSecond * (double)value));

            // Do the conversion from visibility to bool
            if (targetType == typeof(TimeSpan)) return result;
            if (targetType == typeof(Duration)) return new Duration(result);

            return Activator.CreateInstance(targetType);
        }
    }
}
