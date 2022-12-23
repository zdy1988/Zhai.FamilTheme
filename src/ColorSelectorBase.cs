using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    internal abstract class ColorSelectorBase : Control
    {
        private static readonly DependencyPropertyKey OffsetXPropertyKey = DependencyProperty.RegisterReadOnly(nameof(OffsetX), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(0.0));
        public static readonly DependencyProperty OffsetXProperty = OffsetXPropertyKey.DependencyProperty;
        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            private set => SetValue(OffsetXPropertyKey, value);
        }

        private static readonly DependencyPropertyKey OffsetYPropertyKey = DependencyProperty.RegisterReadOnly(nameof(OffsetY), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(0.0));
        public static readonly DependencyProperty OffsetYProperty = OffsetYPropertyKey.DependencyProperty;
        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            private set => SetValue(OffsetYPropertyKey, value);
        }

        public static readonly DependencyProperty RangeXProperty = DependencyProperty.Register(nameof(RangeX), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(100.0));
        public double RangeX
        {
            get => (double)GetValue(RangeXProperty);
            set => SetValue(RangeXProperty, value);
        }

        public static readonly DependencyProperty RangeYProperty = DependencyProperty.Register(nameof(RangeY), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(100.0));
        public double RangeY
        {
            get => (double)GetValue(RangeYProperty);
            set => SetValue(RangeYProperty, value);
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof(X), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(0.0, OnXChanged));
        public double X
        {
            get => (double)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public static readonly DependencyProperty YProperty= DependencyProperty.Register(nameof(Y), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(0.0, OnYChanged));
        public double Y
        {
            get => (double)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public static readonly DependencyProperty HueProperty = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorSelectorBase), new PropertyMetadata(0.0, OnHueChanged));
        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorSelectorBase selector)
            {
                selector.RecalculateGradient();
            }
        }

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorSelectorBase selector)
            {
                selector.OffsetX = selector.ActualWidth * (double)args.NewValue / selector.RangeX;
            }
        }

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorSelectorBase selector)
            {
                selector.OffsetY = selector.ActualHeight * (double)args.NewValue / selector.RangeY;
            }
        }

        private readonly WriteableBitmap writeableBitmap = new WriteableBitmap(32, 32, 96, 96, PixelFormats.Rgb24, null);
        public WriteableBitmap GradientBackground => writeableBitmap;

        protected abstract Tuple<double, double, double> ConvertColorSpace(double x, double y, double z);

        private void RecalculateGradient()
        {
            int w = GradientBackground.PixelWidth;
            int h = GradientBackground.PixelHeight;
            double hue = Hue;
            byte[] pixels = new byte[w * h * 3];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    var rgbtuple = ConvertColorSpace(hue, i / (double)(w - 1), ((h - 1) - j) / (double)(h - 1));
                    double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                    int pos = (j * h + i) * 3;
                    pixels[pos] = (byte)(r * 255);
                    pixels[pos + 1] = (byte)(g * 255);
                    pixels[pos + 2] = (byte)(b * 255);
                }
            }

            GradientBackground.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        public override void EndInit()
        {
            base.EndInit();

            RecalculateGradient();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_SelectorGrid", this) is System.Windows.Controls.Grid selector)
            {
                selector.MouseDown -= Card_MouseDown;
                selector.MouseMove -= Card_MouseMove;
                selector.MouseUp -= Card_MouseUp; ;
                selector.MouseDown += Card_MouseDown;
                selector.MouseMove += Card_MouseMove;
                selector.MouseUp += Card_MouseUp; ;
            }
        }

        private void Card_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void Card_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Grid grid && grid.IsMouseCaptured)
            {
                UpdatePositon(e.GetPosition(this));
            } 
        }

        private void Card_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            UpdatePositon(e.GetPosition(this));
        }

        private void UpdatePositon(Point pos)
        {
            X = MathHelper.Clamp(pos.X / ActualWidth, 0, 1) * RangeX;
            Y = (1 - MathHelper.Clamp(pos.Y / ActualHeight, 0, 1)) * RangeY;

            OffsetX = ActualWidth * X / RangeX;
            OffsetY = ActualHeight * Y / RangeY;
        }
    }
}
