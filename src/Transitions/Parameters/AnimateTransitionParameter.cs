using System;
using System.Globalization;
using System.Windows.Media.Animation;

namespace Zhai.FamilTheme.Transitions
{
    [Flags]
    public enum TransitionOn
    {
        Once = 0x1,
        Loaded = 0x2,
        DataContextChanged = 0x4,
        Visibility = 0x8
    }

    public interface ITransitionParameter<T>
    {
        double BeginTime { get; set; }
        double Duration { get; set; }
        T From { get; set; }
        T To { get; set; }
        EasingFunctionBase Ease { get; set; }
        EasingFunctionBase ReverseEase { get; set; }
        FillBehavior FillBehavior { get; set; }
        TransitionOn TransitionOn { get; set; }
        bool AutoReverse { get; set; }
        RepeatBehavior RepeatBehavior { get; set; }
    }

    public class AnimateTransitionParameter<T>
    {
        public AnimateTransitionParameter()
        {
            this.Duration = 300;
            this.FillBehavior = FillBehavior.HoldEnd;
            this.TransitionOn = TransitionOn.Once;
        }

        public AnimateTransitionParameter(double beginTime, double duration, T from, T to, TransitionOn transitionOn, bool autoReverse, RepeatBehavior repeatBehavior)
            : this()
        {
            this.BeginTime = beginTime;
            this.Duration = duration;
            this.From = from;
            this.To = to;
            this.TransitionOn = transitionOn;
            this.AutoReverse = autoReverse;
            this.RepeatBehavior = repeatBehavior;
        }

        public EasingFunctionBase Ease { get; set; }
        public EasingFunctionBase ReverseEase { get; set; }
        public TransitionOn TransitionOn { get; set; }
        public double BeginTime { get; set; }
        public double Duration { get; set; }
        public T From { get; set; }
        public T To { get; set; }
        public FillBehavior FillBehavior { get; set; }
        public bool AutoReverse { get; set; }
        public RepeatBehavior RepeatBehavior { get; set; }

        protected static double ToDouble(string str)
        {
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}