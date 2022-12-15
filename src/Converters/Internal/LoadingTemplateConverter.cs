using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
            if (value is LoadingTheme theme)
            {
                switch (theme)
                {
                    case LoadingTheme.Circle:
                        return CircleTemplate;
                    case LoadingTheme.Dots:
                        return DotsTemplate;
                    case LoadingTheme.Bars:
                        return BarsTemplate;
                }
            }

            return CircleTemplate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
