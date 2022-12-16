using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Zhai.FamilTheme
{
    public class ProgressBar : System.Windows.Controls.ProgressBar
    {
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(double), typeof(ProgressBar), new PropertyMetadata(default));

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(ProgressBar), new PropertyMetadata(null));

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(ProgressBar), new PropertyMetadata(null));

        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }

        public static readonly DependencyProperty ProgressgroundProperty = DependencyProperty.Register(nameof(Progressground), typeof(Brush), typeof(ProgressBar));

        public Brush Progressground
        {
            get => (Brush)GetValue(ProgressgroundProperty);
            set => SetValue(ProgressgroundProperty, value);
        }

        public static readonly DependencyProperty ProgressAnimationForegroundProperty = DependencyProperty.Register(nameof(ProgressAnimationForeground), typeof(Brush), typeof(ProgressBar));

        public Brush ProgressAnimationForeground
        {
            get => (Brush)GetValue(ProgressAnimationForegroundProperty);
            set => SetValue(ProgressAnimationForegroundProperty, value);
        }

        public static readonly DependencyProperty IsProgressAnimationEnabledProperty = DependencyProperty.Register(nameof(IsProgressAnimationEnabled), typeof(bool), typeof(ProgressBar), new PropertyMetadata(true));

        public bool IsProgressAnimationEnabled
        {
            get => (bool)GetValue(IsProgressAnimationEnabledProperty);
            set => SetValue(IsProgressAnimationEnabledProperty, value);
        }
    }
}
