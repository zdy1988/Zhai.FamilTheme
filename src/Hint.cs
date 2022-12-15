using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    public class Hint : Control
    {
        static Hint()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Hint), new FrameworkPropertyMetadata(typeof(Hint)));
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Hint), new PropertyMetadata(default(CornerRadius)));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(Hint), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(3000))));

        public Duration Duration
        {
            get => (Duration)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(Hint), new PropertyMetadata(OnTextChanged));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        FrameworkElement HintContentElement;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            HintContentElement = this.FindVisualChildByName<FrameworkElement>("PART_HintContent");
        }

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Hint hint && !string.IsNullOrWhiteSpace(hint.Text) && hint.HintContentElement != null)
            {
                var animation = new DoubleAnimation(1, 0, hint.Duration)
                {
                    EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut },
                    FillBehavior = FillBehavior.Stop
                };

                hint.HintContentElement.BeginAnimation(OpacityProperty, animation, HandoffBehavior.SnapshotAndReplace);
            }
        }
    }
}
