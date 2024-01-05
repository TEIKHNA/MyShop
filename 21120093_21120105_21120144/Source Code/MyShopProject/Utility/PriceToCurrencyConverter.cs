using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Utility
{
    public class PriceToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int price = (int)value;
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string vnPrice = price.ToString("#,###đ", cul.NumberFormat);
            if (price == 0)
            {
                vnPrice = "0,000đ";
            }
            return vnPrice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
