using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.FamilTheme
{
    public class Repeater : ListBox
    {
        static Repeater()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Repeater), new FrameworkPropertyMetadata(typeof(Repeater)));
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(RepeaterOrientation), typeof(Repeater), new PropertyMetadata(RepeaterOrientation.Vertical));
        public RepeaterOrientation Orientation
        {
            get => (RepeaterOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
    }
}
