using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.FamilTheme.Transitions
{
    public interface IBlurParameter : ITransitionParameter<double>
    {
    }

    public class AnimateBlurParameter : AnimateTransitionParameter<double>, IBlurParameter
    {
    }
}
