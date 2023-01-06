using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Transitions
{
    public interface IOpacityParameter : ITransitionParameter<double>
    {
    }

    public class AnimateOpacityParameter : AnimateTransitionParameter<double>, IOpacityParameter
    {
    }
}
