using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class DataGrid : System.Windows.Controls.DataGrid
    {
        static DataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGrid), new FrameworkPropertyMetadata(typeof(DataGrid)));
        }

        public static readonly DependencyProperty CellPaddingProperty = DependencyProperty.Register(nameof(CellPadding), typeof(Thickness), typeof(DataGrid), new PropertyMetadata(default(Thickness)));
        public Thickness CellPadding
        {
            get => (Thickness)GetValue(CellPaddingProperty);
            set => SetValue(CellPaddingProperty, value);
        }

        public static readonly DependencyProperty HeaderColumnPaddingProperty = DependencyProperty.Register(nameof(HeaderColumnPadding), typeof(Thickness), typeof(DataGrid), new PropertyMetadata(default(Thickness)));
        public Thickness HeaderColumnPadding
        {
            get => (Thickness)GetValue(HeaderColumnPaddingProperty);
            set => SetValue(HeaderColumnPaddingProperty, value);
        }
    }
}
