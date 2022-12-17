using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Zhai.FamilTheme
{
    public static class MenuExtension
    {
        public static readonly DependencyProperty SubmenuCornerRadiusProperty = DependencyProperty.RegisterAttached("SubmenuCornerRadius", typeof(double), typeof(MenuExtension), new PropertyMetadata(default));
        public static void SetSubmenuCornerRadius(MenuBase element, double value) => element.SetValue(SubmenuCornerRadiusProperty, value);
        public static double GetSubmenuCornerRadius(MenuBase element) => (double)element.GetValue(SubmenuCornerRadiusProperty);


        public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.RegisterAttached("ItemPadding", typeof(Thickness), typeof(MenuExtension), new PropertyMetadata(default));
        public static void SetItemPadding(MenuBase element, Thickness value) => element.SetValue(ItemPaddingProperty, value);
        public static Thickness GetItemPadding(MenuBase element) => (Thickness)element.GetValue(ItemPaddingProperty);


        public static readonly DependencyProperty TopLevelItemPaddingProperty = DependencyProperty.RegisterAttached("TopLevelItemPadding", typeof(Thickness), typeof(MenuExtension), new PropertyMetadata(default));
        public static void SetTopLevelItemPadding(MenuBase element, Thickness value) => element.SetValue(TopLevelItemPaddingProperty, value);
        public static Thickness GetTopLevelItemPadding(MenuBase element) => (Thickness)element.GetValue(TopLevelItemPaddingProperty);
    }
}
