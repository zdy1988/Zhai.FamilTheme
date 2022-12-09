using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ZhaiFamilyContorls
{
    public class ScrollViewer2 : ScrollViewer
    {
        static ScrollViewer2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer2), new FrameworkPropertyMetadata(typeof(ScrollViewer2)));
        }
    }
}
