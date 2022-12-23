using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    public class ColorWheeler : Control
    {
        static ColorWheeler()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorWheeler), new FrameworkPropertyMetadata(typeof(ColorWheeler)));
        }

        public static readonly DependencyProperty HueProperty = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorWheeler), new PropertyMetadata(0.0, OnHueChanged));
        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(ColorWheeler), new PropertyMetadata(1.0));
        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorWheeler), new PropertyMetadata(Colors.Black, OnSelectedColorChanged));
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private static readonly DependencyPropertyKey SelectedColourPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedColour), typeof(string), typeof(ColorWheeler), new PropertyMetadata("#000000"));
        public static readonly DependencyProperty SelectedColourProperty = SelectedColourPropertyKey.DependencyProperty;
        public string SelectedColour
        {
            get => (string)GetValue(SelectedColourProperty);
            private set => SetValue(SelectedColourPropertyKey, value);
        }

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorWheeler));
        public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
        {
            add => this.AddHandler(SelectedColorChangedEvent, value);
            remove => this.RemoveHandler(SelectedColorChangedEvent, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_ColorWheelerPath", this) is Path colorWheeler)
            {
                colorWheeler.MouseDown += ColorWheeler_MouseDown;
                colorWheeler.MouseMove += ColorWheeler_MouseMove;
                colorWheeler.MouseUp += ColorWheeler_MouseUp;
                colorWheeler.PreviewMouseWheel += ColorWheeler_PreviewMouseWheel;
            }
        }

        private void ColorWheeler_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            static double Mod(double value, double m)
            {
                return (value % m + m) % m;
            }

            Hue = Mod(Hue + SmallChange * e.Delta / 120, 360);
            e.Handled = true;
        }

        private void ColorWheeler_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void ColorWheeler_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!((UIElement)sender).IsMouseCaptured)
                return;
            Path circle = (Path)sender;
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.ActualWidth, circle.ActualHeight);
        }

        private void ColorWheeler_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            Path circle = (Path)sender;
            Point mousePos = e.GetPosition(circle);
            UpdateValue(mousePos, circle.ActualWidth, circle.ActualHeight);
        }

        private void UpdateValue(Point mousePos, double width, double height)
        {
            double x = mousePos.X / (width * 2);
            double y = mousePos.Y / (height * 2);

            double length = Math.Sqrt(x * x + y * y);
            if (length == 0)
                return;
            double angle = Math.Acos(x / length);
            if (y < 0)
                angle = -angle;
            angle = angle * 360 / (Math.PI * 2) + 180;
            Hue = MathHelper.Clamp(angle, 0, 360);
        }

        bool IgnoreSelectedColorChanged = false;

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorWheeler colorWheeler)
            {
                var hue = (double)args.NewValue;
                colorWheeler.IgnoreSelectedColorChanged = true;
                var colorContext = new ColorContext();
                colorContext.SetAHSV(1, hue, 1, 1);
                var newColor = Color.FromArgb(
                  (byte)Math.Round(colorContext.A * 255),
                  (byte)Math.Round(colorContext.RGB_R * 255),
                  (byte)Math.Round(colorContext.RGB_G * 255),
                  (byte)Math.Round(colorContext.RGB_B * 255));
                colorWheeler.SelectedColor = newColor;
                colorWheeler.SelectedColour = Convert.ToString(newColor);
                colorWheeler.IgnoreSelectedColorChanged = false;
            }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorWheeler colorWheeler && !colorWheeler.IgnoreSelectedColorChanged)
            {
                var newColor = (Color)args.NewValue;
                var colorContext = new ColorContext();
                colorContext.SetARGB(newColor.A / 255.0, newColor.R / 255.0, newColor.G / 255.0, newColor.B / 255.0);
                colorWheeler.Hue = colorContext.HSV_H;

                colorWheeler.RaiseEvent(new RoutedPropertyChangedEventArgs<Color?>((Color?)args.OldValue, (Color?)args.NewValue, SelectedColorChangedEvent));
            }
        }
    }
}
