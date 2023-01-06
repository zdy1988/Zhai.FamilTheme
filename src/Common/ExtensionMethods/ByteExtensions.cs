using System;
using System.Collections.Generic;
using System.Text;
using Zhai.Famil.Common.Helpers;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class ByteExtensions
    {
        public static string ToFileSizeString(this long size)
        {
            return ByteHelper.FormatFileSizeString(size);
        }
    }
}
