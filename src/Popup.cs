using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using Zhai.FamilTheme.Common;
using Zhai.FamilTheme.Windows.Natives;

namespace Zhai.FamilTheme
{
    [DefaultProperty("Content")]
    [ContentProperty("Content")]
    public class Popup : Control
    {
        static Popup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(typeof(Popup)));
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Popup), new PropertyMetadata(false, OnIsPopupOpenPropertyChanged));
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(Popup), new PropertyMetadata(null));
        public UIElement PlacementTarget
        {
            get => (UIElement)GetValue(PlacementTargetProperty);
            set => SetValue(PlacementTargetProperty, value);
        }

        public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Popup), new PropertyMetadata(PlacementMode.Bottom));
        public PlacementMode Placement
        {
            get => (PlacementMode)GetValue(PlacementProperty);
            set => SetValue(PlacementProperty, value);
        }

        public static readonly DependencyProperty PopupAnimationProperty = DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(Popup), new PropertyMetadata(PopupAnimation.None));
        public PopupAnimation PopupAnimation
        {
            get => (PopupAnimation)GetValue(PopupAnimationProperty);
            set => SetValue(PopupAnimationProperty, value);
        }

        public static readonly DependencyProperty AllowsTransparencyProperty = DependencyProperty.Register(nameof(AllowsTransparency), typeof(bool), typeof(Popup), new PropertyMetadata(default));
        public bool AllowsTransparency
        {
            get => (bool)GetValue(AllowsTransparencyProperty);
            set => SetValue(AllowsTransparencyProperty, value);
        }

        public static readonly DependencyProperty StaysOpenProperty = DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(Popup), new PropertyMetadata(default));
        public bool StaysOpen
        {
            get => (bool)GetValue(StaysOpenProperty);
            set => SetValue(StaysOpenProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(Popup), new PropertyMetadata(default));
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(UIElement), typeof(Popup), new PropertyMetadata(null));
        public UIElement Content
        {
            get => (UIElement)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        private static void OnIsPopupOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_Popup", this) is  PopupEx popup)
            {
                popup.Closed -= Popup_Closed;
                popup.Closed += Popup_Closed;
            }
        }

        private void Popup_Closed(object sender, EventArgs e)
        {
            IsOpen = false;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsOpen && !StaysOpen)
            {
                IsOpen = false;
                e.Handled = true;
            }
            else
                base.OnMouseLeftButtonUp(e);
        }
    }
}
