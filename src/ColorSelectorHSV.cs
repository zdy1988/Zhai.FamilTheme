using System;
using Zhai.FamilTheme.Common;

namespace Zhai.FamilTheme
{
    internal class ColorSelectorHSV : ColorSelectorBase
    {
        protected override Tuple<double, double, double> ConvertColorSpace(double x, double y, double z)
        {
            return ColorSpaceConverter.HsvToRgb(x, y, z);
        }
    }
}
