using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Zhai.FamilTheme
{
    public static class GridViewAssist
    {
        public static readonly DependencyProperty IsInterlaceRowBackgroundEnabledProperty = DependencyProperty.RegisterAttached("IsInterlaceRowBackgroundEnabled", typeof(bool), typeof(GridViewAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsInterlaceRowBackgroundEnabled(DataGrid element, bool value)
        {
            element.SetValue(IsInterlaceRowBackgroundEnabledProperty, value);
        }
        public static bool GetIsInterlaceRowBackgroundEnabled(DataGrid element)
        { 
            return (bool)element.GetValue(IsInterlaceRowBackgroundEnabledProperty);
        }


        public static readonly DependencyProperty ColumnHeaderPaddingProperty = DependencyProperty.RegisterAttached("ColumnHeaderPadding", typeof(Thickness), typeof(GridViewAssist), new FrameworkPropertyMetadata(new Thickness(13, 12, 0, 12), FrameworkPropertyMetadataOptions.Inherits));
        public static Thickness GetColumnHeaderPadding(DataGrid element) => (Thickness)element.GetValue(ColumnHeaderPaddingProperty);
        public static void SetColumnHeaderPadding(DependencyObject element, Thickness value) => element.SetValue(ColumnHeaderPaddingProperty, value);


        public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.RegisterAttached("ItemPadding", typeof(Thickness), typeof(GridViewAssist), new FrameworkPropertyMetadata(new Thickness(8, 8, 8, 8), FrameworkPropertyMetadataOptions.Inherits));
        public static void SetItemPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(ItemPaddingProperty, value);
        }
        public static Thickness GetItemPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(ItemPaddingProperty);
        }
    }
}
