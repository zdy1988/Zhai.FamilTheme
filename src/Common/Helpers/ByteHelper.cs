using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Common.Helpers
{
    public static class ByteHelper
    {
        public static string FormatFileSizeString(long size)
        {
            string outFileSize;

            if (size < 1024L)
            {
                outFileSize = String.Format("{0}B", size);
            }
            else if (size < 1024L * 1024L)
            {
                outFileSize = String.Format("{0}KB", size / 1024L);
            }
            else if (size < 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}MB", size / (1024L * 1024L));
            }
            else if (size < 1024L * 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}GB", Math.Round(size / (1024D * 1024D * 1024D), 2));
            }
            else if (size < 1024L * 1024L * 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}TB", Math.Round(size / (1024D * 1024D * 1024D * 1024D), 2));
            }
            else if (size < 1024L * 1024L * 1024L * 1024L * 1024L * 1024L)
            {
                outFileSize = String.Format("{0}PB", Math.Round(size / (1024D * 1024D * 1024D * 1024D * 1024D), 2));
            }
            else
            {
                outFileSize = String.Format("{0}EB", Math.Round(size / (1024D * 1024D * 1024D * 1024D * 1024D * 1024D), 2));
            }

            return outFileSize;
        }
    }
}
