using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Zhai.Famil.Controls
{
    [TemplateVisualState(GroupName = TemplateAllDrawersGroupName, Name = TemplateAllDrawersAllClosedStateName)]
    [TemplateVisualState(GroupName = TemplateAllDrawersGroupName, Name = TemplateAllDrawersAnyOpenStateName)]
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftClosedStateName)]
    [TemplateVisualState(GroupName = TemplateLeftDrawerGroupName, Name = TemplateLeftOpenStateName)]
    [TemplateVisualState(GroupName = TemplateTopDrawerGroupName, Name = TemplateTopClosedStateName)]
    [TemplateVisualState(GroupName = TemplateTopDrawerGroupName, Name = TemplateTopOpenStateName)]
    [TemplateVisualState(GroupName = TemplateRightDrawerGroupName, Name = TemplateRightClosedStateName)]
    [TemplateVisualState(GroupName = TemplateRightDrawerGroupName, Name = TemplateRightOpenStateName)]
    [TemplateVisualState(GroupName = TemplateBottomDrawerGroupName, Name = TemplateBottomClosedStateName)]
    [TemplateVisualState(GroupName = TemplateBottomDrawerGroupName, Name = TemplateBottomOpenStateName)]
    [TemplatePart(Name = TemplateContentCoverPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TemplateLeftDrawerPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TemplateTopDrawerPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TemplateRightDrawerPartName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TemplateBottomDrawerPartName, Type = typeof(FrameworkElement))]
    public class Drawer : ContentControl
    {
        public const string TemplateAllDrawersGroupName = "AllDrawers";
        public const string TemplateAllDrawersAllClosedStateName = "AllClosed";
        public const string TemplateAllDrawersAnyOpenStateName = "AnyOpen";
        public const string TemplateLeftDrawerGroupName = "LeftDrawer";
        public const string TemplateLeftClosedStateName = "LeftDrawerClosed";
        public const string TemplateLeftOpenStateName = "LeftDrawerOpen";
        public const string TemplateTopDrawerGroupName = "TopDrawer";
        public const string TemplateTopClosedStateName = "TopDrawerClosed";
        public const string TemplateTopOpenStateName = "TopDrawerOpen";
        public const string TemplateRightDrawerGroupName = "RightDrawer";
        public const string TemplateRightClosedStateName = "RightDrawerClosed";
        public const string TemplateRightOpenStateName = "RightDrawerOpen";
        public const string TemplateBottomDrawerGroupName = "BottomDrawer";
        public const string TemplateBottomClosedStateName = "BottomDrawerClosed";
        public const string TemplateBottomOpenStateName = "BottomDrawerOpen";

        public const string TemplateContentCoverPartName = "PART_ContentCover";
        public const string TemplateLeftDrawerPartName = "PART_LeftDrawer";
        public const string TemplateTopDrawerPartName = "PART_TopDrawer";
        public const string TemplateRightDrawerPartName = "PART_RightDrawer";
        public const string TemplateBottomDrawerPartName = "PART_BottomDrawer";

        public static RoutedCommand OpenDrawerCommand = new RoutedCommand();
        public static RoutedCommand CloseDrawerCommand = new RoutedCommand();

        private FrameworkElement _templateContentCoverElement;
        private FrameworkElement _leftDrawerElement;
        private FrameworkElement _topDrawerElement;
        private FrameworkElement _rightDrawerElement;
        private FrameworkElement _bottomDrawerElement;

        private bool _lockZIndexes;

        private readonly IDictionary<DependencyProperty, DependencyPropertyKey> _zIndexPropertyLookup = new Dictionary<DependencyProperty, DependencyPropertyKey>
        {
            { IsLeftDrawerOpenProperty, LeftDrawerZIndexPropertyKey },
            { IsTopDrawerOpenProperty, TopDrawerZIndexPropertyKey },
            { IsRightDrawerOpenProperty, RightDrawerZIndexPropertyKey },
            { IsBottomDrawerOpenProperty, BottomDrawerZIndexPropertyKey }
        };

        static Drawer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer), new FrameworkPropertyMetadata(typeof(Drawer)));
        }

        public Drawer()
        {
            CommandBindings.Add(new CommandBinding(OpenDrawerCommand, OpenDrawerHandler));
            CommandBindings.Add(new CommandBinding(CloseDrawerCommand, CloseDrawerHandler));
        }

        public static readonly DependencyProperty IsOverlayedProperty = DependencyProperty.Register(nameof(IsOverlayed), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool IsOverlayed
        {
            get => (bool)GetValue(IsOverlayedProperty);
            set => SetValue(IsOverlayedProperty, value);
        }

        public static readonly DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register(nameof(OverlayBackground), typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));
        public Brush OverlayBackground
        {
            get => (Brush)GetValue(OverlayBackgroundProperty);
            set => SetValue(OverlayBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsOverlayBackgroundEnabledProperty = DependencyProperty.Register(nameof(IsOverlayBackgroundEnabled), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool IsOverlayBackgroundEnabled
        {
            get => (bool)GetValue(IsOverlayBackgroundEnabledProperty);
            set => SetValue(IsOverlayBackgroundEnabledProperty, value);
        }

        public static readonly DependencyProperty UseTransitionsProperty = DependencyProperty.Register(nameof(UseTransitions), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool UseTransitions
        {
            get => (bool)GetValue(UseTransitionsProperty);
            set => SetValue(UseTransitionsProperty, value);
        }

        #region Top Drawer

        public static readonly DependencyProperty TopDrawerContentProperty = DependencyProperty.Register(nameof(TopDrawerContent), typeof(object), typeof(Drawer), new PropertyMetadata(default(object)));
        public object TopDrawerContent
        {
            get => GetValue(TopDrawerContentProperty);
            set => SetValue(TopDrawerContentProperty, value);
        }

        public static readonly DependencyProperty TopDrawerContentTemplateProperty = DependencyProperty.Register(nameof(TopDrawerContentTemplate), typeof(DataTemplate), typeof(Drawer), new PropertyMetadata(default(DataTemplate)));
        public DataTemplate TopDrawerContentTemplate
        {
            get => (DataTemplate)GetValue(TopDrawerContentTemplateProperty);
            set => SetValue(TopDrawerContentTemplateProperty, value);
        }

        public static readonly DependencyProperty TopDrawerContentTemplateSelectorProperty = DependencyProperty.Register(nameof(TopDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(Drawer), new PropertyMetadata(default(DataTemplateSelector)));
        public DataTemplateSelector TopDrawerContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TopDrawerContentTemplateSelectorProperty);
            set => SetValue(TopDrawerContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty TopDrawerContentStringFormatProperty = DependencyProperty.Register(nameof(TopDrawerContentStringFormat), typeof(string), typeof(Drawer), new PropertyMetadata(default(string)));
        public string TopDrawerContentStringFormat
        {
            get => (string)GetValue(TopDrawerContentStringFormatProperty);
            set => SetValue(TopDrawerContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty TopDrawerBackgroundProperty = DependencyProperty.Register(nameof(TopDrawerBackground), typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));
        public Brush TopDrawerBackground
        {
            get => (Brush)GetValue(TopDrawerBackgroundProperty);
            set => SetValue(TopDrawerBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsTopDrawerOpenProperty = DependencyProperty.Register(nameof(IsTopDrawerOpen), typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));
        public bool IsTopDrawerOpen
        {
            get => (bool)GetValue(IsTopDrawerOpenProperty);
            set => SetValue(IsTopDrawerOpenProperty, value);
        }

        private static readonly DependencyPropertyKey TopDrawerZIndexPropertyKey = DependencyProperty.RegisterReadOnly("TopDrawerZIndex", typeof(int), typeof(Drawer), new PropertyMetadata(4));
        public static readonly DependencyProperty TopDrawerZIndexProperty = TopDrawerZIndexPropertyKey.DependencyProperty;
        public int TopDrawerZIndex
        {
            get => (int)GetValue(TopDrawerZIndexProperty);
            private set => SetValue(TopDrawerZIndexPropertyKey, value);
        }

        public static readonly DependencyProperty TopDrawerCloseOnClickAwayProperty = DependencyProperty.Register(nameof(TopDrawerCloseOnClickAway), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool TopDrawerCloseOnClickAway
        {
            get => (bool)GetValue(TopDrawerCloseOnClickAwayProperty);
            set => SetValue(TopDrawerCloseOnClickAwayProperty, value);
        }
        #endregion

        #region Left Drawer

        public static readonly DependencyProperty LeftDrawerContentProperty = DependencyProperty.Register(nameof(LeftDrawerContent), typeof(object), typeof(Drawer), new PropertyMetadata(default(object)));
        public object LeftDrawerContent
        {
            get => GetValue(LeftDrawerContentProperty);
            set => SetValue(LeftDrawerContentProperty, value);
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateProperty = DependencyProperty.Register(nameof(LeftDrawerContentTemplate), typeof(DataTemplate), typeof(Drawer), new PropertyMetadata(default(DataTemplate)));
        public DataTemplate LeftDrawerContentTemplate
        {
            get => (DataTemplate)GetValue(LeftDrawerContentTemplateProperty);
            set => SetValue(LeftDrawerContentTemplateProperty, value);
        }

        public static readonly DependencyProperty LeftDrawerContentTemplateSelectorProperty = DependencyProperty.Register(nameof(LeftDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(Drawer), new PropertyMetadata(default(DataTemplateSelector)));
        public DataTemplateSelector LeftDrawerContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(LeftDrawerContentTemplateSelectorProperty);
            set => SetValue(LeftDrawerContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty LeftDrawerContentStringFormatProperty = DependencyProperty.Register(nameof(LeftDrawerContentStringFormat), typeof(string), typeof(Drawer), new PropertyMetadata(default(string)));
        public string LeftDrawerContentStringFormat
        {
            get => (string)GetValue(LeftDrawerContentStringFormatProperty);
            set => SetValue(LeftDrawerContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty LeftDrawerBackgroundProperty = DependencyProperty.Register(nameof(LeftDrawerBackground), typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));
        public Brush LeftDrawerBackground
        {
            get => (Brush)GetValue(LeftDrawerBackgroundProperty);
            set => SetValue(LeftDrawerBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsLeftDrawerOpenProperty = DependencyProperty.Register(nameof(IsLeftDrawerOpen), typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));
        public bool IsLeftDrawerOpen
        {
            get => (bool)GetValue(IsLeftDrawerOpenProperty);
            set => SetValue(IsLeftDrawerOpenProperty, value);
        }

        private static readonly DependencyPropertyKey LeftDrawerZIndexPropertyKey = DependencyProperty.RegisterReadOnly("LeftDrawerZIndex", typeof(int), typeof(Drawer), new PropertyMetadata(2));
        public static readonly DependencyProperty LeftDrawerZIndexProperty = LeftDrawerZIndexPropertyKey.DependencyProperty;
        public int LeftDrawerZIndex
        {
            get => (int)GetValue(LeftDrawerZIndexProperty);
            private set => SetValue(LeftDrawerZIndexPropertyKey, value);
        }

        public static readonly DependencyProperty LeftDrawerCloseOnClickAwayProperty = DependencyProperty.Register(nameof(LeftDrawerCloseOnClickAway), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool LeftDrawerCloseOnClickAway
        {
            get => (bool)GetValue(LeftDrawerCloseOnClickAwayProperty);
            set => SetValue(LeftDrawerCloseOnClickAwayProperty, value);
        }

        #endregion

        #region Right Drawer

        public static readonly DependencyProperty RightDrawerContentProperty = DependencyProperty.Register(nameof(RightDrawerContent), typeof(object), typeof(Drawer), new PropertyMetadata(default(object)));
        public object RightDrawerContent
        {
            get => GetValue(RightDrawerContentProperty);
            set => SetValue(RightDrawerContentProperty, value);
        }

        public static readonly DependencyProperty RightDrawerContentTemplateProperty = DependencyProperty.Register(nameof(RightDrawerContentTemplate), typeof(DataTemplate), typeof(Drawer), new PropertyMetadata(default(DataTemplate)));
        public DataTemplate RightDrawerContentTemplate
        {
            get => (DataTemplate)GetValue(RightDrawerContentTemplateProperty);
            set => SetValue(RightDrawerContentTemplateProperty, value);
        }

        public static readonly DependencyProperty RightDrawerContentTemplateSelectorProperty = DependencyProperty.Register(nameof(RightDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(Drawer), new PropertyMetadata(default(DataTemplateSelector)));
        public DataTemplateSelector RightDrawerContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(RightDrawerContentTemplateSelectorProperty);
            set => SetValue(RightDrawerContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty RightDrawerContentStringFormatProperty = DependencyProperty.Register(nameof(RightDrawerContentStringFormat), typeof(string), typeof(Drawer), new PropertyMetadata(default(string)));
        public string RightDrawerContentStringFormat
        {
            get => (string)GetValue(RightDrawerContentStringFormatProperty);
            set => SetValue(RightDrawerContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty RightDrawerBackgroundProperty = DependencyProperty.Register(nameof(RightDrawerBackground), typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));
        public Brush RightDrawerBackground
        {
            get => (Brush)GetValue(RightDrawerBackgroundProperty);
            set => SetValue(RightDrawerBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsRightDrawerOpenProperty = DependencyProperty.Register(nameof(IsRightDrawerOpen), typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));
        public bool IsRightDrawerOpen
        {
            get => (bool)GetValue(IsRightDrawerOpenProperty);
            set => SetValue(IsRightDrawerOpenProperty, value);
        }

        private static readonly DependencyPropertyKey RightDrawerZIndexPropertyKey = DependencyProperty.RegisterReadOnly("RightDrawerZIndex", typeof(int), typeof(Drawer), new PropertyMetadata(1));
        public static readonly DependencyProperty RightDrawerZIndexProperty = RightDrawerZIndexPropertyKey.DependencyProperty;
        public int RightDrawerZIndex
        {
            get => (int)GetValue(RightDrawerZIndexProperty);
            private set => SetValue(RightDrawerZIndexPropertyKey, value);
        }

        public static readonly DependencyProperty RightDrawerCloseOnClickAwayProperty = DependencyProperty.Register(nameof(RightDrawerCloseOnClickAway), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool RightDrawerCloseOnClickAway
        {
            get => (bool)GetValue(RightDrawerCloseOnClickAwayProperty);
            set => SetValue(RightDrawerCloseOnClickAwayProperty, value);
        }

        #endregion

        #region Bottom Drawer

        public static readonly DependencyProperty BottomDrawerContentProperty = DependencyProperty.Register(nameof(BottomDrawerContent), typeof(object), typeof(Drawer), new PropertyMetadata(default(object)));
        public object BottomDrawerContent
        {
            get => GetValue(BottomDrawerContentProperty);
            set => SetValue(BottomDrawerContentProperty, value);
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateProperty = DependencyProperty.Register(nameof(BottomDrawerContentTemplate), typeof(DataTemplate), typeof(Drawer), new PropertyMetadata(default(DataTemplate)));
        public DataTemplate BottomDrawerContentTemplate
        {
            get => (DataTemplate)GetValue(BottomDrawerContentTemplateProperty);
            set => SetValue(BottomDrawerContentTemplateProperty, value);
        }

        public static readonly DependencyProperty BottomDrawerContentTemplateSelectorProperty = DependencyProperty.Register(nameof(BottomDrawerContentTemplateSelector), typeof(DataTemplateSelector), typeof(Drawer), new PropertyMetadata(default(DataTemplateSelector)));
        public DataTemplateSelector BottomDrawerContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(BottomDrawerContentTemplateSelectorProperty);
            set => SetValue(BottomDrawerContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty BottomDrawerContentStringFormatProperty = DependencyProperty.Register(nameof(BottomDrawerContentStringFormat), typeof(string), typeof(Drawer), new PropertyMetadata(default(string)));
        public string BottomDrawerContentStringFormat
        {
            get => (string)GetValue(BottomDrawerContentStringFormatProperty);
            set => SetValue(BottomDrawerContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty BottomDrawerBackgroundProperty = DependencyProperty.Register(nameof(BottomDrawerBackground), typeof(Brush), typeof(Drawer), new PropertyMetadata(default(Brush)));
        public Brush BottomDrawerBackground
        {
            get => (Brush)GetValue(BottomDrawerBackgroundProperty);
            set => SetValue(BottomDrawerBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsBottomDrawerOpenProperty = DependencyProperty.Register(nameof(IsBottomDrawerOpen), typeof(bool), typeof(Drawer), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsDrawerOpenPropertyChangedCallback));
        public bool IsBottomDrawerOpen
        {
            get => (bool)GetValue(IsBottomDrawerOpenProperty);
            set => SetValue(IsBottomDrawerOpenProperty, value);
        }

        private static readonly DependencyPropertyKey BottomDrawerZIndexPropertyKey = DependencyProperty.RegisterReadOnly("BottomDrawerZIndex", typeof(int), typeof(Drawer), new PropertyMetadata(3));
        public static readonly DependencyProperty BottomDrawerZIndexProperty = BottomDrawerZIndexPropertyKey.DependencyProperty;
        public int BottomDrawerZIndex
        {
            get => (int)GetValue(BottomDrawerZIndexProperty);
            private set => SetValue(BottomDrawerZIndexPropertyKey, value);
        }

        public static readonly DependencyProperty BottomDrawerCloseOnClickAwayProperty = DependencyProperty.Register(nameof(BottomDrawerCloseOnClickAway), typeof(bool), typeof(Drawer), new PropertyMetadata(true));
        public bool BottomDrawerCloseOnClickAway
        {
            get => (bool)GetValue(BottomDrawerCloseOnClickAwayProperty);
            set => SetValue(BottomDrawerCloseOnClickAwayProperty, value);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            if (_templateContentCoverElement != null)
                _templateContentCoverElement.PreviewMouseLeftButtonUp += TemplateContentCoverElementOnPreviewMouseLeftButtonUp;
            WireDrawer(_leftDrawerElement, true);
            WireDrawer(_topDrawerElement, true);
            WireDrawer(_rightDrawerElement, true);
            WireDrawer(_bottomDrawerElement, true);

            base.OnApplyTemplate();

            _templateContentCoverElement = GetTemplateChild(TemplateContentCoverPartName) as FrameworkElement;
            if (_templateContentCoverElement != null)
                _templateContentCoverElement.PreviewMouseLeftButtonUp += TemplateContentCoverElementOnPreviewMouseLeftButtonUp;
            _leftDrawerElement = WireDrawer(GetTemplateChild(TemplateLeftDrawerPartName) as FrameworkElement, false);
            _topDrawerElement = WireDrawer(GetTemplateChild(TemplateTopDrawerPartName) as FrameworkElement, false);
            _rightDrawerElement = WireDrawer(GetTemplateChild(TemplateRightDrawerPartName) as FrameworkElement, false);
            _bottomDrawerElement = WireDrawer(GetTemplateChild(TemplateBottomDrawerPartName) as FrameworkElement, false);

            UpdateVisualStates(false);
        }

        private FrameworkElement WireDrawer(FrameworkElement drawerElement, bool unwire)
        {
            if (drawerElement == null) return null;

            if (unwire)
            {
                drawerElement.GotFocus -= DrawerElement_GotFocus;
                drawerElement.MouseDown -= DrawerElement_MouseDown;

                return drawerElement;
            }

            drawerElement.GotFocus += DrawerElement_GotFocus;
            drawerElement.MouseDown += DrawerElement_MouseDown;

            return drawerElement;
        }

        private void DrawerElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReactToFocus(sender);
        }

        private void DrawerElement_GotFocus(object sender, RoutedEventArgs e)
        {
            ReactToFocus(sender);
        }

        private void ReactToFocus(object sender)
        {
            if (sender == _leftDrawerElement)
                PrepareZIndexes(LeftDrawerZIndexPropertyKey);
            else if (sender == _topDrawerElement)
                PrepareZIndexes(TopDrawerZIndexPropertyKey);
            else if (sender == _rightDrawerElement)
                PrepareZIndexes(RightDrawerZIndexPropertyKey);
            else if (sender == _bottomDrawerElement)
                PrepareZIndexes(BottomDrawerZIndexPropertyKey);
        }

        private void TemplateContentCoverElementOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (LeftDrawerCloseOnClickAway)
                SetCurrentValue(IsLeftDrawerOpenProperty, false);
            if (RightDrawerCloseOnClickAway)
                SetCurrentValue(IsRightDrawerOpenProperty, false);
            if (TopDrawerCloseOnClickAway)
                SetCurrentValue(IsTopDrawerOpenProperty, false);
            if (BottomDrawerCloseOnClickAway)
                SetCurrentValue(IsBottomDrawerOpenProperty, false);
        }

        private void UpdateVisualStates(bool useTransitions = true)
        {
            if (IsOverlayed && IsOverlayBackgroundEnabled)
            {
                var anyOpen = IsTopDrawerOpen || IsLeftDrawerOpen || IsBottomDrawerOpen || IsRightDrawerOpen;

                VisualStateManager.GoToState(this, !anyOpen ? TemplateAllDrawersAllClosedStateName : TemplateAllDrawersAnyOpenStateName, useTransitions);
            }

            VisualStateManager.GoToState(this, IsLeftDrawerOpen ? TemplateLeftOpenStateName : TemplateLeftClosedStateName, useTransitions);

            VisualStateManager.GoToState(this, IsTopDrawerOpen ? TemplateTopOpenStateName : TemplateTopClosedStateName, useTransitions);

            VisualStateManager.GoToState(this, IsRightDrawerOpen ? TemplateRightOpenStateName : TemplateRightClosedStateName, useTransitions);

            VisualStateManager.GoToState(this, IsBottomDrawerOpen ? TemplateBottomOpenStateName : TemplateBottomClosedStateName, useTransitions);
        }

        private static void IsDrawerOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var drawer = (Drawer)dependencyObject;
            if (!drawer._lockZIndexes && (bool)dependencyPropertyChangedEventArgs.NewValue)
                drawer.PrepareZIndexes(drawer._zIndexPropertyLookup[dependencyPropertyChangedEventArgs.Property]);
            drawer.UpdateVisualStates(drawer.UseTransitions);
        }

        private void PrepareZIndexes(DependencyPropertyKey zIndexDependencyPropertyKey)
        {
            var newOrder = new[] { zIndexDependencyPropertyKey }
                .Concat(_zIndexPropertyLookup.Values.Except(new[] { zIndexDependencyPropertyKey })
                .OrderByDescending(dpk => (int)GetValue(dpk.DependencyProperty)))
                .Reverse()
                .Select((dpk, idx) => new { dpk, idx });

            foreach (var a in newOrder)
                SetValue(a.dpk, a.idx);
        }

        private void CloseDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetOpenFlag(executedRoutedEventArgs, false);

            executedRoutedEventArgs.Handled = true;
        }

        private void OpenDrawerHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Handled) return;

            SetOpenFlag(executedRoutedEventArgs, true);

            executedRoutedEventArgs.Handled = true;
        }

        private void SetOpenFlag(ExecutedRoutedEventArgs executedRoutedEventArgs, bool value)
        {
            if (executedRoutedEventArgs.Parameter is Dock)
            {
                switch ((Dock)executedRoutedEventArgs.Parameter)
                {
                    case Dock.Left:
                        SetCurrentValue(IsLeftDrawerOpenProperty, value);
                        break;
                    case Dock.Top:
                        SetCurrentValue(IsTopDrawerOpenProperty, value);
                        break;
                    case Dock.Right:
                        SetCurrentValue(IsRightDrawerOpenProperty, value);
                        break;
                    case Dock.Bottom:
                        SetCurrentValue(IsBottomDrawerOpenProperty, value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                try
                {
                    _lockZIndexes = true;
                    SetCurrentValue(IsLeftDrawerOpenProperty, value);
                    SetCurrentValue(IsTopDrawerOpenProperty, value);
                    SetCurrentValue(IsRightDrawerOpenProperty, value);
                    SetCurrentValue(IsBottomDrawerOpenProperty, value);
                }
                finally
                {
                    _lockZIndexes = false;
                }
            }
        }
    }
}
