﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class MultiBooleanToVisibilityConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            CultureInfo culture)
        {
            var param = parameter as string;

            if (string.IsNullOrEmpty(param))
                throw new ArgumentException();

            var visible = param switch
            {
                "OR" => values.OfType<bool>().Aggregate(false, (current, value) => current || value),
                "AND" => values.OfType<bool>().Aggregate(true, (current, value) => current && value),
                _ => values.OfType<bool>().Aggregate(true, (current, value) => current && value),
            };

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private MultiBooleanToVisibilityConverter _instance;

        public override object ProvideValue(IServiceProvider serviceProvider)
            => _instance ?? (_instance = new MultiBooleanToVisibilityConverter());
    }
}