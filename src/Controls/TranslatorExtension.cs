using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;
using System.Xaml;
using System.Collections;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Zhai.Famil.Controls
{
    [RuntimeNameProperty(nameof(TranslatorExtension))]
    public class TranslatorExtension : MarkupExtension
    {
        private static readonly Dictionary<string, ResourceDictionary> ResourceDictionaries = new();

        public string Key { get; private set; }

        public TranslatorExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            object targetObject = (serviceProvider as IProvideValueTarget)?.TargetObject;

            ResourceDictionary dictionary = GetResourceDictionary(targetObject);

            if (dictionary == null && serviceProvider is IRootObjectProvider rootObjectProvider)
            {
                dictionary = GetResourceDictionary(rootObjectProvider.RootObject);
            }

            if (dictionary == null && targetObject is FrameworkElement frameworkElement)
            {
                dictionary = GetResourceDictionary(frameworkElement.TemplatedParent);
            }

            return dictionary != null && Key != null && dictionary.Contains(Key) ? dictionary[Key] : Key;
        }

        internal static ResourceDictionary GetResourceDictionary(object target)
        {
            if (target is DependencyObject dependencyObject)
            {
                object localValue = dependencyObject.ReadLocalValue(Translator.ResourceDictionaryProperty);

                if (localValue != null && localValue != DependencyProperty.UnsetValue)
                {
                    return GetResourceDictionary(localValue.ToString());
                }

                localValue = dependencyObject.GetValue(Translator.ResourceDictionaryProperty);

                if (localValue != null && localValue != DependencyProperty.UnsetValue)
                {
                    return GetResourceDictionary(localValue.ToString());
                }
            }

            return GetResourceDictionary("Zhai.Famil.Languages");
        }

        private static ResourceDictionary GetResourceDictionary(string resourcePartten)
        {
            string cacheKey = $"{resourcePartten}/{Thread.CurrentThread.CurrentCulture}";

            if (!ResourceDictionaries.TryGetValue(cacheKey, out ResourceDictionary dictionary))
            {
                static (string baseName, string stringName) SplitName(string name)
                {
                    int index = name.LastIndexOf('.');

                    return (name[..index], name[(index + 1)..]);
                }

                var (referencedAssembly, subfolder) = SplitName(resourcePartten);

                bool TryGetResourceDictionary(string culture, out ResourceDictionary resourceDictionary)
                {
                    try
                    {
                        resourceDictionary = new ResourceDictionary { Source = new Uri($"pack://application:,,,/{referencedAssembly};component/{subfolder}/{culture}.xaml") };
                        return true;
                    }
                    catch
                    {
                        resourceDictionary = null;
                        return false;
                    }
                }

                var cultureName = Thread.CurrentThread.CurrentCulture.Name;

                if (!TryGetResourceDictionary(cultureName, out dictionary))
                {
                    if (!(cultureName.Contains("-") && TryGetResourceDictionary(cultureName.Split("-")[0], out dictionary)))
                    {
                        TryGetResourceDictionary("zh", out dictionary);
                    }
                }

                ResourceDictionaries.Add(cacheKey, dictionary);
            }

            return dictionary;
        }
    }
}
