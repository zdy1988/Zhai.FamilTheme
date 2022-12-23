using System.Windows;
using System.Windows.Media;

namespace Zhai.FamilTheme
{
    public class ColorEyePreviewImage : DependencyObject
    {
        internal static readonly DependencyPropertyKey PreviewImageSourcePropertyKey = DependencyProperty.RegisterReadOnly(nameof(PreviewImageSource), typeof(ImageSource), typeof(ColorEyePreviewImage), new PropertyMetadata(default(ImageSource)));
        public static readonly DependencyProperty PreviewImageSourceProperty = PreviewImageSourcePropertyKey.DependencyProperty;
        public ImageSource PreviewImageSource => (ImageSource)this.GetValue(PreviewImageSourceProperty);

        internal static readonly DependencyPropertyKey PreviewSelectedColorBrushPropertyKey = DependencyProperty.RegisterReadOnly(nameof(PreviewSelectedColorBrush), typeof(Brush), typeof(ColorEyePreviewImage), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty PreviewSelectedColorBrushProperty = PreviewSelectedColorBrushPropertyKey.DependencyProperty;
        public Brush PreviewSelectedColorBrush => (Brush)this.GetValue(PreviewSelectedColorBrushProperty);
    }
}