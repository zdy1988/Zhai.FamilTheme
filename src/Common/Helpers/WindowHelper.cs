using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.Famil.Common.Helpers
{
    public static class WindowHelper
    {
        public static void Show<T>()
            where T : Window, new()
        {
            new T().Show();
        }

        public static void Show<T>(Window owner)
            where T : Window, new()
        {
            new T { Owner = owner }.Show();
        }

        public static bool? ShowDialog<T>(this T t)
            where T : Window, new()
        {
            return new T().ShowDialog();
        }

        public static bool? ShowDialog<T>(Window owner)
            where T : Window, new()
        {
            return new T { Owner = owner }.ShowDialog();
        }
    }
}
