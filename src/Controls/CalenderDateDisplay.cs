using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Zhai.Famil.Common.ExtensionMethods;

namespace Zhai.Famil.Controls
{
    internal class CalenderDateDisplay : Control
    {
        public static readonly DependencyProperty DisplayDateProperty = DependencyProperty.Register(nameof(DisplayDate), typeof(DateTime), typeof(CalenderDateDisplay), new PropertyMetadata(default(DateTime), DisplayDatePropertyChangedCallback, DisplayDateCoerceValue));
        public DateTime DisplayDate
        {
            get { return (DateTime)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }

        private static readonly DependencyPropertyKey ComponentOneContentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ComponentOneContent), typeof(string), typeof(CalenderDateDisplay), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ComponentOneContentProperty = ComponentOneContentPropertyKey.DependencyProperty;
        public string ComponentOneContent
        {
            get => (string)GetValue(ComponentOneContentProperty);
            private set => SetValue(ComponentOneContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey ComponentTwoContentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ComponentTwoContent), typeof(string), typeof(CalenderDateDisplay), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ComponentTwoContentProperty = ComponentTwoContentPropertyKey.DependencyProperty;
        public string ComponentTwoContent
        {
            get => (string)GetValue(ComponentTwoContentProperty);
            private set => SetValue(ComponentTwoContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey ComponentThreeContentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(ComponentThreeContent), typeof(string), typeof(CalenderDateDisplay), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ComponentThreeContentProperty = ComponentThreeContentPropertyKey.DependencyProperty;
        public string ComponentThreeContent
        {
            get => (string)GetValue(ComponentThreeContentProperty);
            private set => SetValue(ComponentThreeContentPropertyKey, value);
        }

        private static readonly DependencyPropertyKey IsDayInFirstComponentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsDayInFirstComponent), typeof(bool), typeof(CalenderDateDisplay), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty IsDayInFirstComponentProperty = IsDayInFirstComponentPropertyKey.DependencyProperty;
        public bool IsDayInFirstComponent
        {
            get => (bool)GetValue(IsDayInFirstComponentProperty);
            private set => SetValue(IsDayInFirstComponentPropertyKey, value);
        }


        public CalenderDateDisplay()
        {
            SetCurrentValue(DisplayDateProperty, DateTime.Today);
        }

        private static object DisplayDateCoerceValue(DependencyObject d, object baseValue)
        {
            if (d is FrameworkElement element &&
                element.Language.GetSpecificCulture() is CultureInfo culture &&
                baseValue is DateTime displayDate)
            {
                if (displayDate < culture.Calendar.MinSupportedDateTime)
                {
                    return culture.Calendar.MinSupportedDateTime;
                }
                if (displayDate > culture.Calendar.MaxSupportedDateTime)
                {
                    return culture.Calendar.MaxSupportedDateTime;
                }
            }
            return baseValue;
        }

        private static void DisplayDatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((CalenderDateDisplay)dependencyObject).UpdateComponents();
        }

        private void UpdateComponents()
        {
            var culture = Language.GetSpecificCulture();
            var dateTimeFormatInfo = GetDateFormat(culture);
            var minDateTime = dateTimeFormatInfo.Calendar.MinSupportedDateTime;
            var maxDateTime = dateTimeFormatInfo.Calendar.MaxSupportedDateTime;

            if (DisplayDate < minDateTime)
            {
                SetDisplayDateOfCalendar(minDateTime);

                // return to avoid second formatting of the same value
                return;
            }

            if (DisplayDate > maxDateTime)
            {
                SetDisplayDateOfCalendar(maxDateTime);

                // return to avoid second formatting of the same value
                return;
            }

            var calendarFormatInfo = CalendarFormatInfo.FromCultureInfo(culture);
            var displayDate = DisplayDate;
            ComponentOneContent = FormatDate(calendarFormatInfo.ComponentOnePattern, displayDate, culture);
            ComponentTwoContent = FormatDate(calendarFormatInfo.ComponentTwoPattern, displayDate, culture);
            ComponentThreeContent = FormatDate(calendarFormatInfo.ComponentThreePattern, displayDate, culture);
        }

        private static string FormatDate(string format, DateTime displayDate, CultureInfo culture)
        {
            string separator = " ";
            var text = string.IsNullOrEmpty(format) ? string.Empty : displayDate.ToString(format, culture);

            TextInfo textInfo = culture.TextInfo;

            string lowerText = textInfo.ToLower(text);
            string[] words = lowerText.Split(new[] { separator }, StringSplitOptions.None);

            return string.Join(separator, words.Select(v => textInfo.ToTitleCase(v)));
        }

        private void SetDisplayDateOfCalendar(DateTime displayDate)
        {
            Calendar calendarControl = this.GetVisualAncestry().OfType<Calendar>().FirstOrDefault();

            if (calendarControl != null)
            {
                calendarControl.DisplayDate = displayDate;
            }
        }

        private DateTimeFormatInfo GetDateFormat(CultureInfo culture)
        {
            if (culture is null) throw new ArgumentNullException(nameof(culture));

            if (culture.Calendar is GregorianCalendar || culture.Calendar is PersianCalendar)
            {
                return culture.DateTimeFormat;
            }

            GregorianCalendar foundCal = null;
            foreach (var cal in culture.OptionalCalendars.OfType<GregorianCalendar>())
            {
                // Return the first Gregorian calendar with CalendarType == Localized 
                // Otherwise return the first Gregorian calendar
                if (foundCal is null)
                {
                    foundCal = cal;
                }

                if (cal.CalendarType != GregorianCalendarTypes.Localized) continue;

                foundCal = cal;
                break;
            }


            DateTimeFormatInfo dtfi;
            if (foundCal is null)
            {
                // if there are no GregorianCalendars in the OptionalCalendars list, use the invariant dtfi 
                dtfi = ((CultureInfo)CultureInfo.InvariantCulture.Clone()).DateTimeFormat;
                dtfi.Calendar = new GregorianCalendar();
            }
            else
            {
                dtfi = ((CultureInfo)culture.Clone()).DateTimeFormat;
                dtfi.Calendar = foundCal;
            }

            return dtfi;
        }
    }

    /// <summary>
    /// Provides culture-specific information about the format of calendar.
    /// </summary>
    internal class CalendarFormatInfo
    {
        /// <summary>
        /// Gets the custom format string for a year and month value.
        /// </summary>
        public string YearMonthPattern { get; }

        /// <summary>
        /// Gets the custom format string for a component one value.
        /// </summary>
        public string ComponentOnePattern { get; }

        /// <summary>
        /// Gets the custom format string for a component two value.
        /// </summary>
        public string ComponentTwoPattern { get; }

        /// <summary>
        /// Gets the custom format string for a component three value.
        /// </summary>
        public string ComponentThreePattern { get; }

        private const string ShortDayOfWeek = "ddd";
        private const string LongDayOfWeek = "dddd";

        private static readonly Dictionary<string, CalendarFormatInfo> _formatInfoCache = new Dictionary<string, CalendarFormatInfo>();
        private static readonly Dictionary<string, string> _cultureYearPatterns = new Dictionary<string, string>();
        private static readonly Dictionary<string, DayOfWeekStyle> _cultureDayOfWeekStyles = new Dictionary<string, DayOfWeekStyle>();

        private static readonly string[] JapaneseCultureNames = { "ja", "ja-JP" };
        private static readonly string[] ZhongwenCultureNames = { "zh", "zh-CN", "zh-Hans", "zh-Hans-HK", "zh-Hans-MO", "zh-Hant", "zh-HK", "zh-MO", "zh-SG", "zh-TW" };
        private static readonly string[] KoreanCultureNames = { "ko", "ko-KR", "ko-KP" };

        private const string CJKYearSuffix = "\u5e74";
        private const string KoreanYearSuffix = "\ub144";

        static CalendarFormatInfo()
        {
            SetYearPattern(JapaneseCultureNames, "yyyy" + CJKYearSuffix);
            SetYearPattern(ZhongwenCultureNames, "yyyy" + CJKYearSuffix);
            SetYearPattern(KoreanCultureNames, "yyyy" + KoreanYearSuffix);

            var dayOfWeekStyle = new DayOfWeekStyle(LongDayOfWeek, string.Empty, false);
            SetDayOfWeekStyle(JapaneseCultureNames, dayOfWeekStyle);
            SetDayOfWeekStyle(ZhongwenCultureNames, dayOfWeekStyle);
        }

        /// <summary>
        /// Sets the culture-specific custom format string for a year value. 
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to set the <paramref name="yearPattern"/> for.</param>
        /// <param name="yearPattern">The custom format string for a year value. If null, culture-specific custom format string for a year value is removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void SetYearPattern(string[] cultureNames, string yearPattern)
        {
            if (cultureNames is null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
            {
                SetYearPattern(cultureName, yearPattern);
            }
        }

        /// <summary>
        /// Sets the culture-specific custom format string for a year value.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to set the <paramref name="yearPattern"/> for.</param>
        /// <param name="yearPattern">The custom format string for a year value. If null, culture-specific custom format string for a year value is removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void SetYearPattern(string cultureName, string yearPattern)
        {
            if (cultureName is null)
                throw new ArgumentNullException(nameof(cultureName));

            if (yearPattern != null)
            {
                _cultureYearPatterns[cultureName] = yearPattern;
            }
            else
            {
                _cultureYearPatterns.Remove(cultureName);
            }
            DiscardFormatInfoCache(cultureName);
        }

        /// <summary>
        /// Sets the culture-specific day of week style.
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to set the <paramref name="dayOfWeekStyle"/> for.</param>
        /// <param name="dayOfWeekStyle">A <see cref="DayOfWeekStyle"/> to be set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void SetDayOfWeekStyle(string[] cultureNames, DayOfWeekStyle dayOfWeekStyle)
        {
            if (cultureNames is null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
            {
                SetDayOfWeekStyle(cultureName, dayOfWeekStyle);
            }
        }

        /// <summary>
        /// Sets the culture-specific day of week style.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to set the <paramref name="dayOfWeekStyle"/> for.</param>
        /// <param name="dayOfWeekStyle">A <see cref="DayOfWeekStyle"/> to be set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void SetDayOfWeekStyle(string cultureName, DayOfWeekStyle dayOfWeekStyle)
        {
            if (cultureName is null)
                throw new ArgumentNullException(nameof(cultureName));

            _cultureDayOfWeekStyles[cultureName] = dayOfWeekStyle;
            DiscardFormatInfoCache(cultureName);
        }

        /// <summary>
        /// Resets the culture-specific day of week style to default value.
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to reset.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void ResetDayOfWeekStyle(string[] cultureNames)
        {
            if (cultureNames is null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
            {
                ResetDayOfWeekStyle(cultureName);
            }
        }

        /// <summary>
        /// Resets the culture-specific day of week style to default value.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to reset.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void ResetDayOfWeekStyle(string cultureName)
        {
            if (cultureName is null)
                throw new ArgumentNullException(nameof(cultureName));

            if (_cultureDayOfWeekStyles.Remove(cultureName))
            {
                DiscardFormatInfoCache(cultureName);
            }
        }

        private static void DiscardFormatInfoCache(string cultureName)
            => _ = _formatInfoCache.Remove(cultureName);

        private CalendarFormatInfo(string yearMonthPattern, string componentOnePattern, string componentTwoPattern, string componentThreePattern)
        {
            YearMonthPattern = yearMonthPattern;
            ComponentOnePattern = componentOnePattern;
            ComponentTwoPattern = componentTwoPattern;
            ComponentThreePattern = componentThreePattern;
        }

        /// <summary>
        /// Creates a <see cref="CalendarFormatInfo"/> from the <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="cultureInfo">A <see cref="CultureInfo"/> that specifies the culture to get the date format.</param>
        /// <returns>The <see cref="CalendarFormatInfo"/> object that this method creates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cultureInfo"/> is null.</exception>
        public static CalendarFormatInfo FromCultureInfo(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
                throw new ArgumentNullException(nameof(cultureInfo));

            if (_formatInfoCache.TryGetValue(cultureInfo.Name, out var calendarInfo))
                return calendarInfo;

            var dateTimeFormat = cultureInfo.DateTimeFormat;

            if (!_cultureYearPatterns.TryGetValue(cultureInfo.Name, out string yearPattern))
                yearPattern = "yyyy";

            DayOfWeekStyle dayOfWeekStyle;
            if (!_cultureDayOfWeekStyles.TryGetValue(cultureInfo.Name, out dayOfWeekStyle))
                dayOfWeekStyle = DayOfWeekStyle.Parse(dateTimeFormat.LongDatePattern);

            var monthDayPattern = dateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM");
            if (dayOfWeekStyle.IsFirst)
            {
                calendarInfo = new CalendarFormatInfo(dateTimeFormat.YearMonthPattern,
                                                      monthDayPattern,
                                                      dayOfWeekStyle.Pattern + dayOfWeekStyle.Separator,
                                                      yearPattern);
            }
            else
            {
                calendarInfo = new CalendarFormatInfo(dateTimeFormat.YearMonthPattern,
                                                      dayOfWeekStyle.Pattern,
                                                      monthDayPattern + dayOfWeekStyle.Separator,
                                                      yearPattern);
            }
            _formatInfoCache[cultureInfo.Name] = calendarInfo;
            return calendarInfo;
        }

        /// <summary>
        /// Represents a day of week style.
        /// </summary>
        public struct DayOfWeekStyle
        {
            /// <summary>
            /// Gets the custom format string for a day of week value.
            /// </summary>
            public string Pattern { get; }

            /// <summary>
            /// Gets the string that separates MonthDay and DayOfWeek.
            /// </summary>
            public string Separator { get; }

            /// <summary>
            /// Gets a value indicating whether DayOfWeek is before MonthDay.
            /// </summary>
            public bool IsFirst { get; }

            private const string EthiopicWordspace = "\u1361";
            private const string EthiopicComma = "\u1363";
            private const string EthiopicColon = "\u1365";
            private const string ArabicComma = "\u060c";

            private const string SeparatorChars = "," + ArabicComma + EthiopicWordspace + EthiopicComma + EthiopicColon;

            /// <summary>
            /// Initializes a new instance of the <see cref="DayOfWeekStyle"/> struct.
            /// </summary>
            /// <param name="pattern">A custom format string for a day of week value.</param>
            /// <param name="separator">A string that separates MonthDay and DayOfWeek.</param>
            /// <param name="isFirst">A value indicating whether DayOfWeek is before MonthDay.</param>
            public DayOfWeekStyle(string pattern, string separator, bool isFirst)
            {
                Pattern = pattern ?? string.Empty;
                Separator = separator ?? string.Empty;
                IsFirst = isFirst;
            }

            /// <summary>
            /// Extracts the <see cref="DayOfWeekStyle"/> from the date format string.
            /// </summary>
            /// <param name="s">the date format string.</param>
            /// <returns>The <see cref="DayOfWeekStyle"/> struct.</returns>
            /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
            public static DayOfWeekStyle Parse(string s)
            {
                if (s is null)
                    throw new ArgumentNullException(nameof(s));

                if (s.StartsWith(ShortDayOfWeek, StringComparison.Ordinal))
                {
                    var index = 3;
                    if (index < s.Length && s[index] == 'd')
                        index++;
                    for (; index < s.Length && IsSpace(s[index]); index++)
                        ;
                    var separator = index < s.Length && IsSeparator(s[index]) ? s[index].ToString() : string.Empty;
                    return new DayOfWeekStyle(ShortDayOfWeek, separator, true);
                }
                else if (s.EndsWith(ShortDayOfWeek, StringComparison.Ordinal))
                {
                    var index = s.Length - 4;
                    if (index >= 0 && s[index] == 'd')
                        index--;
                    for (; index >= 0 && IsSpace(s[index]); index--)
                        ;
                    var separator = index >= 0 && IsSeparator(s[index]) ? s[index].ToString() : string.Empty;
                    return new DayOfWeekStyle(ShortDayOfWeek, separator, false);
                }
                return new DayOfWeekStyle(ShortDayOfWeek, string.Empty, true);

                static bool IsSpace(char c) => c == ' ' || c == '\'';

                static bool IsSeparator(char c) => SeparatorChars.IndexOf(c) >= 0;
            }
        }
    }
}
