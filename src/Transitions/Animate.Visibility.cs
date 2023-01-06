using System;
using System.Linq;
using System.Windows;

namespace Zhai.Famil.Transitions
{
    public partial class Animate
    {
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached("Visibility", typeof(Visibility), typeof(Animate), new PropertyMetadata(default(Visibility)));
        public static void SetVisibility(UIElement element, Visibility value) => element.SetValue(VisibilityProperty, value);
        public static Visibility GetVisibility(UIElement element) => (Visibility)element.GetValue(VisibilityProperty);

        private static bool HasFlag(TransitionOn transitionOn, TransitionOn flag)
        {
            return (transitionOn & flag) != 0;
        }

        public static void AddVisibilityChangedHandler(DependencyObject d, EventHandler handler)
        {           
            var pcn = new PropertyChangeNotifier(d, Animate.VisibilityProperty);
            pcn.ValueChanged += handler;

            var currentValue = (Visibility)d.GetValue(Animate.VisibilityProperty);
            if (currentValue == Visibility.Visible)
            {
                // Execute handler now if already Visible
                handler(pcn, EventArgs.Empty);
            }
        }

        public static void RemoveVisibilityChangedHandler(DependencyObject d, EventHandler handler)
        {
            if (d == null) throw new ArgumentNullException("d");

            var notifiers = PropertyChangeNotifier.GetNotifiers(d);
            if (notifiers == null) return;

            var pcn = notifiers.FirstOrDefault(n => n.PropertySource == d);
            if (pcn != null)
            {
                pcn.ValueChanged -= handler;
                pcn.Dispose();
                notifiers.Remove(pcn);
            }
        }

        private static bool IsVisibilityHidden(Visibility? visibility)
        {
            return visibility == Visibility.Collapsed || visibility == Visibility.Hidden;
        }
    }
}