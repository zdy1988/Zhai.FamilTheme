using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Zhai.Famil.Controls
{
    public class Card : ContentControl
    {
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(nameof(ShadowColor), typeof(Color), typeof(Card), new PropertyMetadata(Colors.Black, OnShadowEdgeChanged));
        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(nameof(ShadowOpacity), typeof(double), typeof(Card), new PropertyMetadata(0.42, OnShadowEdgeChanged));
        public double ShadowOpacity
        {
            get => (double)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        public static readonly DependencyProperty ShadowBlurRadiusProperty = DependencyProperty.Register(nameof(ShadowBlurRadius), typeof(double), typeof(Card), new PropertyMetadata(default(double), OnShadowEdgeChanged));
        public double ShadowBlurRadius
        {
            get => (double)GetValue(ShadowBlurRadiusProperty);
            set => SetValue(ShadowBlurRadiusProperty, value);
        }

        private static readonly DependencyPropertyKey ShadowEdgePropertyKey = DependencyProperty.RegisterReadOnly(nameof(ShadowEdge), typeof(DrawingBrush), typeof(Card), new PropertyMetadata(default(DrawingBrush)));
        public static readonly DependencyProperty ShadowEdgeProperty = ShadowEdgePropertyKey.DependencyProperty;
        public DrawingBrush ShadowEdge
        {
            get => (DrawingBrush)GetValue(ShadowEdgeProperty);
            private set => SetValue(ShadowEdgePropertyKey, value);
        }

        private static readonly DependencyPropertyKey ShadowEffectPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ShadowEffect), typeof(DropShadowEffect), typeof(Card), new PropertyMetadata(default(DropShadowEffect)));
        public static readonly DependencyProperty ShadowEffectProperty = ShadowEffectPropertyKey.DependencyProperty;
        public DropShadowEffect ShadowEffect
        {
            get => (DropShadowEffect)GetValue(ShadowEffectProperty);
            private set => SetValue(ShadowEffectPropertyKey, value);
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

            UpdateShadowEdge();

            UpdateShadow();
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Card card)
            {
                card.UpdateClipBorder();
            }
        }

        private static void OnShadowEdgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Card card)
            {
                card.UpdateShadowEdge();
                card.UpdateShadow();
            }
        }

        private void UpdateClipBorder()
        {
            if (ClipBorder is null) return;

            var farPointX = Math.Max(0, ClipBorder.ActualWidth);
            var farPointY = Math.Max(0, ClipBorder.ActualHeight);
            var farPoint = new Point(farPointX, farPointY);

            var clipRect = new Rect(new Point(0, 0), farPoint);
            ContentClip = new RectangleGeometry(clipRect, CornerRadius, CornerRadius);
        }

        private void UpdateShadowEdge()
        {
            if (ClipBorder is null) return;

            var width = ClipBorder.ActualWidth;
            var height = ClipBorder.ActualHeight;
            var blurRadius = ShadowBlurRadius;

            if (double.IsNaN(width) || double.IsInfinity(width) || double.IsNaN(height) || double.IsInfinity(height) || double.IsNaN(blurRadius) || double.IsInfinity(blurRadius))
            {
                return;
            }

            var rect = new Rect(-blurRadius / 2, -blurRadius / 2, width + blurRadius, height + blurRadius);

            var size = new GeometryDrawing(new SolidColorBrush(Colors.White), new Pen(), new RectangleGeometry(rect));

            ShadowEdge = new DrawingBrush(size)
            {
                Stretch = Stretch.None,
                TileMode = TileMode.None,
                Viewport = rect,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewbox = rect,
                ViewboxUnits = BrushMappingMode.Absolute
            };
        }

        private void UpdateShadow()
        {
            ShadowEffect = ShadowBlurRadius <= 0 ? null : new DropShadowEffect
            {
                BlurRadius = ShadowBlurRadius,
                ShadowDepth = 1,
                Direction = 270,
                Color = ShadowColor,
                Opacity = ShadowOpacity,
                RenderingBias = RenderingBias.Performance
            };
        }
    }
}
