using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
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
            CommandBindings.Add(new CommandBinding(GridView.SortCommand, GridViewSortCommandHandler));
        }

        private void GridViewSortCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.View is GridView view && view != null 
                && e.OriginalSource is GridViewColumnHeader header
                && header.FindChild<Icon>() is Icon directionIcon
                && header.Column.DisplayMemberBinding is Binding binding)
            {
                var direction = directionIcon.Tag == null || (ListSortDirection)directionIcon.Tag == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                var sortBy = binding.Path.Path;

                ICollectionView dataView = CollectionViewSource.GetDefaultView(this.ItemsSource);

                dataView.SortDescriptions.Clear();
                dataView.SortDescriptions.Add(new SortDescription(sortBy, direction));
                dataView.Refresh();

                directionIcon.Tag = direction;
            }
        }
    }
}
