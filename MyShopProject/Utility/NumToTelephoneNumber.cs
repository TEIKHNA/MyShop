using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Utility
{
    public class NumToTelephoneNumber : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string phone = (string)value;
            if (phone != null && phone.Length > 0)
            {
                if (phone.Length == 10)
                {
                    Regex regex = new Regex(@"[^\d]");
                    phone = regex.Replace(phone, "");
                    phone = Regex.Replace(phone, @"(\d{4})(\d{3})(\d{3})", "$1-$2-$3");
                    return phone;
                }
                return phone;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
