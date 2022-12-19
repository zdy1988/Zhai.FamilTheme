using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.FamilTheme.Converters;

namespace Zhai.FamilTheme
{
    public class DatePicker: System.Windows.Controls.DatePicker
    {
        static DatePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
        }
    }
}
