using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class VisibilityToInverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                var hiddenVisibility = parameter != null && parameter.Equals("Hidden") ? Visibility.Hidden : Visibility.Collapsed;

                return visibility == Visibility.Visible ? hiddenVisibility : Visibility.Visible;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
