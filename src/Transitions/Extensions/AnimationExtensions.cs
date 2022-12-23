using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Media.Animation
{
    internal static class AnimationExtensions
    {
        internal static void SetDesiredFrameRate(this Timeline animation, int? fps)
        {
            Timeline.SetDesiredFrameRate(animation, fps);
        }
    }
}
