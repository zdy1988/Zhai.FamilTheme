using System;

namespace Zhai.Famil.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static string DateStringFromNow(DateTime datetime)
        {
            TimeSpan span = DateTime.Now - datetime;
            if (span.TotalDays > 60)
            {
                return datetime.ToShortDateString();
            }
            else
            if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else
            if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else
            if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else
            if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else
            if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else
            if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else
            if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }

        }

        public static string DateDiff(DateTime dateTime1, DateTime dateTime2)
        {
            TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            string dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="dateTime1">当前日期</param>
        /// <param name="dateTime2">输入日期</param>
        /// <param name="n">比较天数</param>
        /// <returns>大于天数返回true，小于返回false</returns>
        public static bool DateTimeCompare(string dateTime1, string dateTime2, int n)
        {
            DateTime DateTime1 = Convert.ToDateTime(dateTime1);
            DateTime DateTime2 = Convert.ToDateTime(dateTime2);
            DateTime2 = DateTime2.AddDays(n);

            if (DateTime1 >= DateTime2)
                return false;
            else
                return true;
        }
    }
}
