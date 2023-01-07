using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class ApplicationExtensions
    {
        public static int? GetIntPtrSize(this Application application)
        {
            if (IntPtr.Size == 8)
            {
                // 64 bit machine
                return 64;
            }
            else if (IntPtr.Size == 4)
            {
                // 32 bit machine
                return 32;
            }

            return null;
        }
    }
}
