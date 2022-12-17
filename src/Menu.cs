using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class Menu : System.Windows.Controls.Menu
    {
        static Menu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Menu), new FrameworkPropertyMetadata(typeof(Menu)));
        }
    }
}
