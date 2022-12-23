using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.FamilTheme.Transitions
{
    public interface IOpacityParameter : ITransitionParameter<double>
    {
    }

    public class AnimateOpacityParameter : AnimateTransitionParameter<double>, IOpacityParameter
    {
    }
}
