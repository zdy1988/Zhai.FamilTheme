using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    public class GridView : System.Windows.Controls.GridView
    {
        protected override object DefaultStyleKey => typeof(ListView);

        public static RoutedCommand SortCommand = new RoutedCommand();
    }
}
