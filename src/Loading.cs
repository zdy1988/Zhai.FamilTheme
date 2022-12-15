using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.FamilTheme
{
    public class Loading : Control
    {
        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
        }

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(nameof(Theme), typeof(LoadingTheme), typeof(CheckBox), new PropertyMetadata(LoadingTheme.Circle));

        public LoadingTheme Theme
        {
            get => (LoadingTheme)GetValue(ThemeProperty);
            set => SetValue(ThemeProperty, value);
        }
    }
}
