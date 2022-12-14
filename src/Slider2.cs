using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class Slider2 : System.Windows.Controls.Slider
    {
        static Slider2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider2), new FrameworkPropertyMetadata(typeof(Slider2)));
        }
    }
}
