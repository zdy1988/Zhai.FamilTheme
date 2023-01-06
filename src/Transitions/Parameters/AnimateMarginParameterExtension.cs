using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Transitions
{
    [MarkupExtensionReturnType(typeof(AnimateMarginParameterExtension))]
    public class AnimateMarginParameterExtension : AnimateTransitionParameterExtension<Thickness>
    {
        public AnimateMarginParameterExtension() { }

        public AnimateMarginParameterExtension(double beginTime, double duration, Thickness from, Thickness to, EasingFunctionBase ease, TransitionOn transitionOn, bool autoReverse, RepeatBehavior repeatBehavior)
            : base(beginTime, duration, from, to, ease, null, transitionOn, autoReverse, repeatBehavior)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}