using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Zhai.FamilTheme
{
    public class SwitchToggleButton : ToggleButton
    {
        static SwitchToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchToggleButton), new FrameworkPropertyMetadata(typeof(SwitchToggleButton)));
        }

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(nameof(Size), typeof(double), typeof(SwitchToggleButton), new PropertyMetadata(30.0));

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
    }
}
