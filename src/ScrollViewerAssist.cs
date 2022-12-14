using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public static class ScrollViewerAssist
    {
        public static readonly DependencyProperty IsAutoHideEnabledProperty = DependencyProperty.RegisterAttached("IsAutoHideEnabled", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(default(bool)));

        public static void SetIsAutoHideEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsAutoHideEnabledProperty, value);
        }

        public static bool GetIsAutoHideEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsAutoHideEnabledProperty);
        }
    }
}
