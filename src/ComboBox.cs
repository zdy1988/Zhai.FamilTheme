using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.FamilTheme.Converters;

namespace Zhai.FamilTheme
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }
    }
}
