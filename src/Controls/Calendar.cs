using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zhai.Famil.Controls
{
    public class Calendar : System.Windows.Controls.Calendar
    {
        static Calendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Calendar), new FrameworkPropertyMetadata(typeof(Calendar)));
        }
    }
}
