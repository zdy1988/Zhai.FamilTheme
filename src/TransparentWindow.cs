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
using Zhai.FamilyContorls.Common;

namespace Zhai.FamilyContorls
{
    public class TransparentWindow : Window
    {
        #region NativeMethods

        public const int WM_SYSCOMMAND = 0x112;
        public HwndSource HwndSource;

        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        static TransparentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransparentWindow), new FrameworkPropertyMetadata(typeof(TransparentWindow)));
        }

        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(TransparentWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public string AppName
        {
            get { return (string)GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(nameof(Theme), typeof(WindowTheme), typeof(TransparentWindow), new FrameworkPropertyMetadata(WindowTheme.Dark, FrameworkPropertyMetadataOptions.Inherits));

        public WindowTheme Theme
        {
            get { return (WindowTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly DependencyProperty IsTransparencyProperty = DependencyProperty.Register(nameof(IsTransparency), typeof(bool), typeof(TransparentWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits, OnTransparencyChanged));

        public bool IsTransparency
        {
            get { return (bool)GetValue(IsTransparencyProperty); }
            set { SetValue(IsTransparencyProperty, value); }
        }

        public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(FrameworkElement), typeof(TransparentWindow), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public FrameworkElement TitleBarContent
        {
            get { return (FrameworkElement)GetValue(TitleBarContentProperty); }
            set { SetValue(TitleBarContentProperty, value); }
        }

        public static readonly DependencyProperty TitleBarVisibilityProperty = DependencyProperty.Register(nameof(TitleBarVisibility), typeof(Visibility), typeof(TransparentWindow), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility TitleBarVisibility
        {
            get { return (Visibility)GetValue(TitleBarVisibilityProperty); }
            set { SetValue(TitleBarVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IsTopmostButtonEnabledProperty = DependencyProperty.Register(nameof(IsTopmostButtonEnabled), typeof(bool), typeof(TransparentWindow), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public bool IsTopmostButtonEnabled
        {
            get { return (bool)GetValue(IsTopmostButtonEnabledProperty); }
            set { SetValue(IsTopmostButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsThemeButtonEnabledProperty = DependencyProperty.Register(nameof(IsThemeButtonEnabled), typeof(bool), typeof(TransparentWindow), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public bool IsThemeButtonEnabled
        {
            get { return (bool)GetValue(IsThemeButtonEnabledProperty); }
            set { SetValue(IsThemeButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsTransparencyButtonEnabledProperty = DependencyProperty.Register(nameof(IsTransparencyButtonEnabled), typeof(bool), typeof(TransparentWindow), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public bool IsTransparencyButtonEnabled
        {
            get { return (bool)GetValue(IsTransparencyButtonEnabledProperty); }
            set { SetValue(IsTransparencyButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty TransparencyProperty = DependencyProperty.Register(nameof(Transparency), typeof(double), typeof(TransparentWindow), new FrameworkPropertyMetadata(0.72, FrameworkPropertyMetadataOptions.Inherits, OnTransparencyChanged));

        public double Transparency
        {
            get { return (double)GetValue(TransparencyProperty); }
            set { SetValue(TransparencyProperty, value); }
        }

        double MaxTransparency = 1.0;
        double MinTransparency = 0.1;
        double DefaultTransparency = 0.72;
        double TempTransparency = 1.0;

        private static void OnTransparencyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TransparentWindow window)
            {
                if (e.Property.Name.Equals("IsTransparency"))
                {
                    if (!window.IsTransparency)
                    {
                        window.TempTransparency = window.Transparency;
                        window.Transparency = window.MaxTransparency;
                    }
                    else if (window.IsTransparency && window.TempTransparency == window.MaxTransparency)
                    {
                        window.Transparency = window.DefaultTransparency;
                    }
                    else
                    {
                        window.Transparency = window.TempTransparency;
                    }
                }
                else if (e.Property.Name.Equals("Transparency"))
                {
                    if (window.Transparency < window.MinTransparency)
                    {
                        window.IsTransparency = true;
                        window.Transparency = window.MinTransparency;
                    }
                    else if (window.Transparency >= window.MaxTransparency)
                    {
                        window.IsTransparency = false;
                        window.Transparency = window.MaxTransparency;
                    }
                    else
                    {
                        window.IsTransparency = true;
                    }
                }
            }
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            this.HwndSource = PresentationSource.FromVisual((Visual)this) as HwndSource;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (VisualTreeHelper.GetChild(this, 0) is Border layout)
            {
                //var TopLeft = layout.FindChild<Rectangle>("ResizeTopLeft");
                //TopLeft.MouseMove += ResizePressed;
                //TopLeft.MouseDown += ResizePressed;
                //var Top = layout.FindChild<Rectangle>("ResizeTop");
                //Top.MouseMove += ResizePressed;
                //Top.MouseDown += ResizePressed;
                //var TopRight = layout.FindChild<Rectangle>("ResizeTopRight");
                //TopRight.MouseMove += ResizePressed;
                //TopRight.MouseDown += ResizePressed;
                //var Left = layout.FindChild<Rectangle>("ResizeLeft");
                //Left.MouseMove += ResizePressed;
                //Left.MouseDown += ResizePressed;
                var Right = layout.FindChild<Rectangle>("ResizeRight");
                Right.MouseMove += ResizePressed;
                Right.MouseDown += ResizePressed;
                //var BottomLeft = layout.FindChild<Rectangle>("ResizeBottomLeft");
                //BottomLeft.MouseMove += ResizePressed;
                //BottomLeft.MouseDown += ResizePressed;
                var Bottom = layout.FindChild<Rectangle>("ResizeBottom");
                Bottom.MouseMove += ResizePressed;
                Bottom.MouseDown += ResizePressed;
                var BottomRight = layout.FindChild<Rectangle>("ResizeBottomRight");
                BottomRight.MouseMove += ResizePressed;
                BottomRight.MouseDown += ResizePressed;


                var TitleBar = layout.FindChild<DockPanel>("TitleBar");
                TitleBar.MouseMove += (s, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        this.DragMove();
                    }
                };
                TitleBar.MouseLeftButtonDown += (s, e) =>
                {
                    if (e.ClickCount == 2)
                    {
                        if (this.WindowState == WindowState.Maximized)
                        {
                            SystemCommands.RestoreWindow(this);
                        }
                        else
                        {
                            SystemCommands.MaximizeWindow(this);
                        }
                    }
                };

                var MinimizeButton = layout.FindChild<IconButton>("MinimizeButton");
                MinimizeButton.Click += (s, e) => SystemCommands.MinimizeWindow(this);
                var MaximizeButton = layout.FindChild<IconButton>("MaximizeButton");
                MaximizeButton.Click += (s, e) => SystemCommands.MaximizeWindow(this);
                var RestoreButton = layout.FindChild<IconButton>("RestoreButton");
                RestoreButton.Click += (s, e) => SystemCommands.RestoreWindow(this);
                var CloseButton = layout.FindChild<IconButton>("CloseButton");
                CloseButton.Click += (s, e) => SystemCommands.CloseWindow(this);

                var PinButton = layout.FindChild<IconButton>("PinButton");
                PinButton.Click += (s, e) => this.Topmost = !this.Topmost;

                var ThemeButton = layout.FindChild<IconButton>("ThemeButton");
                ThemeButton.Click += (s, e) => this.Theme = this.Theme == WindowTheme.Dark ? WindowTheme.Light : WindowTheme.Dark;

                var TransparentButton = layout.FindChild<IconButton>("TransparentButton");
                TransparentButton.Click += (s, e) => this.IsTransparency = !this.IsTransparency;
            }
        }

        public void ResizePressed(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ResizeWindow(direction);
            }
        }

        public void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
    }
}
