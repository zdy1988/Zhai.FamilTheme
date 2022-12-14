using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.Famil.Converters
{
    public class NullOrEmptyStringToInverseVisibilityConverter : ConverterMarkupExtensionBase<NullOrEmptyStringToInverseVisibilityConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a VisibilityProperty");

            if (value == null || value.ToString() == "")
            {
                return Visibility.Visible;
            }

            return (parameter != null && parameter.Equals("Hidden")) ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

