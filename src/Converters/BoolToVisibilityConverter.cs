﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;

            return (parameter != null && parameter.Equals("Hidden")) ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}