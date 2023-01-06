using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zhai.Famil.Common;

namespace Zhai.Famil.Controls
{
    public class GridView : System.Windows.Controls.GridView
    {
        protected override object DefaultStyleKey => typeof(ListView);

        public static RoutedCommand SortCommand = new RoutedCommand();
    }
}
