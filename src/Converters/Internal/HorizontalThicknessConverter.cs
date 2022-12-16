using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Zhai.FamilTheme.Converters
{
    internal class HorizontalThicknessConverter : ConverterMarkupExtensionBase<HorizontalThicknessConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                return new Thickness(thickness.Left, 0, thickness.Right, 0);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
