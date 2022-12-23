using System.Windows;
using System.Windows.Media;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    internal class ColorSliderHSL : ColorSliderBase
    {
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(HSL), typeof(ColorSliderHSL), new PropertyMetadata(OnKindChanged));
        public HSL Kind
        {
            get => (HSL)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderHSL slider)
            {
                slider.KindUpdating();
            }
        }

        public ColorSliderHSL() : base()
        {
            KindUpdating();
        }

        private void KindUpdating()
        {
            Minimum = 0;
            Maximum = Kind == HSL.H ? 360 : 100;
        }

        protected override void GenerateBackground()
        {
            if (Kind == HSL.H)
            {
                var colorStart = GetColorForSelectedArgb(0);
                var colorEnd = GetColorForSelectedArgb(360);

                StartColorBrush = new SolidColorBrush(colorStart);
                EndColorBrush = new SolidColorBrush(colorEnd);
                BackgroundGradientStops = new GradientStopCollection()
                {
                    new GradientStop(colorStart, 0),
                    new GradientStop(GetColorForSelectedArgb(60), 1/6.0),
                    new GradientStop(GetColorForSelectedArgb(120), 2/6.0),
                    new GradientStop(GetColorForSelectedArgb(180), 0.5),
                    new GradientStop(GetColorForSelectedArgb(240), 4/6.0),
                    new GradientStop(GetColorForSelectedArgb(300), 5/6.0),
                    new GradientStop(colorEnd, 1)
                };
            }
            else if (Kind == HSL.L)
            {
                var colorStart = GetColorForSelectedArgb(0);
                var colorEnd = GetColorForSelectedArgb(255);

                StartColorBrush = new SolidColorBrush(colorStart);
                EndColorBrush = new SolidColorBrush(colorEnd);
                BackgroundGradientStops = new GradientStopCollection()
                {
                    new GradientStop(colorStart, 0),
                    new GradientStop(GetColorForSelectedArgb(128), 0.5),
                    new GradientStop(colorEnd, 1)
                };
            }
            else
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
        }

        private Color GetColorForSelectedArgb(int value)
        {
            switch (Kind)
            {
                case HSL.H:
                    {
                        var rgbtuple = ColorSpaceConverter.HslToRgb(value, CurrentColorContext.HSL_S, CurrentColorContext.HSL_L);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case HSL.S:
                    {
                        var rgbtuple = ColorSpaceConverter.HslToRgb(CurrentColorContext.HSL_H, value / 255.0, CurrentColorContext.HSL_L);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case HSL.L:
                    {
                        var rgbtuple = ColorSpaceConverter.HslToRgb(CurrentColorContext.HSL_H, CurrentColorContext.HSL_S, value / 255.0);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                default:
                    return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(CurrentColorContext.RGB_R * 255), (byte)(CurrentColorContext.RGB_G * 255), (byte)(CurrentColorContext.RGB_B * 255));
            }
        }
    }
}
