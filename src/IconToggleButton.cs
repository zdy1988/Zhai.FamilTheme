using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Zhai.FamilTheme
{
    public class IconToggleButton : ToggleButton
    {
        static IconToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), new FrameworkPropertyMetadata(typeof(IconToggleButton)));
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(IconKind), typeof(IconToggleButton), new PropertyMetadata(IconKind.Info));

        public IconKind Icon
        {
            get => (IconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty CheckedIconProperty = DependencyProperty.Register(nameof(CheckedIcon), typeof(IconKind), typeof(IconToggleButton), new PropertyMetadata(IconKind.None));

        public IconKind CheckedIcon
        {
            get => (IconKind)GetValue(CheckedIconProperty);
            set => SetValue(CheckedIconProperty, value);
        }

        public static readonly DependencyProperty CheckedToolTipProperty = DependencyProperty.Register(nameof(CheckedToolTip), typeof(object), typeof(IconToggleButton));

        public object CheckedToolTip
        {
            get => GetValue(CheckedToolTipProperty);
            set => SetValue(CheckedToolTipProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(IconToggleButton), new PropertyMetadata(16.0));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }
}
