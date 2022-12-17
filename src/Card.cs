using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zhai.FamilTheme
{
    public class Card : ContentControl
    {
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(Card), new PropertyMetadata(default(double), OnCornerRadiusChanged));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static readonly DependencyPropertyKey ContentClipPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ContentClip), typeof(Geometry), typeof(Card), new PropertyMetadata(default(Geometry)));
        
        public static readonly DependencyProperty ContentClipProperty = ContentClipPropertyKey.DependencyProperty;
        
        public Geometry ContentClip
        {
            get => (Geometry)GetValue(ContentClipProperty);
            private set => SetValue(ContentClipPropertyKey, value);
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Card card)
            {
                card.UpdateClipBorder();
            }
        }

        private Border ClipBorder;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ClipBorder = Template.FindName("PART_ClipBorder", this) as Border;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            UpdateClipBorder();
        }

        private void UpdateClipBorder()
        {
            if (ClipBorder is null)
            {
                return;
            }

            var farPointX = Math.Max(0, ClipBorder.ActualWidth);
            var farPointY = Math.Max(0, ClipBorder.ActualHeight);
            var farPoint = new Point(farPointX, farPointY);

            var clipRect = new Rect(new Point(0, 0), farPoint);
            ContentClip = new RectangleGeometry(clipRect, CornerRadius, CornerRadius);
        }
    }
}
