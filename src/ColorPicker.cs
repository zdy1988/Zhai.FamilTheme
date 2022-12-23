using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Zhai.FamilTheme
{
    public class ColorPicker : Control
    {
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(ColorPickerKind), typeof(ColorPicker), new PropertyMetadata(ColorPickerKind.Normal));
        public ColorPickerKind Kind
        {
            get => (ColorPickerKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly DependencyProperty AProperty = DependencyProperty.Register(nameof(A), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double A
        {
            get => (double)GetValue(AProperty);
            set => SetValue(AProperty, value);
        }

        public static readonly DependencyProperty RGB_RProperty = DependencyProperty.Register(nameof(RGB_R), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double RGB_R
        {
            get => (double)GetValue(RGB_RProperty);
            set => SetValue(RGB_RProperty, value);
        }

        public static readonly DependencyProperty RGB_GProperty = DependencyProperty.Register(nameof(RGB_G), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double RGB_G
        {
            get => (double)GetValue(RGB_GProperty);
            set => SetValue(RGB_GProperty, value);
        }

        public static readonly DependencyProperty RGB_BProperty = DependencyProperty.Register(nameof(RGB_B), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double RGB_B
        {
            get => (double)GetValue(RGB_BProperty);
            set => SetValue(RGB_BProperty, value);
        }

        public static readonly DependencyProperty HSV_HProperty = DependencyProperty.Register(nameof(HSV_H), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSV_H
        {
            get => (double)GetValue(HSV_HProperty);
            set => SetValue(HSV_HProperty, value);
        }

        public static readonly DependencyProperty HSV_SProperty = DependencyProperty.Register(nameof(HSV_S), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSV_S
        {
            get => (double)GetValue(HSV_SProperty);
            set => SetValue(HSV_SProperty, value);
        }

        public static readonly DependencyProperty HSV_VProperty = DependencyProperty.Register(nameof(HSV_V), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSV_V
        {
            get => (double)GetValue(HSV_VProperty);
            set => SetValue(HSV_VProperty, value);
        }

        public static readonly DependencyProperty HSL_HProperty = DependencyProperty.Register(nameof(HSL_H), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSL_H
        {
            get => (double)GetValue(HSL_HProperty);
            set => SetValue(HSL_HProperty, value);
        }

        public static readonly DependencyProperty HSL_SProperty = DependencyProperty.Register(nameof(HSL_S), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSL_S
        {
            get => (double)GetValue(HSL_SProperty);
            set => SetValue(HSL_SProperty, value);
        }

        public static readonly DependencyProperty HSL_LProperty = DependencyProperty.Register(nameof(HSL_L), typeof(double), typeof(ColorPicker), new PropertyMetadata(default(double), OnColorValueChanged));
        public double HSL_L
        {
            get => (double)GetValue(HSL_LProperty);
            set => SetValue(HSL_LProperty, value);
        }

        private static readonly DependencyPropertyKey ColorContextPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ColorContext), typeof(ColorContext), typeof(ColorPicker), new PropertyMetadata(new ColorContext(0, 0, 0, 1, 0, 0, 0, 0, 0, 0)));
        public static readonly DependencyProperty ColorContextProperty = ColorContextPropertyKey.DependencyProperty;
        public ColorContext ColorContext
        {
            get => (ColorContext)GetValue(ColorContextProperty);
            private set => SetValue(ColorContextPropertyKey, value);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black, OnSelectedColorChanged));
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private static readonly DependencyPropertyKey SelectedColourPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedColour), typeof(string), typeof(ColorPicker), new PropertyMetadata("#000000"));
        public static readonly DependencyProperty SelectedColourProperty = SelectedColourPropertyKey.DependencyProperty;
        public string SelectedColour
        {
            get => (string)GetValue(SelectedColourProperty);
            private set => SetValue(SelectedColourPropertyKey, value);
        }

        public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register(nameof(SmallChange), typeof(double), typeof(ColorPicker), new PropertyMetadata(1.0));
        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public static readonly DependencyProperty LargeChangeProperty = DependencyProperty.Register(nameof(LargeChange), typeof(double), typeof(ColorPicker), new PropertyMetadata(10.0));
        public double LargeChange
        {
            get => (double)GetValue(LargeChangeProperty);
            set => SetValue(LargeChangeProperty, value);
        }

        public static readonly DependencyProperty IsAlphaEnabledProperty = DependencyProperty.Register(nameof(IsAlphaEnabled), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));
        public bool IsAlphaEnabled
        {
            get => (bool)GetValue(IsAlphaEnabledProperty);
            set => SetValue(IsAlphaEnabledProperty, value);
        }

        public static readonly DependencyProperty IsColorPropertyNameVisibledProperty = DependencyProperty.Register(nameof(IsColorPropertyNameVisibled), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));
        public bool IsColorPropertyNameVisibled
        {
            get => (bool)GetValue(IsColorPropertyNameVisibledProperty);
            set => SetValue(IsColorPropertyNameVisibledProperty, value);
        }

        public static readonly DependencyProperty IsColorPropertyValueVisibledProperty = DependencyProperty.Register(nameof(IsColorPropertyValueVisibled), typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));
        public bool IsColorPropertyValueVisibled
        {
            get => (bool)GetValue(IsColorPropertyValueVisibledProperty);
            set => SetValue(IsColorPropertyValueVisibledProperty, value);
        }

        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(ColorChanged),  RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));
        public event RoutedEventHandler ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }

        public override void EndInit()
        {
            base.EndInit();

            UpdateColorValue(ColorContext);
        }

        bool IgnoreSelectedColorChanged = false;

        private static void OnColorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorPicker colorPicker)
            {
                if (colorPicker.UpdateColorContext(args.Property.Name, args.NewValue))
                {
                    colorPicker.IgnoreSelectedColorChanged = true;
                    var newColor = Color.FromArgb(
                        (byte)Math.Round(colorPicker.ColorContext.A * 255),
                        (byte)Math.Round(colorPicker.ColorContext.RGB_R * 255),
                        (byte)Math.Round(colorPicker.ColorContext.RGB_G * 255),
                        (byte)Math.Round(colorPicker.ColorContext.RGB_B * 255));
                    colorPicker.SelectedColor = newColor;
                    colorPicker.SelectedColour = Convert.ToString(newColor);
                    colorPicker.RaiseEvent(new ColorChangedEventArgs(ColorChangedEvent, newColor));
                    colorPicker.IgnoreSelectedColorChanged = false;
                }
            }
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorPicker colorPicker && !colorPicker.IgnoreSelectedColorChanged)
            {
                var newColor = (Color)args.NewValue;
                var colorContext = new ColorContext();
                colorContext.SetARGB(newColor.A / 255.0, newColor.R / 255.0, newColor.G / 255.0, newColor.B / 255.0);
                colorPicker.UpdateColorValue(colorContext);
            }
        }

        public bool UpdateColorContext(string name, object newValue)
        {
            var context = ColorContext;

            var oldValue = context.GetType().GetProperty(name).GetValue(context);

            if (newValue.Equals(oldValue)) 
                return false;

            var value = (double)newValue;

            switch (name)
            {
                case nameof(A):
                    context.A = value / 255;
                    ColorContext = context;
                    break;
                case nameof(RGB_R):
                    context.RGB_R = value / 255;
                    ColorContext = context;
                    break;
                case nameof(RGB_G):
                    context.RGB_G = value / 255;
                    ColorContext = context;
                    break;
                case nameof(RGB_B):
                    context.RGB_B = value / 255;
                    ColorContext = context;
                    break;
                case nameof(HSV_H):
                    context.HSV_H = value;
                    ColorContext = context;
                    break;
                case nameof(HSV_S):
                    context.HSV_S = value / 100;
                    ColorContext = context;
                    break;
                case nameof(HSV_V):
                    context.HSV_V = value / 100;
                    ColorContext = context;
                    break;
                case nameof(HSL_H):
                    context.HSL_H = value;
                    ColorContext = context;
                    break;
                case nameof(HSL_S):
                    context.HSL_S = value / 100;
                    ColorContext = context;
                    break;
                case nameof(HSL_L):
                    context.HSL_L = value / 100;
                    ColorContext = context;
                    break;
            }

            return true;
        }

        public void UpdateColorValue(ColorContext colorContext)
        {
            if (colorContext.A != A) A = colorContext.A * 255;

            if (colorContext.RGB_R != RGB_R) RGB_R = colorContext.RGB_R * 255;
            if (colorContext.RGB_G != RGB_G) RGB_G = colorContext.RGB_G * 255;
            if (colorContext.RGB_B != RGB_B) RGB_B = colorContext.RGB_B * 255;

            if (colorContext.HSV_H != HSV_H) HSV_H = colorContext.HSV_H;
            if (colorContext.HSV_S != HSV_S) HSV_S = colorContext.HSV_S * 100;
            if (colorContext.HSV_V != HSV_V) HSV_V = colorContext.HSV_V * 100;

            if (colorContext.HSL_H != HSL_H) HSL_H = colorContext.HSL_H;
            if (colorContext.HSL_S != HSL_S) HSL_S = colorContext.HSL_S * 100;
            if (colorContext.HSL_L != HSL_L) HSL_L = colorContext.HSL_L * 100;
        }
    }
}
