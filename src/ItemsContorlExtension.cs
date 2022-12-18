using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace Zhai.FamilTheme
{
    public static class ItemsContorlExtension
    {
        /// <summary>
        /// 行容器PADDING
        /// </summary>
        public static readonly DependencyProperty ItemContainerPaddingProperty = DependencyProperty.RegisterAttached("ItemContainerPadding", typeof(Thickness), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerPadding(DependencyObject element, Thickness value) => element.SetValue(ItemContainerPaddingProperty, value);
        public static Thickness GetItemContainerPadding(DependencyObject element) => (Thickness)element.GetValue(ItemContainerPaddingProperty);

        /// <summary>
        /// 行容器边框颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerBorderBrushProperty = DependencyProperty.RegisterAttached("ItemContainerBorderBrush", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerBorderBrush(DependencyObject element, Brush value) => element.SetValue(ItemContainerBorderBrushProperty, value);
        public static Brush GetItemContainerBorderBrush(DependencyObject element) => (Brush)element.GetValue(ItemContainerBorderBrushProperty);

        /// <summary>
        /// 行容器背景颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerBackgroundBrushProperty = DependencyProperty.RegisterAttached("ItemContainerBackgroundBrush", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerBackgroundBrush(DependencyObject element, Brush value) => element.SetValue(ItemContainerBackgroundBrushProperty, value);
        public static Brush GetItemContainerBackgroundBrush(DependencyObject element) => (Brush)element.GetValue(ItemContainerBackgroundBrushProperty);

        /// <summary>
        /// Mouseover行容器背景颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerHoveredBackgroundBrushProperty = DependencyProperty.RegisterAttached("ItemContainerHoveredBackgroundBrush", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerHoveredBackgroundBrush(DependencyObject element, Brush value) => element.SetValue(ItemContainerHoveredBackgroundBrushProperty, value);
        public static Brush GetItemContainerHoveredBackgroundBrush(DependencyObject element) => (Brush)element.GetValue(ItemContainerHoveredBackgroundBrushProperty);

        /// <summary>
        /// 选中时行容器背景颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerActivedBackgroundBrushProperty = DependencyProperty.RegisterAttached("ItemContainerActivedBackgroundBrush", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerActivedBackgroundBrush(DependencyObject element, Brush value) => element.SetValue(ItemContainerActivedBackgroundBrushProperty, value);
        public static Brush GetItemContainerActivedBackgroundBrush(DependencyObject element) => (Brush)element.GetValue(ItemContainerActivedBackgroundBrushProperty);
    }
}
