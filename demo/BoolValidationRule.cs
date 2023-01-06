using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Zhai.Famil.Demo
{
    public class BoolValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value is bool b && b
                ? new ValidationResult(false, "数据提示！")
                : ValidationResult.ValidResult;
        }
    }
}
