using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Zhai.FamilTheme.Transitions
{
    [MarkupExtensionReturnType(typeof(IBlurParameter))]
    public class AnimateBlurParameterExtension : AnimateTransitionParameterExtension<double>, IBlurParameter
    {
        public AnimateBlurParameterExtension() { }

        public AnimateBlurParameterExtension(double beginTime, double duration, double from, double to, EasingFunctionBase ease, EasingFunctionBase reverseEase, TransitionOn transitionOn, bool autoReverse, RepeatBehavior repeatBehavior)
            : base(beginTime, duration, from, to, ease, reverseEase, transitionOn, autoReverse, repeatBehavior)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
