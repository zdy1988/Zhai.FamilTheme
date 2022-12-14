using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Zhai.FamilTheme
{
    public class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        static ScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer), new FrameworkPropertyMetadata(typeof(ScrollViewer)));
        }

        private double totalVerticalOffset;

        private double totalHorizontalOffset;

        private bool isRunning;

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.Register(nameof(IsInertiaEnabled), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        public bool IsInertiaEnabled
        {
            get => (bool)GetValue(IsInertiaEnabledProperty);
            set => SetValue(IsInertiaEnabledProperty, value);
        }

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached(nameof(IsPenetrating), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        public bool IsPenetrating
        {
            get => (bool)GetValue(IsPenetratingProperty);
            set => SetValue(IsPenetratingProperty, value);
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return IsPenetrating ? null : base.HitTestCore(hitTestParameters);
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

        /// <summary>
        /// 滚动方向
        /// </summary>
        public static readonly DependencyProperty MouseWheelOrientationProperty = DependencyProperty.Register(nameof(MouseWheelOrientation), typeof(Orientation), typeof(ScrollViewer), new PropertyMetadata(Orientation.Vertical));

        public Orientation MouseWheelOrientation
        {
            get => (Orientation)GetValue(MouseWheelOrientationProperty);
            set => SetValue(MouseWheelOrientationProperty, value);
        }

        /// <summary>
        /// 是否响应鼠标滚轮操作
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelEnabledProperty = DependencyProperty.Register(nameof(IsMouseWheelEnabled), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

        public bool IsMouseWheelEnabled
        {
            get => (bool)GetValue(IsMouseWheelEnabledProperty);
            set => SetValue(IsMouseWheelEnabledProperty, value);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!IsMouseWheelEnabled) return;

            if (!IsInertiaEnabled)
            {
                if (MouseWheelOrientation == Orientation.Vertical)
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

            if (MouseWheelOrientation == Orientation.Vertical)
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

        /// <summary>
        /// 是否自动隐藏滚动条
        /// </summary>
        public static readonly DependencyProperty IsAutoHideEnabledProperty = DependencyProperty.Register(nameof(IsAutoHideEnabled), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

        public bool IsAutoHideEnabled
        {
            get => (bool)GetValue(IsAutoHideEnabledProperty);
            set => SetValue(IsAutoHideEnabledProperty, value);
        }

        /// <summary>
        /// 是否覆盖在内容之上
        /// </summary>
        public static readonly DependencyProperty IsOverlayedProperty = DependencyProperty.Register(nameof(IsOverlayed), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        /// <summary>
        /// 是否覆盖在内容之上
        /// </summary>
        public bool IsOverlayed
        {
            get { return (bool)GetValue(IsOverlayedProperty); }
            set { SetValue(IsOverlayedProperty, value); }
        }
    }
}
