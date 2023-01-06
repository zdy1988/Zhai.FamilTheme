using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.Famil.Controls
{
    public class CopyButton : IconButton
    {
        static CopyButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CopyButton), new FrameworkPropertyMetadata(typeof(CopyButton)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(CopyButton), new PropertyMetadata(OnCopyTextChanged));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnCopyTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is CopyButton button && !string.IsNullOrWhiteSpace(button.Text))
            {
                button.Click -= Button_Click;
                button.Click += Button_Click;
            }
        }

        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CopyButton button && !string.IsNullOrWhiteSpace(button.Text))
            {
                Clipboard.SetText(button.Text);
            }
        }
    }
}
