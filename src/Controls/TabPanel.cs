using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Zhai.Famil.Controls
{
    public class TabPanel : TabControl
    {
        static TabPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabPanel), new FrameworkPropertyMetadata(typeof(TabPanel)));
        }

        public static readonly DependencyProperty TabStripKindProperty = DependencyProperty.Register(nameof(TabStripKind), typeof(TabStripKind), typeof(TabPanel), new PropertyMetadata(TabStripKind.Stack));
        public TabStripKind TabStripKind
        {
            get => (TabStripKind)GetValue(TabStripKindProperty);
            set => SetValue(TabStripKindProperty, value);
        }
    }
}
