using System;
using System.Windows.Data;
using System.Windows;
using Zhai.Famil.Converters;
using System.Windows.Controls;

namespace Zhai.Famil.Controls
{
    public static class TextBoxExtension
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(double), typeof(TextBoxExtension), new PropertyMetadata(default(double)));
        public static void SetCornerRadius(DependencyObject element, double value) => element.SetValue(CornerRadiusProperty, value);
        public static double GetCornerRadius(DependencyObject element) => (double)element.GetValue(CornerRadiusProperty);


        public static readonly DependencyProperty IsPlaceholderVisibledProperty = DependencyProperty.RegisterAttached("IsPlaceholderVisibled", typeof(bool), typeof(TextBoxExtension), new PropertyMetadata(false));
        public static void SetIsPlaceholderVisibled(DependencyObject element, bool value) => element.SetValue(IsPlaceholderVisibledProperty, value);
        public static bool GetIsPlaceholderVisibled(DependencyObject element) => (bool)element.GetValue(IsPlaceholderVisibledProperty);


        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(TextBoxExtension), new PropertyMetadata(null, OnPlaceholderChanged));
        public static void SetPlaceholder(DependencyObject element, string value) => element.SetValue(PlaceholderProperty, value);
        public static string GetPlaceholder(DependencyObject element) => (string)element.GetValue(PlaceholderProperty);


        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                SetIsPlaceholderVisibled(textBox, string.IsNullOrEmpty(textBox.Text));

                BindingPlaceholderVisibled(textBox);
            }
            else if (d is ComboBox comboBox)
            {
                SetIsPlaceholderVisibled(comboBox, string.IsNullOrEmpty(comboBox.Text));

                BindingPlaceholderVisibled(comboBox);
            }
            else if (d is PasswordBox passwordBox)
            {
                SetIsPlaceholderVisibled(passwordBox, string.IsNullOrEmpty(passwordBox.Text));

                BindingPlaceholderVisibled(passwordBox);
            }
            else if (d is DatePicker datePicker)
            {
                SetIsPlaceholderVisibled(datePicker, string.IsNullOrEmpty(datePicker.Text));

                BindingPlaceholderVisibled(datePicker);
            }
        }

        private static void BindingPlaceholderVisibled(Control control)
        {
            var textBinding = new Binding
            {
                Path = new PropertyPath("Text"),
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                Converter = new NullOrEmptyStringToInverseBoolConverter(),
            };

            BindingOperations.SetBinding(control, IsPlaceholderVisibledProperty, textBinding);
        }
    }
}
