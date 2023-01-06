using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Zhai.Famil.Controls
{
    public static class GridViewExtension
    {
        /// <summary>
        /// 样式
        /// </summary>
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached("Theme", typeof(GridViewTheme), typeof(GridViewExtension), new PropertyMetadata(GridViewTheme.Default));
        public static void SetTheme(DependencyObject element, GridViewTheme value) => element.SetValue(ThemeProperty, value);
        public static GridViewTheme GetTheme(DependencyObject element) => (GridViewTheme)element.GetValue(ThemeProperty);


        /// <summary>
        /// 启用默认排序功能
        /// </summary>
        public static readonly DependencyProperty IsSortEnabledProperty = DependencyProperty.RegisterAttached("IsSortEnabled", typeof(bool), typeof(GridViewExtension), new PropertyMetadata(false));
        public static void SetIsSortEnabled(DependencyObject element, bool value) => element.SetValue(IsSortEnabledProperty, value);
        public static bool GetIsSortEnabled(DependencyObject element) => (bool)element.GetValue(IsSortEnabledProperty);


        /// <summary>
        /// 显示交错行
        /// </summary>
        public static readonly DependencyProperty IsInterlaceRowBackgroundEnabledProperty = DependencyProperty.RegisterAttached("IsInterlaceRowBackgroundEnabled", typeof(bool), typeof(GridViewExtension), new PropertyMetadata(false));
        public static void SetIsInterlaceRowBackgroundEnabled(DependencyObject element, bool value) => element.SetValue(IsInterlaceRowBackgroundEnabledProperty, value);
        public static bool GetIsInterlaceRowBackgroundEnabled(DependencyObject element) => (bool)element.GetValue(IsInterlaceRowBackgroundEnabledProperty);


        /// <summary>
        /// 列头PADDING
        /// </summary>
        public static readonly DependencyProperty HeaderColumnPaddingProperty = DependencyProperty.RegisterAttached("HeaderColumnPadding", typeof(Thickness), typeof(GridViewExtension), new PropertyMetadata(default));
        public static void SetHeaderColumnPadding(DependencyObject element, Thickness value) => element.SetValue(HeaderColumnPaddingProperty, value);
        public static Thickness GetHeaderColumnPadding(DependencyObject element) => (Thickness)element.GetValue(HeaderColumnPaddingProperty);

        /// <summary>
        /// 头部容器PADDING
        /// </summary>
        public static readonly DependencyProperty HeaderContainerPaddingProperty = DependencyProperty.RegisterAttached("HeaderContainerPadding", typeof(Thickness), typeof(GridViewExtension), new PropertyMetadata(default));
        public static void SetHeaderContainerPadding(DependencyObject element, Thickness value) => element.SetValue(HeaderContainerPaddingProperty, value);
        public static Thickness GetHeaderContainerPadding(DependencyObject element) => (Thickness)element.GetValue(HeaderContainerPaddingProperty);
    }
}
