using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Utility
{
    public class RequiredRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || ((string)value).Length == 0)
            {
                return new ValidationResult(false,$"*Required");
            }

            return ValidationResult.ValidResult;
        }
    }
}
