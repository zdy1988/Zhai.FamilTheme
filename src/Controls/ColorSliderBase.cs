using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Zhai.Famil.Common;
using Zhai.Famil.Common.ExtensionMethods;

namespace Zhai.Famil.Controls
{
    internal abstract class ColorSliderBase : System.Windows.Controls.Slider
    {
        public static readonly DependencyProperty CurrentColorContextProperty = DependencyProperty.Register(nameof(CurrentColorContext), typeof(ColorContext), typeof(ColorSliderBase), new PropertyMetadata(OnColorContextChanged));
        public ColorContext CurrentColorContext
        {
            get => (ColorContext)GetValue(CurrentColorContextProperty);
            set => SetValue(CurrentColorContextProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ColorSliderBase), new PropertyMetadata(0.0, OnCornerRadiusChanged));
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static readonly DependencyPropertyKey LeftCornerRadiusPropertyKey = DependencyProperty.RegisterReadOnly(nameof(LeftCornerRadius), typeof(CornerRadius), typeof(ColorSliderBase), new PropertyMetadata(default));
        public static readonly DependencyProperty LeftCornerRadiusProperty = LeftCornerRadiusPropertyKey.DependencyProperty;
        public CornerRadius LeftCornerRadius
        {
            get => (CornerRadius)GetValue(LeftCornerRadiusProperty);
            protected set => SetValue(LeftCornerRadiusPropertyKey, value);
        }

        private static readonly DependencyPropertyKey RightCornerRadiusPropertyKey = DependencyProperty.RegisterReadOnly(nameof(RightCornerRadius), typeof(CornerRadius), typeof(ColorSliderBase), new PropertyMetadata(default));
        public static readonly DependencyProperty RightCornerRadiusProperty = RightCornerRadiusPropertyKey.DependencyProperty;
        public CornerRadius RightCornerRadius
        {
            get => (CornerRadius)GetValue(RightCornerRadiusProperty);
            protected set => SetValue(RightCornerRadiusPropertyKey, value);
        }

        private static readonly DependencyPropertyKey StartColorBrushPropertyKey = DependencyProperty.RegisterReadOnly(nameof(StartColorBrush), typeof(SolidColorBrush), typeof(ColorSliderBase), new PropertyMetadata(default));
        public static readonly DependencyProperty StartColorBrushProperty = StartColorBrushPropertyKey.DependencyProperty;
        public SolidColorBrush StartColorBrush
        {
            get => (SolidColorBrush)GetValue(StartColorBrushProperty);
            protected set => SetValue(StartColorBrushPropertyKey, value);
        }

        private static readonly DependencyPropertyKey EndColorBrushPropertyKey = DependencyProperty.RegisterReadOnly(nameof(EndColorBrush), typeof(SolidColorBrush), typeof(ColorSliderBase), new PropertyMetadata(default));
        public static readonly DependencyProperty EndColorBrushProperty = EndColorBrushPropertyKey.DependencyProperty;
        public SolidColorBrush EndColorBrush
        {
            get => (SolidColorBrush)GetValue(EndColorBrushProperty);
            protected set => SetValue(EndColorBrushPropertyKey, value);
        }

        private static readonly DependencyPropertyKey BackgroundGradientStopsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(BackgroundGradientStops), typeof(GradientStopCollection), typeof(ColorSliderBase), new PropertyMetadata(default));
        public static readonly DependencyProperty BackgroundGradientStopsProperty = BackgroundGradientStopsPropertyKey.DependencyProperty;
        public GradientStopCollection BackgroundGradientStops
        {
            get => (GradientStopCollection)GetValue(BackgroundGradientStopsProperty);
            protected set => SetValue(BackgroundGradientStopsPropertyKey, value);
        }

        public static readonly DependencyProperty IsTransparetBackgroundEnabledProperty = DependencyProperty.Register(nameof(IsTransparetBackgroundEnabled), typeof(bool), typeof(ColorSliderBase), new PropertyMetadata(true));

        public bool IsTransparetBackgroundEnabled
        {
            get => (bool)GetValue(IsTransparetBackgroundEnabledProperty);
            set => SetValue(IsTransparetBackgroundEnabledProperty, value);
        }

        public ColorSliderBase()
        {
            PreviewMouseWheel += OnPreviewMouseWheel;
        }

        public override void EndInit()
        {
            base.EndInit();

            GenerateBackground();
        }

        protected abstract void GenerateBackground();

        private static void OnColorContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderBase slider)
            {
                slider.GenerateBackground();
            }
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderBase slider)
            {
                double cornerRadius = (double)e.NewValue;

                slider.LeftCornerRadius = new CornerRadius(cornerRadius, 0, 0, cornerRadius);
                slider.RightCornerRadius = new CornerRadius(0, cornerRadius, cornerRadius, 0);
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args)
        {
            Value = (Value + SmallChange * args.Delta / 120).Clip(Minimum, Maximum);
            args.Handled = true;
        }
    }
}
