using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class SubStringConverter : ConverterMarkupExtensionBase<SubStringConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not null && int.TryParse(parameter.ToString(), out int startIndex))
            {
                var str = value.ToString();

                if (str.Length > 0 && str.Length > startIndex)
                {
                    return value.ToString().Remove(startIndex);
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
