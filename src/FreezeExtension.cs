using System.Windows;

namespace Zhai.FamilTheme
{
    public class FreezeExtension
    {
        public static readonly DependencyProperty FreezeProperty = DependencyProperty.RegisterAttached("Freeze", typeof(bool), typeof(FreezeExtension), new PropertyMetadata(false, OnFreezePropertyChanged));

        public static void SetFreeze(DependencyObject element, bool value)
        {
            element.SetValue(FreezeProperty, value);
        }

        public static bool GetFreeze(DependencyObject element)
        {
            return (bool)element.GetValue(FreezeProperty);
        }

        private static void OnFreezePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Freezable f)
            {
                if (true.Equals(e.NewValue) && f.CanFreeze)
                    f.Freeze();
            }
        }
    }
}
