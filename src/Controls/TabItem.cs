using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.Famil.Controls
{
    public class TabItem : System.Windows.Controls.TabItem
    {
        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
        }
    }
}
