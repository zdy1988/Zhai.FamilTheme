using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Zhai.Famil.Controls
{
    internal static class ToolTipExtension
    {
        public static CustomPopupPlacementCallback CustomPopupPlacementCallback => CustomPopupPlacementCallbackImpl;

        public static CustomPopupPlacement[] CustomPopupPlacementCallbackImpl(Size popupSize, Size targetSize, Point offset)
        {
            return new[]
            {
                new CustomPopupPlacement(new Point(targetSize.Width/2 - popupSize.Width/2, targetSize.Height + 6), PopupPrimaryAxis.Horizontal)
            };
        }
    }
}
