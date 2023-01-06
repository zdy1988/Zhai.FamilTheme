using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace Zhai.Famil.Controls
{
    internal class ShadowExtension
    {
        public static readonly DependencyProperty CacheModeProperty = DependencyProperty.RegisterAttached("CacheMode", typeof(CacheMode), typeof(ShadowExtension), new FrameworkPropertyMetadata(new BitmapCache { EnableClearType = true, SnapsToDevicePixels = true }, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetCacheMode(DependencyObject element, CacheMode value) => element.SetValue(CacheModeProperty, value);
        public static CacheMode GetCacheMode(DependencyObject element) => (CacheMode)element.GetValue(CacheModeProperty);
    }
}
