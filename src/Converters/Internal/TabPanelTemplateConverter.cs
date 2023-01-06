using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Zhai.Famil.Controls;

namespace Zhai.Famil.Converters
{
    internal class TabPanelTemplateConverter : IValueConverter
    {
        public object StackTemplate { get; set; }

        public object UniformTemplate { get; set; }

        public object ScrollTemplate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TabStripKind)value switch
            {
                TabStripKind.Stack => StackTemplate,
                TabStripKind.Uniform => UniformTemplate,
                TabStripKind.Scroll => ScrollTemplate,
                _ => StackTemplate,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
