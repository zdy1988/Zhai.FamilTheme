using System;
using System.Drawing;

namespace Zhai.Famil.Common.Helpers
{
    public static class MathHelper
    {
        public static bool AreClose(double value1, double value2) =>
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            value1 == value2 || IsVerySmall(value1 - value2);

        public static double Lerp(double x, double y, double alpha) => x * (1.0 - alpha) + y * alpha;

        public static bool IsVerySmall(double value) => Math.Abs(value) < 1E-06;

        public static bool IsFiniteDouble(double x) => !double.IsInfinity(x) && !double.IsNaN(x);

        public static double DoubleFromMantissaAndExponent(double x, int exp) => x * Math.Pow(2.0, exp);

        public static bool GreaterThan(double value1, double value2) => value1 > value2 && !AreClose(value1, value2);

        public static bool GreaterThanOrClose(double value1, double value2)
        {
            if (value1 <= value2)
            {
                return AreClose(value1, value2);
            }
            return true;
        }

        public static double Hypotenuse(double x, double y) => Math.Sqrt(x * x + y * y);

        public static bool LessThan(double value1, double value2) => value1 < value2 && !AreClose(value1, value2);

        public static bool LessThanOrClose(double value1, double value2)
        {
            if (value1 >= value2)
            {
                return AreClose(value1, value2);
            }
            return true;
        }

        public static double EnsureRange(double value, double? min, double? max)
        {
            if (min.HasValue && value < min.Value)
            {
                return min.Value;
            }
            if (max.HasValue && value > max.Value)
            {
                return max.Value;
            }
            return value;
        }

        public static double SafeDivide(double lhs, double rhs, double fallback)
        {
            if (!IsVerySmall(rhs))
            {
                return lhs / rhs;
            }
            return fallback;
        }

        /// <summary>
        /// 两点计算角度
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public static double CalulateRotateAngle(double startX, double startY, double endX, double endY)
        {
            double angle;
            var tan = Math.Atan2(Math.Abs(startX - endX), Math.Abs(startY - endY));
            if (endX >= startX && endY >= startY) //第一象限
                angle = tan;
            else if (endX >= startX && endY < startY) //第四象限
                angle = Math.PI - tan;
            else if (endX < startX && endY >= startY) //第二象限
                angle = 2 * Math.PI - tan;
            else //第三象限
                angle = Math.PI + tan;
            angle = angle * 180 / Math.PI;
            return angle;
        }

        /// <summary>
        /// 两点计算角度
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public static double CalulateXYAnagle(double startX, double startY, double endX, double endY)
        {
            //除数不能为0
            var tan = Math.Atan(Math.Abs((endY - startY) / (endX - startX))) * 180 / Math.PI;
            if (endX > startX && endY > startY) //第一象限
                return -tan;
            if (endX > startX && endY < startY) //第二象限
                return tan;
            if (endX < startX && endY > startY) //第三象限
                return tan - 180;
            return 180 - tan;
        }

        /// <summary>
        /// 获取两个点之间的距离
        /// </summary>
        /// <param name="mp1"></param>
        /// <param name="mp2"></param>
        /// <returns></returns>
        public static double PolyLineDistance(Point mp1, Point mp2)
        {
            return Math.Sqrt(Math.Pow(mp1.X - mp2.X, 2) + Math.Pow(mp1.Y - mp2.Y, 2));
        }
    }
}
