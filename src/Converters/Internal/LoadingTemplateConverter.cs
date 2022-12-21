using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    internal class LoadingTemplateConverter : IValueConverter
    {
        public object CircleTemplate { get; set; }

        public object DotsTemplate { get; set; }

        public object BarsTemplate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (LoadingTheme)value switch
            {
                LoadingTheme.Circle => CircleTemplate,
                LoadingTheme.Dots => DotsTemplate,
                LoadingTheme.Bars => BarsTemplate,
                _ => CircleTemplate,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
