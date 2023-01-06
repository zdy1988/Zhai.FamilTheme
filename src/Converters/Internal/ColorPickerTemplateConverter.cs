using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Zhai.Famil.Controls;

namespace Zhai.Famil.Converters
{
    internal class ColorPickerTemplateConverter : IValueConverter
    {
        public object NormalTemplate { get; set; }

        public object RGBTemplate { get; set; }

        public object HSVTemplate { get; set; }

        public object HSLTemplate { get; set; }

        public object HSV2Template { get; set; }

        public object HSL2Template { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ColorPickerKind)value switch
            {
                ColorPickerKind.RGB => RGBTemplate,
                ColorPickerKind.HSV => HSVTemplate,
                ColorPickerKind.HSV2 => HSV2Template,
                ColorPickerKind.HSL => HSLTemplate,
                ColorPickerKind.HSL2 => HSL2Template,
                _ => NormalTemplate,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
