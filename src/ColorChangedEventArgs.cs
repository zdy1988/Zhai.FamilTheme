using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Zhai.FamilTheme
{
    public class ColorChangedEventArgs : RoutedEventArgs
    {
        public ColorChangedEventArgs(RoutedEvent routedEvent, Color color) 
            : base(routedEvent)
        {
            Color = color;
        }

        public Color Color { get; private set; }
    }
}
