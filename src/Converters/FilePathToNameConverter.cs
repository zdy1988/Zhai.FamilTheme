using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;

namespace Zhai.FamilTheme.Converters
{
    public class FilePathToNameConverter : ConverterMarkupExtensionBase<FilePathToNameConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string localPath && !string.IsNullOrWhiteSpace(localPath))
            {
                if (File.Exists(localPath))
                {
                    return new FileInfo(localPath).Name;
                }
                else if (Directory.Exists(localPath))
                {
                    return new DirectoryInfo(localPath).Name;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
