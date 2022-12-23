using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Zhai.FamilTheme
{
    public class ColorEyeDropper : Button
    {
        static ColorEyeDropper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorEyeDropper), new FrameworkPropertyMetadata(typeof(ColorEyeDropper)));
        }

        public static readonly DependencyProperty PreviewImageOuterPixelCountProperty = DependencyProperty.Register(nameof(PreviewImageOuterPixelCount), typeof(int), typeof(ColorEyeDropper), new PropertyMetadata(2));
        public int PreviewImageOuterPixelCount
        {
            get => (int)this.GetValue(PreviewImageOuterPixelCountProperty);
            set => this.SetValue(PreviewImageOuterPixelCountProperty, value);
        }

        public static readonly DependencyProperty PreviewContentTemplateProperty = DependencyProperty.Register(nameof(PreviewContentTemplate), typeof(DataTemplate), typeof(ColorEyeDropper), new PropertyMetadata(default(DataTemplate)));
        public DataTemplate PreviewContentTemplate
        {
            get => (DataTemplate)this.GetValue(PreviewContentTemplateProperty);
            set => this.SetValue(PreviewContentTemplateProperty, value);
        }

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorEyeDropper), new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColorPropertyChanged));
        public Color SelectedColor
        {
            get => (Color)this.GetValue(SelectedColorProperty);
            set => this.SetValue(SelectedColorProperty, value);
        }

        private static readonly DependencyPropertyKey SelectedColourPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedColour), typeof(string), typeof(ColorEyeDropper), new PropertyMetadata("#000000"));
        public static readonly DependencyProperty SelectedColourProperty = SelectedColourPropertyKey.DependencyProperty;
        public string SelectedColour
        {
            get => (string)GetValue(SelectedColourProperty);
            private set => SetValue(SelectedColourPropertyKey, value);
        }

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(SelectedColorChanged), RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorEyeDropper));
        public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
        {
            add => this.AddHandler(SelectedColorChangedEvent, value);
            remove => this.RemoveHandler(SelectedColorChangedEvent, value);
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is ColorEyeDropper colorEyeDropper)
            {
                colorEyeDropper.RaiseEvent(new RoutedPropertyChangedEventArgs<Color?>((Color?)args.OldValue, (Color?)args.NewValue, SelectedColorChangedEvent));
            }
        }

        internal ColorEyePreviewImage PreviewImage = new ColorEyePreviewImage();
        private ToolTip? PreviewToolTip;
        private DispatcherOperation? CurrentTask;

        private void SetPreview(Point mousePos)
        {
            this.PreviewToolTip?.Move(mousePos, new Point(16, 16));

            if (this.CurrentTask?.Status == DispatcherOperationStatus.Executing || this.CurrentTask?.Status == DispatcherOperationStatus.Pending)
            {
                this.CurrentTask.Abort();
            }

            var action = new Action(() =>
            {
                mousePos = this.PointToScreen(mousePos);
                var outerPixelCount = this.PreviewImageOuterPixelCount;
                var posX = (int)Math.Round(mousePos.X - outerPixelCount);
                var posY = (int)Math.Round(mousePos.Y - outerPixelCount);
                var region = new Int32Rect(posX, posY, 2 * outerPixelCount + 1, 2 * outerPixelCount + 1);
                var previewImage = EyeDropperHelper.CaptureRegion(region);
                var previewBrush = new SolidColorBrush(EyeDropperHelper.GetPixelColor(mousePos));
                previewBrush.Freeze();

                this.PreviewImage.SetValue(ColorEyePreviewImage.PreviewImageSourcePropertyKey, previewImage);
                this.PreviewImage.SetValue(ColorEyePreviewImage.PreviewSelectedColorBrushPropertyKey, previewBrush);
            });

            this.CurrentTask = this.Dispatcher.BeginInvoke(DispatcherPriority.Background, action);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            Mouse.Capture(this);

            this.PreviewToolTip ??= ColorEyePreview.GetPreviewToolTip(this);

            this.PreviewToolTip.Show();

            this.Cursor = Cursors.Pen;

            SetPreview(e.GetPosition(this));
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            Mouse.Capture(null);

            this.PreviewToolTip?.Hide();

            this.Cursor = Cursors.Arrow;;

            var newColor = EyeDropperHelper.GetPixelColor(this.PointToScreen(e.GetPosition(this)));

            this.SelectedColor = newColor;

            this.SelectedColour = Convert.ToString(newColor);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SetPreview(e.GetPosition(this));
            }
        }
    }

    internal static class EyeDropperHelper
    {
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nxDest, int nyDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int nHeight);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        private const int SRCCOPY = 0x00CC0020;
        private const int CAPTUREBLT = 0x40000000;

        public static BitmapSource CaptureRegion(Int32Rect region)
        {
            BitmapSource result;

            IntPtr desktophWnd = GetDesktopWindow();
            IntPtr desktopDc = GetWindowDC(desktophWnd);
            IntPtr memoryDc = CreateCompatibleDC(desktopDc);
            IntPtr bitmap = CreateCompatibleBitmap(desktopDc, region.Width, region.Height);
            IntPtr oldBitmap = SelectObject(memoryDc, bitmap);

            bool success = BitBlt(memoryDc, 0, 0, region.Width, region.Height, desktopDc, region.X, region.Y, SRCCOPY | CAPTUREBLT);

            try
            {
                if (!success)
                {
                    throw new Win32Exception();
                }

                result = Imaging.CreateBitmapSourceFromHBitmap(bitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                SelectObject(memoryDc, oldBitmap);
                DeleteObject(bitmap);
                DeleteDC(memoryDc);
                ReleaseDC(desktophWnd, desktopDc);
            }

            result.Freeze();
            return result;
        }

        public static Color GetPixelColor(Point point)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, (int)Math.Round(point.X), (int)Math.Round(point.Y));
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromRgb((byte)(pixel & 0x000000FF),
                                        (byte)((pixel & 0x0000FF00) >> 8),
                                        (byte)((pixel & 0x00FF0000) >> 16));
            return color;
        }
    }
}