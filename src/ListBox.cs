using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        private static readonly DependencyPropertyKey IsVerticalScrollBarVisibledPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsVerticalScrollBarVisibled), typeof(bool), typeof(ListBox), new PropertyMetadata(false));

        public static readonly DependencyProperty IsVerticalScrollBarVisibledProperty = IsVerticalScrollBarVisibledPropertyKey.DependencyProperty;

        public bool IsVerticalScrollBarVisibled
        {
            get => (bool)GetValue(IsVerticalScrollBarVisibledProperty);
        }

        private static readonly DependencyPropertyKey IsHorizontalScrollBarVisibledPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsHorizontalScrollBarVisibled), typeof(bool), typeof(ListBox), new PropertyMetadata(false));

        public static readonly DependencyProperty IsHorizontalScrollBarVisibledProperty = IsHorizontalScrollBarVisibledPropertyKey.DependencyProperty;

        public bool IsHorizontalScrollBarVisibled
        {
            get => (bool)GetValue(IsHorizontalScrollBarVisibledProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var scrollViewer = this.FindChild<ScrollViewer>("PART_ScrollViewer");

            if (scrollViewer != null)
            {
                DependencyPropertyDescriptor.FromProperty(System.Windows.Controls.ScrollViewer.ComputedVerticalScrollBarVisibilityProperty, typeof(ScrollViewer))
                    .AddValueChanged(scrollViewer, (o, args) => SetValue(IsVerticalScrollBarVisibledPropertyKey, scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible));

                DependencyPropertyDescriptor.FromProperty(System.Windows.Controls.ScrollViewer.ComputedHorizontalScrollBarVisibilityProperty, typeof(ScrollViewer))
                    .AddValueChanged(scrollViewer, (o, args) => SetValue(IsHorizontalScrollBarVisibledPropertyKey, scrollViewer.ComputedHorizontalScrollBarVisibility == Visibility.Visible));
            }
        }
    }
}
