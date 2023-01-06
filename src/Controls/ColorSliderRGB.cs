using System.Windows;
using System.Windows.Media;

namespace Zhai.Famil.Controls
{
    internal class ColorSliderRGB : ColorSliderBase
    {
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(RGB), typeof(ColorSliderRGB), new PropertyMetadata(OnKindChanged));
        public RGB Kind
        {
            get => (RGB)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderRGB slider)
            {
                slider.KindUpdating();
            }
        }

        public ColorSliderRGB() : base()
        {
            KindUpdating();
        }

        private void KindUpdating()
        {
            Minimum = 0;
            Maximum = 255;
        }

        protected override void GenerateBackground()
        {
            var colorStart = GetColorForSelectedArgb(0);
            var colorEnd = GetColorForSelectedArgb(255);
            StartColorBrush = new SolidColorBrush(colorStart);
            EndColorBrush = new SolidColorBrush(colorEnd);
            BackgroundGradientStops = new GradientStopCollection
            {
                new GradientStop(colorStart, 0.0),
                new GradientStop(colorEnd, 1)
            };
        }

        private Color GetColorForSelectedArgb(int value)
        {
            byte a = (byte)(CurrentColorContext.A * 255);
            byte r = (byte)(CurrentColorContext.RGB_R * 255);
            byte g = (byte)(CurrentColorContext.RGB_G * 255);
            byte b = (byte)(CurrentColorContext.RGB_B * 255);

            return Kind switch
            {
                RGB.A => Color.FromArgb((byte)value, r, g, b),
                RGB.R => Color.FromArgb(a, (byte)value, g, b),
                RGB.G => Color.FromArgb(a, r, (byte)value, b),
                RGB.B => Color.FromArgb(a, r, g, (byte)value),
                _ => Color.FromArgb(a, r, g, b),
            };
        }
    }
}
