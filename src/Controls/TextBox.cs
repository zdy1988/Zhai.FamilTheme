using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Zhai.Famil.Converters;

namespace Zhai.Famil.Controls
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }
    }
}
