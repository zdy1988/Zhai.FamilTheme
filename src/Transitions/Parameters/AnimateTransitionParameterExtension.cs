using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Zhai.Famil.Transitions
{
    public abstract class AnimateTransitionParameterExtension<T> : MarkupExtension
    {
        protected AnimateTransitionParameterExtension()
        {
            this.FillBehavior = FillBehavior.HoldEnd;
            this.TransitionOn = TransitionOn.Once;
            this.RepeatBehavior = new RepeatBehavior(0);
        }

        protected AnimateTransitionParameterExtension(double beginTime, double duration, T from, T to, EasingFunctionBase ease, EasingFunctionBase reverseEase, TransitionOn transitionOn, bool autoreverse, RepeatBehavior repeatBehavior)
            : this()
        {
            this.BeginTime = beginTime;
            this.Duration = duration;
            this.From = from;
            this.To = to;
            this.Ease = ease;
            this.ReverseEase = reverseEase;
            this.TransitionOn = transitionOn;
            this.AutoReverse = autoreverse;
            this.RepeatBehavior = repeatBehavior;
        }

        [ConstructorArgument("BeginTime")]
        public double BeginTime { get; set; }

        [ConstructorArgument("Duration")]
        public double Duration { get; set; }

        [ConstructorArgument("From")]
        public T From { get; set; }

        [ConstructorArgument("To")]
        public T To { get; set; }

        [ConstructorArgument("Ease")]
        public EasingFunctionBase Ease { get; set; }

        [ConstructorArgument("ReverseEase")]
        public EasingFunctionBase ReverseEase { get; set; }

        [ConstructorArgument("TransitionOn")]
        public TransitionOn TransitionOn { get; set; }

        public FillBehavior FillBehavior { get; set; }

        [ConstructorArgument("AutoReverse")]
        public bool AutoReverse { get; set; }

        [ConstructorArgument("RepeatBehavior")]
        public RepeatBehavior RepeatBehavior { get; set; }
    }
}