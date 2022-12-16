using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.FamilTheme.Converters;

namespace Zhai.FamilTheme
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(TextBox), new PropertyMetadata(default(double)));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty IsPlaceholderVisibledProperty = DependencyProperty.Register(nameof(IsPlaceholderVisibled), typeof(bool), typeof(TextBox), new PropertyMetadata(false));

        public bool IsPlaceholderVisibled
        {
            get => (bool)GetValue(IsPlaceholderVisibledProperty);
            set => SetValue(IsPlaceholderVisibledProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox), new PropertyMetadata(null, OnPlaceholderChanged));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.IsPlaceholderVisibled = String.IsNullOrEmpty(textBox.Text);

                var textBinding = new Binding
                {
                    Path = new PropertyPath(nameof(Text)),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new NullOrEmptyStringToInverseBoolConverter(),
                };

                BindingOperations.SetBinding(textBox, IsPlaceholderVisibledProperty, textBinding);
            }
        }
    }
}
