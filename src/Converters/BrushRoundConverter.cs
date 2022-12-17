﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Zhai.FamilTheme.Converters
{
    public class BrushRoundConverter : IValueConverter
    {
        public Brush HighValue { get; set; } = Brushes.White;

        public Brush LowValue { get; set; } = Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not SolidColorBrush solidColorBrush) return null;

            var color = solidColorBrush.Color;

            var brightness = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;

            return brightness < 123 ? LowValue : HighValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
