using System.Windows;
using System.Windows.Media;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    internal class ColorSliderHSV : ColorSliderBase
    {
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(HSV), typeof(ColorSliderHSV), new PropertyMetadata(OnKindChanged));

        public HSV Kind
        {
            get => (HSV)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderHSV slider)
            {
                slider.KindUpdating();
            }
        }

        public ColorSliderHSV() : base() 
        {
            KindUpdating();
        }

        private void KindUpdating()
        {
            Minimum = 0;
            Maximum = Kind == HSV.H ? 360 : 100;
        }

        protected override void GenerateBackground()
        {
            if (Kind == HSV.H)
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
                case HSV.H:
                    {
                        var rgbtuple = ColorSpaceConverter.HsvToRgb(value, CurrentColorContext.HSV_S, CurrentColorContext.HSV_V);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case HSV.S:
                    {
                        var rgbtuple = ColorSpaceConverter.HsvToRgb(CurrentColorContext.HSV_H, value / 255.0, CurrentColorContext.HSV_V);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                case HSV.V:
                    {
                        var rgbtuple = ColorSpaceConverter.HsvToRgb(CurrentColorContext.HSV_H, CurrentColorContext.HSV_S, value / 255.0);
                        double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                        return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
                    }
                default:
                    return Color.FromArgb((byte)(CurrentColorContext.A * 255), (byte)(CurrentColorContext.RGB_R * 255), (byte)(CurrentColorContext.RGB_G * 255), (byte)(CurrentColorContext.RGB_B * 255));
            }
        }
    }
}
