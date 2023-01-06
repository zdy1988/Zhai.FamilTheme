using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class MediaExtensions
    {
        public static string ZeroDurationString { get; } = TimeSpan.Zero.ToString(@"mm\:ss");

        public static string ToDurationString(this long ticks)
        {
            if (ticks <= 0)
            {
                return ZeroDurationString;
            }
            else if (ticks > 0 && ticks <= TimeSpan.TicksPerMinute * 60)
            {
                return new TimeSpan(ticks).ToString(@"mm\:ss");
            }
            else if (ticks > TimeSpan.TicksPerMinute * 60 && ticks <= TimeSpan.TicksPerMinute * 60 * 24)
            {
                return new TimeSpan(ticks).ToString(@"hh\:mm\:ss");
            }
            else if (ticks <= TimeSpan.TicksPerMinute * 60 * 24)
            {
                return new TimeSpan(ticks).ToString(@"dd\.hh\:mm\:ss");
            }

            return ZeroDurationString;
        }

        public static string ToMinuteString(this long ticks)
        {
            var timeSpan = new TimeSpan(ticks);

            var minutes = timeSpan.TotalMinutes;

            if (minutes >= 1)
            {
                return $"{(int)minutes} 分钟";
            }
            else
            {
                return $"{(int)timeSpan.TotalSeconds} 秒";
            }
        }

        public static string ToFrameSizeString(this Size size)
        {
            if (size != null)
            {
                var frame = size.Width * size.Height;

                if (frame <= 640 * 480)
                {
                    return "标清";
                }
                else if (frame <= 1024 * 720)
                {
                    return "高清";
                }
                else if (frame <= 1920 * 1080)
                {
                    return "全高清";
                }
                else if (frame < 3840 * 2160)
                {
                    return "全高清";
                }
                else if (frame >= 3840 * 2160)
                {
                    return "4K超清";
                }
                else if (frame >= 3840 * 2160 * 2)
                {
                    return "8K超清";
                }
                else
                {
                    return "极清";
                }
            }

            return string.Empty;
        }
    }
}
