using System;
using System.Drawing;
using Zhai.Famil.Common.Helpers;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class MathExtensions
    {

        /// <summary>
        /// 剪裁指定的值，使其不超过最小值或最大值。
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The clipped value.</returns>
        public static double Clip(this double value, double minValue, double maxValue) => Math.Min(Math.Max(value, minValue), maxValue);

        /// <summary>
        /// 剪裁指定的值，使其不超过最小值或最大值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The clipped value.</returns>
        public static float Clip(this float value, float minValue, float maxValue) => Math.Min(Math.Max(value, minValue), maxValue);

        /// <summary>
        /// 剪裁指定的值，使其不超过最小值或最大值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The clipped value.</returns>
        public static int Clip(this int value, int minValue, int maxValue) => Math.Min(Math.Max(value, minValue), maxValue);

        /// <summary>
        /// 剪裁指定的值，使其不超过最小值或最大值
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>The clipped value.</returns>
        public static long Clip(this long value, long minValue, long maxValue) => Math.Min(Math.Max(value, minValue), maxValue);

        public static bool AreClose(this double value1, double value2) => MathHelper.AreClose(value1, value2);

        public static bool IsVerySmall(this double value) => MathHelper.IsVerySmall(value);

        public static bool IsFiniteDouble(this double x) => MathHelper.IsFiniteDouble(x);

        public static bool GreaterThan(this double value1, double value2) => MathHelper.GreaterThan(value1, value2);

        public static bool GreaterThanOrClose(this double value1, double value2) => MathHelper.GreaterThanOrClose(value1, value2);

        public static bool LessThan(this double value1, double value2) => MathHelper.LessThan(value1, value2);

        public static bool LessThanOrClose(this double value1, double value2) => MathHelper.LessThanOrClose(value1, value2);

        public static double EnsureRange(this double value, double? min, double? max) => MathHelper.EnsureRange(value, min, max);

        public static double SafeDivide(this double lhs, double rhs, double fallback) => MathHelper.SafeDivide(lhs, rhs, fallback);

        public static double CalulateRotateAngle(this Point mp1, Point mp2) => MathHelper.CalulateRotateAngle(mp1.X, mp1.Y, mp2.X, mp2.Y);

        public static double CalulateXYAnagle(this Point mp1, Point mp2) => MathHelper.CalulateXYAnagle(mp1.X, mp1.Y, mp2.X, mp2.Y);

        public static double PolyLineDistance(this Point mp1, Point mp2) => MathHelper.PolyLineDistance(mp1, mp2);
    }
}