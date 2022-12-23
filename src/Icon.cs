using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class Icon : Control
    {
        static Icon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
        }

        private static readonly Lazy<IDictionary<IconKind, string>> _dataIndex  = new Lazy<IDictionary<IconKind, string>>(IconDataFactory.Create);

        public static readonly DependencyProperty KindProperty = DependencyProperty.Register(nameof(Kind), typeof(IconKind), typeof(Icon), new PropertyMetadata(default(IconKind), OnKindPropertyChanged));
        public IconKind Kind
        {
            get => (IconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        // ReSharper disable once StaticMemberInGenericType
        private static readonly DependencyPropertyKey DataPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(Icon), new PropertyMetadata(""));
        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;
        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        [TypeConverter(typeof(GeometryConverter))]
        public string? Data
        {
            get => (string?)GetValue(DataProperty);
            private set => SetValue(DataPropertyKey, value);
        }

        private static void OnKindPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((Icon)dependencyObject).UpdateData();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateData();
        }

        private void UpdateData()
        {
            string? data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            Data = data;
        }
    }
}
