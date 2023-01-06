using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace Zhai.Famil.Converters
{
    public class Base64ToImageSourceConverter : ConverterMarkupExtensionBase<Base64ToImageSourceConverter>, IValueConverter
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
