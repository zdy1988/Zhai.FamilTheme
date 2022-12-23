using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Zhai.FamilTheme.Transitions
{
    public partial class Animate
    {
        public static readonly DependencyProperty BlurProperty = DependencyProperty.RegisterAttached("Blur", typeof(IBlurParameter), typeof(Animate), new PropertyMetadata(default(IBlurParameter), OnBlurParamsChanged));
        public static void SetBlur(DependencyObject element, IBlurParameter value) => element.SetValue(BlurProperty, value);
        public static IBlurParameter GetBlur(DependencyObject element) => (IBlurParameter)element.GetValue(BlurProperty);

        private static void OnBlurParamsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldTransitionParams = e.OldValue as IBlurParameter;
            var newTransitionParams = e.NewValue as IBlurParameter;
            var target = d as FrameworkElement;
            if (target == null)
                return;

            if (oldTransitionParams != null)
            {
                target.Loaded -= Animate.OnLoadedForTranslate;
                target.DataContextChanged -= Animate.OnDataContextChangedForTranslate;
            }

            if (newTransitionParams != null)
            {
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.Loaded))
                {
                    target.Loaded += OnLoadedForBlur;
                    if (target.IsLoaded()) OnLoadedForBlur(target, null);
                }
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.DataContextChanged))
                {
                    target.DataContextChanged += OnDataContextChangedForBlur;
                }
                if (Animate.HasFlag(newTransitionParams.TransitionOn, TransitionOn.Visibility))
                {
                    Animate.AddVisibilityChangedHandler(target, OnVisibilityChangedForBlur);
                }
            }
        }

        private static void OnVisibilityChangedForBlur(object sender, EventArgs e)
        {
            var element = ((FrameworkElement)((PropertyChangeNotifier)sender).PropertySource);
            var visibility = Animate.GetVisibility(element);
            if (visibility == Visibility.Visible)
            {
                element.Visibility = Visibility.Visible;
            }
            DoBlurTansition(GetBlur(element), element, null, visibility);
        }

        private static void OnLoadedForBlur(object sender, RoutedEventArgs e)
        {
            var element = ((FrameworkElement)sender);
            DoBlurTansition(GetBlur(element), element, OnLoadedForBlur, null);
        }

        private static void OnDataContextChangedForBlur(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = ((FrameworkElement)sender);
            DoBlurTansition(GetBlur(element), element, null, null);
        }

        private static void DoBlurTansition(IBlurParameter blurParams, FrameworkElement target, RoutedEventHandler onLoaded, Visibility? visibility)
        {
            var reverse = Animate.IsVisibilityHidden(visibility);
            var blurEffect = new BlurEffect() { Radius = reverse ? blurParams.To : blurParams.From };
            target.Effect = blurEffect;

            if (onLoaded != null && Animate.HasFlag(blurParams.TransitionOn, TransitionOn.Once))
            {
                target.Loaded -= onLoaded;
            }

            var a = new DoubleAnimation
            {
                From = reverse ? blurParams.To : blurParams.From,
                To = reverse ? blurParams.From : blurParams.To,
                FillBehavior = blurParams.FillBehavior,
                BeginTime = TimeSpan.FromMilliseconds(blurParams.BeginTime),
                Duration = new Duration(TimeSpan.FromMilliseconds(blurParams.Duration)),
                EasingFunction = reverse ? (blurParams.ReverseEase ?? blurParams.Ease) : blurParams.Ease,
                AutoReverse = blurParams.AutoReverse,
            };

            // Directly adding RepeatBehavior to constructor breaks existing animations, so only add it if properly defined
            if (blurParams.RepeatBehavior == RepeatBehavior.Forever
                || blurParams.RepeatBehavior.HasDuration
                || (blurParams.RepeatBehavior.HasDuration && blurParams.RepeatBehavior.Count > 0))
            {
                a.RepeatBehavior = blurParams.RepeatBehavior;
            }

            if (blurParams.To == 0.0)
            {
                a.Completed += (_, __) => target.Effect = null;
            }

            if (visibility.HasValue)
                a.Completed += (_, __) => target.Visibility = visibility.Value;

            a.SetDesiredFrameRate(24);

            var storyboard = new Storyboard();
            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, ((BlurEffect)target.Effect));
            Storyboard.SetTargetProperty(a, new PropertyPath(BlurEffect.RadiusProperty));
            FreezeExtension.SetFreeze(storyboard, true);
            storyboard.Begin();
        }
    }
}