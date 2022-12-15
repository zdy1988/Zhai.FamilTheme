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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ScrollViewerExtension.SyncScrollBarVisibled(this);
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
