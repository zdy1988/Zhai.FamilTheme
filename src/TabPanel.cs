using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.FamilyContorls
{
    public class TabPanel : TabControl
    {
        static TabPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabPanel), new FrameworkPropertyMetadata(typeof(TabPanel)));
        }
    }
}
