using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Transitions
{
    public partial class Animate
    {
        public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached("Margin", typeof(AnimateMarginParameterExtension), typeof(Animate), new PropertyMetadata(default(AnimateMarginParameterExtension), OnMarginParamsChanged));
        public static void SetMargin(UIElement element, AnimateMarginParameterExtension value) => element.SetValue(MarginProperty, value);
        public static AnimateMarginParameterExtension GetMargin(UIElement element) => (AnimateMarginParameterExtension)element.GetValue(MarginProperty);

        private static void OnMarginParamsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var transitionParams = e.NewValue as AnimateMarginParameterExtension;
            var target = d as FrameworkElement;
            if (transitionParams == null || target == null)
                return;

            target.Margin = transitionParams.From;
            RoutedEventHandler onLoaded = null;
            onLoaded = (_, __) => target.BeginInvoke(() =>
            {
                target.Loaded -= onLoaded;
                var a = new ThicknessAnimation
                {
                    From = transitionParams.From,
                    To = transitionParams.To,
                    FillBehavior = transitionParams.FillBehavior,
                    BeginTime = TimeSpan.FromMilliseconds(transitionParams.BeginTime),
                    Duration = new Duration(TimeSpan.FromMilliseconds(transitionParams.Duration)),
                    EasingFunction = transitionParams.Ease
                };


                // Directly adding RepeatBehavior to constructor breaks existing animations, so only add it if properly defined
                if (transitionParams.RepeatBehavior == RepeatBehavior.Forever
                    || transitionParams.RepeatBehavior.HasDuration
                    || (transitionParams.RepeatBehavior.HasDuration && transitionParams.RepeatBehavior.Count > 0))
                {
                    a.RepeatBehavior = transitionParams.RepeatBehavior;
                }
                var storyboard = new Storyboard();

                storyboard.Children.Add(a);
                Storyboard.SetTarget(a, target);
                Storyboard.SetTargetProperty(a, new PropertyPath(FrameworkElement.MarginProperty));
                storyboard.Begin();
            }, DispatchPriority.DataBind);

            if (target.IsLoaded())
                onLoaded(null, null);
            else
                target.Loaded += onLoaded;
        }
    }
}
