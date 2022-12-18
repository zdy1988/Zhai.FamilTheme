﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.FamilTheme.Converters;

namespace Zhai.FamilTheme
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ComboBox), new PropertyMetadata(default(double)));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty IsPlaceholderVisibledProperty = DependencyProperty.Register(nameof(IsPlaceholderVisibled), typeof(bool), typeof(ComboBox), new PropertyMetadata(false));

        public bool IsPlaceholderVisibled
        {
            get => (bool)GetValue(IsPlaceholderVisibledProperty);
            set => SetValue(IsPlaceholderVisibledProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ComboBox), new PropertyMetadata(null, OnPlaceholderChanged));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ComboBox comboBox)
            {
                comboBox.IsPlaceholderVisibled = String.IsNullOrEmpty(comboBox.Text);

                var textBinding = new Binding
                {
                    Path = new PropertyPath(nameof(Text)),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new NullOrEmptyStringToInverseBoolConverter(),
                };

                BindingOperations.SetBinding(comboBox, IsPlaceholderVisibledProperty, textBinding);
            }
        }
    }
}
