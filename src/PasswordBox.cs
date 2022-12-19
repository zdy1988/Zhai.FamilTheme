using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Zhai.FamilTheme
{
    public class PasswordBox : TextBox
    {
        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(PasswordBox)));
        }

        public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(nameof(PasswordChar), typeof(char), typeof(PasswordBox), new PropertyMetadata('●'));

        public char PasswordChar
        {
            get => (char)GetValue(PasswordCharProperty);
            set => SetValue(PasswordCharProperty, value);
        }

        public static readonly DependencyProperty IsPasswordVisibledProperty = DependencyProperty.Register(nameof(IsPasswordVisibled), typeof(bool), typeof(PasswordBox), new PropertyMetadata(false, OnIsPasswordVisibledChanged));

        public bool IsPasswordVisibled
        {
            get => (bool)GetValue(IsPasswordVisibledProperty);
            set => SetValue(IsPasswordVisibledProperty, value);
        }

        private static void OnIsPasswordVisibledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if (passwordBox.IsPasswordVisibled)
                {
                    Keyboard.Focus(passwordBox);
                    passwordBox.Select(passwordBox.Text.Length, 0);
                }
            }
        }


        private System.Windows.Controls.PasswordBox CurrentPasswordBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CurrentPasswordBox = Template.FindName("PART_PasswordBox", this) as System.Windows.Controls.PasswordBox;

            if (CurrentPasswordBox != null)
            {
                CurrentPasswordBox.PasswordChanged -= CurrentPasswordBox_PasswordChanged;
                CurrentPasswordBox.PasswordChanged += CurrentPasswordBox_PasswordChanged;
                CurrentPasswordBox.Loaded += CurrentPasswordBox_Loaded;
                CurrentPasswordBox.Unloaded -= CurrentPasswordBox_Unloaded;
                CurrentPasswordBox.Unloaded += CurrentPasswordBox_Unloaded;
                TextChanged -= PasswordBox_TextChanged;
                TextChanged += PasswordBox_TextChanged;
            }
        }

        private void CurrentPasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentPasswordBox.Password = this.Text;
        }

        private void CurrentPasswordBox_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Text = CurrentPasswordBox.Password;
        }

        private void CurrentPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!IsPasswordVisibled)
            {
                this.Text = CurrentPasswordBox.Password;
            }
        }

        private void PasswordBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (IsPasswordVisibled)
            {
                CurrentPasswordBox.Password = this.Text;
            }
        }
    }
}
