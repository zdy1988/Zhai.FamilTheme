using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    public class ListView : System.Windows.Controls.ListView
    {
        static ListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListView), new FrameworkPropertyMetadata(typeof(ListView)));
        }

        private static readonly DependencyPropertyKey IsVerticalScrollBarVisibledPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsVerticalScrollBarVisibled), typeof(bool), typeof(ListView), new PropertyMetadata(false));

        public static readonly DependencyProperty IsVerticalScrollBarVisibledProperty = IsVerticalScrollBarVisibledPropertyKey.DependencyProperty;

        public bool IsVerticalScrollBarVisibled
        {
            get => (bool)GetValue(IsVerticalScrollBarVisibledProperty);
        }

        private static readonly DependencyPropertyKey IsHorizontalScrollBarVisibledPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsHorizontalScrollBarVisibled), typeof(bool), typeof(ListView), new PropertyMetadata(false));

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

        public ListView()
        {
            this.Loaded += ListView_Loaded;
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            var view = this.View;
        }
    }
}
