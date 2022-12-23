using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows;

namespace Zhai.FamilTheme
{
    [RuntimeNameProperty(nameof(Translator))]
    public class Translator : DependencyObject
    {
        public static readonly DependencyProperty ResourceDictionaryProperty = DependencyProperty.RegisterAttached("ResourceDictionary", typeof(string), typeof(Translator), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        public static string GetResourceDictionary(DependencyObject obj) => (string)obj.GetValue(ResourceDictionaryProperty);
        public static void SetResourceDictionary(DependencyObject obj, string value) => obj.SetValue(ResourceDictionaryProperty, value);


        public static string GetTranslationString(string resourceDictionary, string key)
        {
            var dictionary = TranslatorExtension.GetResourceDictionary(resourceDictionary);

            return dictionary != null && !string.IsNullOrWhiteSpace(key) && dictionary.Contains(key) ? (string)dictionary[key] : key;
        }
    }
}
