using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class NumberToSimplifiedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (long.TryParse(value.ToString(), out long num))
            {
                return ConvertNumber(num);
            }
            else
            {
                return "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string ConvertNumber(long num)
        {
            string outNum;
            if (num <= 0)
            {
                return "0";
            }
            else if (num < 1000L)
            {
                outNum = num.ToString();
            }
            else if (num < 10000L)
            {
                outNum = String.Format("{0}K", num / 1000);
            }
            else if (num < 1000L * 10000L)
            {
                outNum = String.Format("{0}W", num / 10000);
            }
            else
            {
                outNum = String.Format("{0}KW", num / (1000 * 10000));
            }
            return outNum;
        }
    }
}
