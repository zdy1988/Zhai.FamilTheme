using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zhai.Famil.Controls
{
    public static class CalendarExtension
    {
        public static readonly DependencyProperty SelectionBackgroundProperty = DependencyProperty.RegisterAttached("SelectionBackground", typeof(Brush), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetSelectionBackground(DependencyObject element, Brush value) => element.SetValue(SelectionBackgroundProperty, value);
        public static Brush GetSelectionBackground(DependencyObject element) => (Brush)element.GetValue(SelectionBackgroundProperty);


        public static readonly DependencyProperty SelectionForegroundProperty = DependencyProperty.RegisterAttached("SelectionForeground", typeof(Brush), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetSelectionForeground(DependencyObject element, Brush value) => element.SetValue(SelectionForegroundProperty, value);
        public static Brush GetSelectionForeground(DependencyObject element) => (Brush)element.GetValue(SelectionForegroundProperty);


        public static readonly DependencyProperty IsHeaderVisibledProperty = DependencyProperty.RegisterAttached("IsHeaderVisibled", typeof(bool), typeof(CalendarExtension), new PropertyMetadata(true));
        public static void SetIsHeaderVisibled(DependencyObject element, bool value) => element.SetValue(IsHeaderVisibledProperty, value);
        public static bool GetIsHeaderVisibled(DependencyObject element) => (bool)element.GetValue(IsHeaderVisibledProperty);


        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetHeaderBackground(DependencyObject element, Brush value) => element.SetValue(HeaderBackgroundProperty, value);
        public static Brush GetHeaderBackground(DependencyObject element) => (Brush)element.GetValue(HeaderBackgroundProperty);


        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetHeaderForeground(DependencyObject element, Brush value) => element.SetValue(HeaderForegroundProperty, value);
        public static Brush GetHeaderForeground(DependencyObject element) => (Brush)element.GetValue(HeaderForegroundProperty);


        public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(CalendarExtension), new PropertyMetadata(default(Orientation)));
        public static void SetOrientation(DependencyObject element, Orientation value) => element.SetValue(OrientationProperty, value);
        public static Orientation GetOrientation(DependencyObject element) => (Orientation)element.GetValue(OrientationProperty);


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetCornerRadius(DependencyObject element, double value) => element.SetValue(CornerRadiusProperty, value);
        public static double GetCornerRadius(DependencyObject element) => (double)element.GetValue(CornerRadiusProperty);


        public static readonly DependencyProperty ButtonCornerRadiusProperty = DependencyProperty.RegisterAttached("ButtonCornerRadius", typeof(double), typeof(CalendarExtension), new PropertyMetadata(default));
        public static void SetButtonCornerRadius(DependencyObject element, double value) => element.SetValue(ButtonCornerRadiusProperty, value);
        public static double GetButtonCornerRadius(DependencyObject element) => (double)element.GetValue(ButtonCornerRadiusProperty);


        public static readonly DependencyProperty IsShadowVisibledProperty = DependencyProperty.RegisterAttached("IsShadowVisibled", typeof(bool), typeof(CalendarExtension), new PropertyMetadata(false));
        public static void SetIsShadowVisibled(DependencyObject element, bool value) => element.SetValue(IsShadowVisibledProperty, value);
        public static bool GetIsShadowVisibled(DependencyObject element) => (bool)element.GetValue(IsShadowVisibledProperty);
    }
}
