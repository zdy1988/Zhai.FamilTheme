using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Zhai.FamilTheme.Transitions
{
    public interface ITranslateParams
    {
        double BeginTime { get; set; }
        double Duration { get; set; }
        Point From { get; set; }
        Point To { get; set; }
        EasingFunctionBase Ease { get; set; }
        EasingFunctionBase ReverseEase { get; set; }
        FillBehavior FillBehavior { get; set; }
        TransitionOn TransitionOn { get; set; }
        bool AutoReverse { get; set; }
        RepeatBehavior RepeatBehavior { get; set; }
    }

    public class AnimateTranslateParameter : ITranslateParams
    {
        public double BeginTime { get; set; }
        public double Duration { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
        public EasingFunctionBase Ease { get; set; }
        public EasingFunctionBase ReverseEase { get; set; }
        public FillBehavior FillBehavior { get; set; }
        public TransitionOn TransitionOn { get; set; }
        public bool AutoReverse { get; set; }
        public RepeatBehavior RepeatBehavior { get; set; }
    }
}
