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

        private readonly List<Icon> headerSortIcons = new List<Icon>();

        private void GridViewSortCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.View is GridView view && view != null 
                && e.OriginalSource is GridViewColumnHeader header
                && header.Column.DisplayMemberBinding is Binding binding
                && header.Parent is GridViewHeaderRowPresenter headerRowPresenter
                && header.FindChild<Icon>() is Icon directionIcon)
            {
                var direction = directionIcon.Tag == null || (ListSortDirection)directionIcon.Tag == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                var sortBy = binding.Path.Path;

                ICollectionView dataView = CollectionViewSource.GetDefaultView(this.ItemsSource);

                dataView.SortDescriptions.Clear();
                dataView.SortDescriptions.Add(new SortDescription(sortBy, direction));
                dataView.Refresh();

                if (!headerSortIcons.Contains(directionIcon))
                {
                    headerSortIcons.Add(directionIcon);
                }

                foreach (Icon sortIcon in headerSortIcons)
                {
                    sortIcon.Tag = null;
                }

                foreach (GridViewColumn column in headerRowPresenter.Columns)
                {
                    if (column == header.Column)
                    {
                        directionIcon.Tag = direction;
                        directionIcon.Kind = direction == ListSortDirection.Ascending ? IconKind.SortUp : IconKind.SortDown;
                        break;
                    }
                }
            }
        }
    }
}
