using System;
using Zhai.Famil.Common.Media;

namespace Zhai.Famil.Controls
{
    internal class ColorSelectorHSL : ColorSelectorBase
    {
        protected override Tuple<double, double, double> ConvertColorSpace(double x, double y, double z)
        {
            return ColorSpaceConverter.HslToRgb(x, y, z);
        }
    }
}
