using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace Zhai.FamilyContorls
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
        /// 滚动方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ScrollViewer), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// 滚动方向
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// 是否响应鼠标滚轮操作
        /// </summary>
        public static readonly DependencyProperty CanMouseWheelProperty = DependencyProperty.Register("CanMouseWheel", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

        /// <summary>
        /// 是否响应鼠标滚轮操作
        /// </summary>
        public bool CanMouseWheel
        {
            get => (bool)GetValue(CanMouseWheelProperty);
            set => SetValue(CanMouseWheelProperty, value);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!CanMouseWheel) return;

            if (!IsInertiaEnabled)
            {
                if (Orientation == Orientation.Vertical)
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

            if (Orientation == Orientation.Vertical)
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

        internal void ScrollToTopInternal(double milliseconds = 500)
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

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) =>
            IsPenetrating ? null : base.HitTestCore(hitTestParameters);

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public static readonly DependencyProperty IsInertiaEnabledProperty = DependencyProperty.RegisterAttached("IsInertiaEnabled", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        public static void SetIsInertiaEnabled(DependencyObject element, bool value) => element.SetValue(IsInertiaEnabledProperty, value);

        public static bool GetIsInertiaEnabled(DependencyObject element) => (bool)element.GetValue(IsInertiaEnabledProperty);

        /// <summary>
        /// 是否支持惯性
        /// </summary>
        public bool IsInertiaEnabled
        {
            get => (bool)GetValue(IsInertiaEnabledProperty);
            set => SetValue(IsInertiaEnabledProperty, value);
        }

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached("IsPenetrating", typeof(bool), typeof(ScrollViewer), new PropertyMetadata(false));

        /// <summary>
        /// 控件是否可以穿透点击
        /// </summary>
        public bool IsPenetrating
        {
            get => (bool)GetValue(IsPenetratingProperty);
            set => SetValue(IsPenetratingProperty, value);
        }

        public static void SetIsPenetrating(DependencyObject element, bool value) => element.SetValue(IsPenetratingProperty, value);

        public static bool GetIsPenetrating(DependencyObject element) => (bool)element.GetValue(IsPenetratingProperty);

        /// <summary>
        /// 当前垂直滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register("CurrentVerticalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata(.0, OnCurrentVerticalOffsetChanged));

        private static void OnCurrentVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToVerticalOffset(v);
            }
        }

        /// <summary>
        /// 当前垂直滚动偏移
        /// </summary>
        internal double CurrentVerticalOffset
        {
            // ReSharper disable once UnusedMember.Local
            get => (double)GetValue(CurrentVerticalOffsetProperty);
            set => SetValue(CurrentVerticalOffsetProperty, value);
        }

        /// <summary>
        /// 当前水平滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register("CurrentHorizontalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata(.0, OnCurrentHorizontalOffsetChanged));

        private static void OnCurrentHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToHorizontalOffset(v);
            }
        }

        /// <summary>
        /// 当前水平滚动偏移
        /// </summary>
        internal double CurrentHorizontalOffset
        {
            get => (double)GetValue(CurrentHorizontalOffsetProperty);
            set => SetValue(CurrentHorizontalOffsetProperty, value);
        }
    }
}
