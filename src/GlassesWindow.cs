using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Zhai.FamilTheme.Windows;

namespace Zhai.FamilTheme
{
    public class GlassesWindow : WindowBase
    {
        static GlassesWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlassesWindow), new FrameworkPropertyMetadata(typeof(GlassesWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var windowHelper = new WindowInteropHelper(this);

            WindowsForm.EnableBlur(windowHelper.Handle);
        }
    }
}
