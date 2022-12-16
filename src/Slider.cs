using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class Slider : System.Windows.Controls.Slider
    {
        static Slider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Slider), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
