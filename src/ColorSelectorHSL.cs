using System;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    internal class ColorSelectorHSL: ColorSelectorBase
    {
        protected override Tuple<double, double, double> ConvertColorSpace(double x, double y, double z)
        {
            return ColorSpaceConverter.HslToRgb(x, y, z);
        }
    }
}
