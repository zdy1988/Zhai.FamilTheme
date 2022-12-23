using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace Zhai.FamilTheme
{
    public class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        static ScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer), new FrameworkPropertyMetadata(typeof(ScrollViewer)));
        }

        /// <summary>
        /// 当前垂直滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register(nameof(CurrentVerticalOffset), typeof(double), typeof(ScrollViewer), new PropertyMetadata(.0, OnCurrentVerticalOffsetChanged));
        internal double CurrentVerticalOffset
        {
            // ReSharper disable once UnusedMember.Local
            get => (double)GetValue(CurrentVerticalOffsetProperty);
            set => SetValue(CurrentVerticalOffsetProperty, value);
        }
        private static void OnCurrentVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToVerticalOffset(v);
            }
        }

        /// <summary>
        /// 当前水平滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register(nameof(CurrentHorizontalOffset), typeof(double), typeof(ScrollViewer), new PropertyMetadata(.0, OnCurrentHorizontalOffsetChanged));
        internal double CurrentHorizontalOffset
        {
            get => (double)GetValue(CurrentHorizontalOffsetProperty);
            set => SetValue(CurrentHorizontalOffsetProperty, value);
        }
        private static void OnCurrentHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToHorizontalOffset(v);
            }
        }


        private double totalVerticalOffset;

        private double totalHorizontalOffset;

        private bool isRunning;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!ScrollViewerExtension.GetIsMouseWheelEnabled(this)) return;

            if (!ScrollViewerExtension.GetIsInertiaEnabled(this))
            {
                if (ScrollViewerExtension.GetMouseWheelOrientation(this) == Orientation.Vertical)
                {
                    base.OnMouseWheel(e);
                }
                else
                {
                    totalHorizontalOffset = HorizontalOffset;
                    CurrentHorizontalOffset = HorizontalOffset;
                    totalHorizontalOffset = Math.Min(Math.Max(0, totalHorizontalOffset - e.Delta), ScrollableWidth);
                    CurrentHorizontalOffset = totalHorizontalOffset;
                }
                return;
            }
            e.Handled = true;

            if (ScrollViewerExtension.GetMouseWheelOrientation(this) == Orientation.Vertical)
            {
                if (!isRunning)
                {
                    totalVerticalOffset = VerticalOffset;
                    CurrentVerticalOffset = VerticalOffset;
                }
                totalVerticalOffset = Math.Min(Math.Max(0, totalVerticalOffset - e.Delta), ScrollableHeight);
                ScrollToVerticalOffsetWithAnimation(totalVerticalOffset);
            }
            else
            {
                if (!isRunning)
                {
                    totalHorizontalOffset = HorizontalOffset;
                    CurrentHorizontalOffset = HorizontalOffset;
                }
                totalHorizontalOffset = Math.Min(Math.Max(0, totalHorizontalOffset - e.Delta), ScrollableWidth);
                ScrollToHorizontalOffsetWithAnimation(totalHorizontalOffset);
            }
        }

        public void ScrollToTopWithAnimation(double milliseconds = 500)
        {
            if (!isRunning)
            {
                totalVerticalOffset = VerticalOffset;
                CurrentVerticalOffset = VerticalOffset;
            }
            ScrollToVerticalOffsetWithAnimation(0, milliseconds);
        }

        public void ScrollToVerticalOffsetWithAnimation(double offset, double milliseconds = 300)
        {
            var animation = new DoubleAnimation(offset, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop
            };
            animation.Completed += (s, e1) =>
            {
                CurrentVerticalOffset = offset;
                isRunning = false;
            };
            isRunning = true;

            BeginAnimation(CurrentVerticalOffsetProperty, animation, HandoffBehavior.Compose);
        }

        public void ScrollToHorizontalOffsetWithAnimation(double offset, double milliseconds = 500)
        {
            var animation = new DoubleAnimation(offset, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                FillBehavior = FillBehavior.Stop,
            };
            animation.Completed += (s, e1) =>
            {
                CurrentHorizontalOffset = offset;
                isRunning = false;
            };
            isRunning = true;

            BeginAnimation(CurrentHorizontalOffsetProperty, animation, HandoffBehavior.Compose);
        }


        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return ScrollViewerExtension.GetIsPenetrating(this) ? null : base.HitTestCore(hitTestParameters);
        }
    }
}
