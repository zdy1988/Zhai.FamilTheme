using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Transitions
{
    [MarkupExtensionReturnType(typeof(ITranslateParams))]
    public class AnimateTranslateParameterExtension : AnimateTransitionParameterExtension<Point>, ITranslateParams
    {
        public AnimateTranslateParameterExtension()
        {
        }

        public AnimateTranslateParameterExtension(double beginTime, double duration, Point from, Point to, EasingFunctionBase ease, EasingFunctionBase reverseEase, TransitionOn transitionOn, bool autoReverse, RepeatBehavior repeatBehavior)
            : base(beginTime, duration, from, to, ease, reverseEase, transitionOn, autoReverse, repeatBehavior)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
