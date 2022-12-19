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
        public static readonly DependencyProperty ItemContainerBackgroundProperty = DependencyProperty.RegisterAttached("ItemContainerBackground", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerBackground(DependencyObject element, Brush value) => element.SetValue(ItemContainerBackgroundProperty, value);
        public static Brush GetItemContainerBackground(DependencyObject element) => (Brush)element.GetValue(ItemContainerBackgroundProperty);

        /// <summary>
        /// Mouseover行容器背景颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerHoveredBackgroundProperty = DependencyProperty.RegisterAttached("ItemContainerHoveredBackground", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerHoveredBackground(DependencyObject element, Brush value) => element.SetValue(ItemContainerHoveredBackgroundProperty, value);
        public static Brush GetItemContainerHoveredBackground(DependencyObject element) => (Brush)element.GetValue(ItemContainerHoveredBackgroundProperty);

        /// <summary>
        /// 选中时行容器背景颜色
        /// </summary>
        public static readonly DependencyProperty ItemContainerActivedBackgroundProperty = DependencyProperty.RegisterAttached("ItemContainerActivedBackground", typeof(Brush), typeof(ItemsContorlExtension), new PropertyMetadata(default));
        public static void SetItemContainerActivedBackground(DependencyObject element, Brush value) => element.SetValue(ItemContainerActivedBackgroundProperty, value);
        public static Brush GetItemContainerActivedBackground(DependencyObject element) => (Brush)element.GetValue(ItemContainerActivedBackgroundProperty);
    }
}
