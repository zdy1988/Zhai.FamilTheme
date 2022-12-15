using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Zhai.FamilTheme
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ScrollViewerExtension.SyncScrollBarVisibled(this);
        }
    }
}
