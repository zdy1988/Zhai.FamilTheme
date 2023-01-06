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
    public class NumericalComparisonToBoolConverter : ConverterMarkupExtensionBase<NumericalComparisonToBoolConverter>, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter as string;

            if (string.IsNullOrEmpty(param) && values.Length == 2)
                throw new ArgumentException();

            if (double.TryParse(values[0].ToString(), out double n1) && double.TryParse(values[1].ToString(), out double n2))
            {
                bool Comparie()
                {
                    return param switch
                    {
                        "=" => n1 == n2,
                        ">" => n1 > n2,
                        "<" => n1 < n2,
                        ">=" => n1 >= n2,
                        "<=" => n1 <= n2,
                        _ => false
                    };
                }

                return Comparie();
            }

            throw new ArgumentException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
