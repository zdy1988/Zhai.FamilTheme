using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Transitions
{
    public interface IBlurParameter : ITransitionParameter<double>
    {
    }

    public class AnimateBlurParameter : AnimateTransitionParameter<double>, IBlurParameter
    {
    }
}
