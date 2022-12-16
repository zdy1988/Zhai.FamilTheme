using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace Zhai.FamilTheme.Converters
{
    public class ConverterMarkupExtensionBase<T> : MarkupExtension
        where T : new()
    {
        private T _instance;

        public override object ProvideValue(IServiceProvider serviceProvider) => _instance ??= new T();
    }
}
