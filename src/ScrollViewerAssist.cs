using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.FamilTheme
{
    public static class ScrollViewerAssist
    {
        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.RegisterAttached("IsInertiaEnabled", typeof(bool), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsInertiaEnabled(DependencyObject element, bool value) => element.SetValue(IsInertiaEnabledProperty, value);
        public static bool GetIsInertiaEnabled(DependencyObject element) => (bool)element.GetValue(IsInertiaEnabledProperty);


        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached("IsPenetrating", typeof(bool), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsPenetrating(DependencyObject element, bool value) => element.SetValue(IsPenetratingProperty, value);
        public static bool GetIsPenetrating(DependencyObject element) => (bool)element.GetValue(IsPenetratingProperty);


        /// <summary>
        /// 是否响应鼠标滚轮操作
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelEnabledProperty = DependencyProperty.RegisterAttached("IsMouseWheelEnabled", typeof(bool), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsMouseWheelEnabled(DependencyObject element, bool value) => element.SetValue(IsMouseWheelEnabledProperty, value);
        public static bool GetIsMouseWheelEnabled(DependencyObject element) => (bool)element.GetValue(IsMouseWheelEnabledProperty);


        /// <summary>
        /// 是否响应鼠标滚轮操作
        /// </summary>
        public static readonly DependencyProperty MouseWheelOrientationProperty = DependencyProperty.RegisterAttached("MouseWheelOrientation", typeof(Orientation), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetMouseWheelOrientation(DependencyObject element, Orientation value) => element.SetValue(MouseWheelOrientationProperty, value);
        public static Orientation GetMouseWheelOrientation(DependencyObject element) => (Orientation)element.GetValue(MouseWheelOrientationProperty);


        /// <summary>
        /// 是否自动隐藏滚动条
        /// </summary>
        public static readonly DependencyProperty IsAutoHideEnabledProperty = DependencyProperty.RegisterAttached("IsAutoHideEnabled", typeof(bool), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsAutoHideEnabled(DependencyObject element, bool value) => element.SetValue(IsAutoHideEnabledProperty, value);
        public static bool GetIsAutoHideEnabled(DependencyObject element) => (bool)element.GetValue(IsAutoHideEnabledProperty);


        /// <summary>
        /// 是否覆盖在内容之上
        /// </summary>
        public static readonly DependencyProperty IsOverlayedProperty = DependencyProperty.RegisterAttached("IsOverlayed", typeof(bool), typeof(ScrollViewerAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        public static void SetIsOverlayed(DependencyObject element, bool value) => element.SetValue(IsOverlayedProperty, value);
        public static bool GetIsOverlayed(DependencyObject element) => (bool)element.GetValue(IsOverlayedProperty);
    }
}
