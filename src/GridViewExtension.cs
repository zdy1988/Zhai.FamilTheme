using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace Zhai.FamilTheme
{
    public static class GridViewExtension
    {
        /// <summary>
        /// 样式
        /// </summary>
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached("Theme", typeof(GridViewTheme), typeof(GridViewExtension), new FrameworkPropertyMetadata(GridViewTheme.Default, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetTheme(DataGrid element, GridViewTheme value) => element.SetValue(ThemeProperty, value);
        public static GridViewTheme GetTheme(DataGrid element) => (GridViewTheme)element.GetValue(ThemeProperty);


        /// <summary>
        /// 启用默认排序功能
        /// </summary>
        public static readonly DependencyProperty IsSortEnabledProperty = DependencyProperty.RegisterAttached("IsSortEnabled", typeof(bool), typeof(GridViewExtension), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsSortEnabled(DataGrid element, bool value) => element.SetValue(IsSortEnabledProperty, value);
        public static bool GetIsSortEnabled(DataGrid element) => (bool)element.GetValue(IsSortEnabledProperty);


        /// <summary>
        /// 显示交错行
        /// </summary>
        public static readonly DependencyProperty IsInterlaceRowBackgroundEnabledProperty = DependencyProperty.RegisterAttached("IsInterlaceRowBackgroundEnabled", typeof(bool), typeof(GridViewExtension), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsInterlaceRowBackgroundEnabled(DataGrid element, bool value) => element.SetValue(IsInterlaceRowBackgroundEnabledProperty, value);
        public static bool GetIsInterlaceRowBackgroundEnabled(DataGrid element) => (bool)element.GetValue(IsInterlaceRowBackgroundEnabledProperty);


        /// <summary>
        /// 列头PADDING
        /// </summary>
        public static readonly DependencyProperty ColumnHeaderPaddingProperty = DependencyProperty.RegisterAttached("ColumnHeaderPadding", typeof(Thickness), typeof(GridViewExtension), new FrameworkPropertyMetadata(new Thickness(13, 12, 0, 12), FrameworkPropertyMetadataOptions.Inherits));
        public static void SetColumnHeaderPadding(DependencyObject element, Thickness value) => element.SetValue(ColumnHeaderPaddingProperty, value);
        public static Thickness GetColumnHeaderPadding(DataGrid element) => (Thickness)element.GetValue(ColumnHeaderPaddingProperty);


        /// <summary>
        /// 行PADDING
        /// </summary>
        public static readonly DependencyProperty ItemPaddingProperty = DependencyProperty.RegisterAttached("ItemPadding", typeof(Thickness), typeof(GridViewExtension), new FrameworkPropertyMetadata(new Thickness(8, 8, 8, 8), FrameworkPropertyMetadataOptions.Inherits));
        public static void SetItemPadding(DependencyObject element, Thickness value) => element.SetValue(ItemPaddingProperty, value);
        public static Thickness GetItemPadding(DependencyObject element) => (Thickness)element.GetValue(ItemPaddingProperty);
    }
}
