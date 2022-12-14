using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Zhai.FamilTheme.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String && !string.IsNullOrEmpty(base64String))
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);

                byte[] bytes = System.Convert.FromBase64String(base64String);

                MemoryStream ms = new MemoryStream(bytes);
                ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                ImageSource source = (ImageSource)imageSourceConverter.ConvertFrom(ms);

                return source;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
