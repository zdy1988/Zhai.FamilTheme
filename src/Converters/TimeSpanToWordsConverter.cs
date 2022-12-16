using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class TimeSpanToWordsConverter : ConverterMarkupExtensionBase<TimeSpanToWordsConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var span = new TimeSpan((long)value);
            return TimeSpanToWords(span);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string TimeSpanToWords(TimeSpan aTimeSpan)
        {
            List<string> timeStrings = new List<string>();

            int[] timeParts = new[] { aTimeSpan.Days, aTimeSpan.Hours, aTimeSpan.Minutes, aTimeSpan.Seconds };
            string[] timeUnits = new[] { "天", "时", "分", "秒" };

            for (int i = 0; i < timeParts.Length; i++)
            {
                if (timeParts[i] > 0)
                {
                    timeStrings.Add($"{timeParts[i]} {timeUnits[i]}");
                }
            }

            return timeStrings.Count != 0 ? string.Join(" ", timeStrings.ToArray()) : "0 秒";
        }
    }
}
