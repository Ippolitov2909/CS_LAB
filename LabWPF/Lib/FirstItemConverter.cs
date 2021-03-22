using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Lab;
using System.Numerics;

namespace Lab
{
    [ValueConversion(typeof(Vector2), typeof(string))]
    public class FirstItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Vector2 item = (Vector2)value;
                return "vector" + item.ToString();
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}