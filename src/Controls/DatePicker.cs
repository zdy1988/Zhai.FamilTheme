using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.Famil.Converters;

namespace Zhai.Famil.Controls
{
    public class DatePicker : System.Windows.Controls.DatePicker
    {
        static DatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
        }
    }
}
