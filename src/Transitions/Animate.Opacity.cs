using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Transitions
{
    public partial class Animate
    {
        public static readonly DependencyProperty OpacityProperty = DependencyProperty.RegisterAttached("Opacity", typeof(IOpacityParameter), typeof(Animate), new PropertyMetadata(default(IOpacityParameter), OnOpacityChanged));
        public static void SetOpacity(UIElement element, IOpacityParameter value) => element.SetValue(OpacityProperty, value);
        public static IOpacityParameter GetOpacity(UIElement element) => (IOpacityParameter)element.GetValue(OpacityProperty);

        private static void OnOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldTransitionParams = e.OldValue as IOpacityParameter;
            var newTransitionParams = e.NewValue as IOpacityParameter;
            var target = d as FrameworkElement;
            if (target == null)
                return;

            if (oldTransitionParams != null)
            {
                target.Loaded -= OnLoadedForOpacity;
                target.DataContextChanged -= OnDataContextChangedForOpacity;
                Animate.RemoveVisibilityChangedHandler(target, OnVisibilityChangedForOpacity);
            }

            if (newTransitionParams != null)
            {
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.Loaded) || Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.Once))
                {
                    target.Loaded += OnLoadedForOpacity;
                    if (target.IsLoaded()) OnLoadedForOpacity(target, null);
                }
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.DataContextChanged))
                {
                    target.DataContextChanged += OnDataContextChangedForOpacity;
                }
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.Visibility))
                {
                    Animate.AddVisibilityChangedHandler(target, OnVisibilityChangedForOpacity);
                }
            }
        }

        private static void OnVisibilityChangedForOpacity(object sender, EventArgs e)
        {
            var element = ((FrameworkElement)((PropertyChangeNotifier)sender).PropertySource);
            var visibility = Animate.GetVisibility(element);
            if (visibility == Visibility.Visible)
            {
                element.Visibility = Visibility.Visible;
            }

            DoOpacityTransition(GetOpacity(element), element, null, visibility);
        }

        private static void OnDataContextChangedForOpacity(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = ((FrameworkElement)sender);
            DoOpacityTransition(GetOpacity(element), element, null, null);
        }

        private static void OnLoadedForOpacity(object sender, RoutedEventArgs routedEventArgs)
        {
            var element = ((FrameworkElement)sender);
            DoOpacityTransition(GetOpacity(element), element, OnLoadedForOpacity, null);
        }

        private static void DoOpacityTransition(IOpacityParameter transitionParams, FrameworkElement target, RoutedEventHandler onLoaded, Visibility? visibility)
        {
            var reverse = Animate.IsVisibilityHidden(visibility);
            target.Opacity = reverse ? transitionParams.To : transitionParams.From;

            if (onLoaded != null && Animate.HasFlag(transitionParams.TransitionOn, TransitionOn.Once))
            {
                target.Loaded -= onLoaded;
            }

            var a = new DoubleAnimation
            {
                From = reverse ? transitionParams.To : transitionParams.From,
                To = reverse ? transitionParams.From : transitionParams.To,
                FillBehavior = transitionParams.FillBehavior,
                BeginTime = TimeSpan.FromMilliseconds(transitionParams.BeginTime),
                Duration = new Duration(TimeSpan.FromMilliseconds(transitionParams.Duration)),
                EasingFunction = reverse ? (transitionParams.ReverseEase ?? transitionParams.Ease) : transitionParams.Ease,
                AutoReverse = transitionParams.AutoReverse,
            };

            // Directly adding RepeatBehavior to constructor breaks existing animations, so only add it if properly defined
            if (transitionParams.RepeatBehavior == RepeatBehavior.Forever
                || transitionParams.RepeatBehavior.HasDuration
                || (transitionParams.RepeatBehavior.HasDuration && transitionParams.RepeatBehavior.Count > 0))
            {
                a.RepeatBehavior = transitionParams.RepeatBehavior;
            }

            a.SetDesiredFrameRate(24);

            var storyboard = new Storyboard();
            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, target);
            Storyboard.SetTargetProperty(a, new PropertyPath(UIElement.OpacityProperty));

            a.Completed += (_, __) =>
            {
                if (visibility.HasValue)
                    target.Visibility = visibility.Value;
                target.Opacity = reverse ? transitionParams.From : transitionParams.To;
                storyboard.Stop();
            };
            storyboard.Begin();
        }
    }
}