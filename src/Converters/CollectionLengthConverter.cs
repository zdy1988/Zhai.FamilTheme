﻿using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class CollectionLengthConverter : ConverterMarkupExtensionBase<CollectionLengthConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection collection)
            {
                return collection.Count;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
