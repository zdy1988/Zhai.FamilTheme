using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using Zhai.FamilTheme.Windows;
using Zhai.FamilTheme.Windows.Natives;

namespace Zhai.FamilTheme
{
    public class TransparentWindow : WindowBase
    {
        static TransparentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransparentWindow), new FrameworkPropertyMetadata(typeof(TransparentWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //var TopLeft = Template.FindName("ResizeTopLeft", this) as Rectangle;
            //TopLeft.MouseMove += ResizePressed;
            //TopLeft.MouseDown += ResizePressed;
            //var Top = Template.FindName("ResizeTop", this) as Rectangle;
            //Top.MouseMove += ResizePressed;
            //Top.MouseDown += ResizePressed;
            //var TopRight = Template.FindName("ResizeTopRight", this) as Rectangle;
            //TopRight.MouseMove += ResizePressed;
            //TopRight.MouseDown += ResizePressed;
            //var Left = Template.FindName("ResizeLeft", this) as Rectangle;
            //Left.MouseMove += ResizePressed;
            //Left.MouseDown += ResizePressed;
            var Right = Template.FindName("ResizeRight", this) as Rectangle;
            Right.MouseMove += ResizePressed;
            Right.MouseDown += ResizePressed;
            //var BottomLeft = Template.FindName("ResizeBottomLeft", this) as Rectangle;
            //BottomLeft.MouseMove += ResizePressed;
            //BottomLeft.MouseDown += ResizePressed;
            var Bottom = Template.FindName("ResizeBottom", this) as Rectangle;
            Bottom.MouseMove += ResizePressed;
            Bottom.MouseDown += ResizePressed;
            var BottomRight = Template.FindName("ResizeBottomRight", this) as Rectangle;
            BottomRight.MouseMove += ResizePressed;
            BottomRight.MouseDown += ResizePressed;
        }

        private HwndSource HwndSource;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            this.HwndSource = PresentationSource.FromVisual((Visual)this) as HwndSource;
        }

        public void ResizePressed(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            var direction = Enum.Parse<WindowResizeDirection>(element.Name.Replace("Resize", ""));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                WindowsForm.Resize(HwndSource.Handle, direction);
            }
        }
    }
}
