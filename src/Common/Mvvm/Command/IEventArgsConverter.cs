using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Common.Mvvm.Command
{
    public interface IEventArgsConverter
    {
        object Convert(object value, object parameter);
    }
}
