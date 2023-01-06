using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Controls
{
    public class Badged : ContentControl
    {
        static Badged()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Badged), new FrameworkPropertyMetadata(typeof(Badged)));
        }

        private static readonly DependencyPropertyKey IsBadgeSetPropertyKey = DependencyProperty.RegisterReadOnly("IsBadgeSet", typeof(bool), typeof(Badged), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty IsBadgeSetProperty = IsBadgeSetPropertyKey.DependencyProperty;
        public bool IsBadgeSet
        {
            get { return (bool)GetValue(IsBadgeSetProperty); }
            private set { SetValue(IsBadgeSetPropertyKey, value); }
        }

        public static readonly DependencyProperty BadgeBackgroundProperty = DependencyProperty.Register("BadgeBackground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));
        public Brush BadgeBackground
        {
            get { return (Brush)GetValue(BadgeBackgroundProperty); }
            set { SetValue(BadgeBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BadgeForegroundProperty = DependencyProperty.Register("BadgeForeground", typeof(Brush), typeof(Badged), new PropertyMetadata(default(Brush)));
        public Brush BadgeForeground
        {
            get { return (Brush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }

        public static readonly DependencyProperty BadgePlacementModeProperty = DependencyProperty.Register("BadgePlacementMode", typeof(BadgePlacementMode), typeof(Badged), new PropertyMetadata(default(BadgePlacementMode)));
        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode)GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register("Badge", typeof(object), typeof(Badged), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.AffectsArrange, OnBadgeChanged));
        public object Badge
        {
            get { return GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly RoutedEvent BadgeChangedEvent = EventManager.RegisterRoutedEvent("BadgeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(Badged));
        public event RoutedPropertyChangedEventHandler<object> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }

        public static readonly DependencyProperty BadgeChangedStoryboardProperty = DependencyProperty.Register(nameof(BadgeChangedStoryboard), typeof(Storyboard), typeof(Badged), new PropertyMetadata(default(Storyboard)));
        public Storyboard BadgeChangedStoryboard
        {
            get { return (Storyboard)GetValue(BadgeChangedStoryboardProperty); }
            set { SetValue(BadgeChangedStoryboardProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(Badged), new PropertyMetadata(9.0));
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        private static void OnBadgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Badged)d;

            instance.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || e.NewValue != null && !(e.NewValue is string);

            var args = new RoutedPropertyChangedEventArgs<object>(
                e.OldValue,
                e.NewValue)
            { RoutedEvent = BadgeChangedEvent };
            instance.RaiseEvent(args);
        }

        protected FrameworkElement BadgeContainer;

        public override void OnApplyTemplate()
        {
            BadgeChanged -= OnBadgeChanged;
            base.OnApplyTemplate();
            BadgeChanged += OnBadgeChanged;

            BadgeContainer = GetTemplateChild("PART_BadgeContainer") as FrameworkElement;
        }

        private void OnBadgeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (BadgeContainer is null || BadgeChangedStoryboard is null)
            {
                return;
            }

            BadgeContainer.BeginStoryboard(BadgeChangedStoryboard);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (BadgeContainer == null) return result;

            var containerDesiredSize = BadgeContainer.DesiredSize;
            if ((containerDesiredSize.Width <= 0.0 || containerDesiredSize.Height <= 0.0)
                && !double.IsNaN(BadgeContainer.ActualWidth) && !double.IsInfinity(BadgeContainer.ActualWidth)
                && !double.IsNaN(BadgeContainer.ActualHeight) && !double.IsInfinity(BadgeContainer.ActualHeight))
            {
                containerDesiredSize = new Size(BadgeContainer.ActualWidth, BadgeContainer.ActualHeight);
            }

            var h = 0 - containerDesiredSize.Width / 2;
            var v = 0 - containerDesiredSize.Height / 2;
            BadgeContainer.Margin = new Thickness(0);
            BadgeContainer.Margin = new Thickness(h, v, h, v);

            return result;
        }
    }
}
