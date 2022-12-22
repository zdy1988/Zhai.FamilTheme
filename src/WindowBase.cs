using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zhai.FamilTheme
{
    public abstract class WindowBase : Window
    {
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(WindowBase), new PropertyMetadata(null));
        public string AppName
        {
            get { return (string)GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(nameof(Theme), typeof(WindowTheme), typeof(WindowBase), new FrameworkPropertyMetadata(WindowTheme.Dark));
        public WindowTheme Theme
        {
            get { return (WindowTheme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public static readonly DependencyProperty IsTransparencyProperty = DependencyProperty.Register(nameof(IsTransparency), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(true, OnTransparencyChanged));
        public bool IsTransparency
        {
            get { return (bool)GetValue(IsTransparencyProperty); }
            set { SetValue(IsTransparencyProperty, value); }
        }

        public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register(nameof(TitleBarContent), typeof(FrameworkElement), typeof(WindowBase), new PropertyMetadata(null));
        public FrameworkElement TitleBarContent
        {
            get { return (FrameworkElement)GetValue(TitleBarContentProperty); }
            set { SetValue(TitleBarContentProperty, value); }
        }

        public static readonly DependencyProperty TitleBarVisibilityProperty = DependencyProperty.Register(nameof(TitleBarVisibility), typeof(Visibility), typeof(WindowBase), new FrameworkPropertyMetadata(Visibility.Visible));
        public Visibility TitleBarVisibility
        {
            get { return (Visibility)GetValue(TitleBarVisibilityProperty); }
            set { SetValue(TitleBarVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IsTitleBarBackgroundEnabledProperty = DependencyProperty.Register(nameof(IsTitleBarBackgroundEnabled), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(true));
        public bool IsTitleBarBackgroundEnabled
        {
            get { return (bool)GetValue(IsTitleBarBackgroundEnabledProperty); }
            set { SetValue(IsTitleBarBackgroundEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsCaptionButtonsEnabledProperty = DependencyProperty.Register(nameof(IsCaptionButtonsEnabled), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(true));
        public bool IsCaptionButtonsEnabled
        {
            get { return (bool)GetValue(IsCaptionButtonsEnabledProperty); }
            set { SetValue(IsCaptionButtonsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsTopmostButtonEnabledProperty = DependencyProperty.Register(nameof(IsTopmostButtonEnabled), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(false));
        public bool IsTopmostButtonEnabled
        {
            get { return (bool)GetValue(IsTopmostButtonEnabledProperty); }
            set { SetValue(IsTopmostButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsThemeButtonEnabledProperty = DependencyProperty.Register(nameof(IsThemeButtonEnabled), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(false));
        public bool IsThemeButtonEnabled
        {
            get { return (bool)GetValue(IsThemeButtonEnabledProperty); }
            set { SetValue(IsThemeButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsTransparencyButtonEnabledProperty = DependencyProperty.Register(nameof(IsTransparencyButtonEnabled), typeof(bool), typeof(WindowBase), new FrameworkPropertyMetadata(false));
        public bool IsTransparencyButtonEnabled
        {
            get { return (bool)GetValue(IsTransparencyButtonEnabledProperty); }
            set { SetValue(IsTransparencyButtonEnabledProperty, value); }
        }

        public static readonly DependencyProperty TransparencyProperty = DependencyProperty.Register(nameof(Transparency), typeof(double), typeof(WindowBase), new FrameworkPropertyMetadata(0.72, OnTransparencyChanged));
        public double Transparency
        {
            get { return (double)GetValue(TransparencyProperty); }
            set { SetValue(TransparencyProperty, value); }
        }

        readonly double MaxTransparency = 1.0;
        readonly double MinTransparency = 0.1;
        readonly double DefaultTransparency = 0.72;
        double TempTransparency = 1.0;

        private static void OnTransparencyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WindowBase window)
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var TitleBar = Template.FindName("TitleBar", this) as DockPanel;
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

            var MinimizeButton = Template.FindName("MinimizeButton", this) as IconButton;
            MinimizeButton.Click += (s, e) => SystemCommands.MinimizeWindow(this);
            var MaximizeButton = Template.FindName("MaximizeButton", this) as IconButton;
            MaximizeButton.Click += (s, e) => SystemCommands.MaximizeWindow(this);
            var RestoreButton = Template.FindName("RestoreButton", this) as IconButton;
            RestoreButton.Click += (s, e) => SystemCommands.RestoreWindow(this);
            var CloseButton = Template.FindName("CloseButton", this) as IconButton;
            CloseButton.Click += (s, e) => SystemCommands.CloseWindow(this);

            var PinButton = Template.FindName("PinButton", this) as IconButton;
            PinButton.Click += (s, e) => this.Topmost = !this.Topmost;

            var ThemeButton = Template.FindName("ThemeButton", this) as IconButton;
            ThemeButton.Click += (s, e) => this.Theme = this.Theme == WindowTheme.Dark ? WindowTheme.Light : WindowTheme.Dark;

            var TransparentButton = Template.FindName("TransparentButton", this) as IconButton;
            TransparentButton.Click += (s, e) => this.IsTransparency = !this.IsTransparency;
        }
    }
}
