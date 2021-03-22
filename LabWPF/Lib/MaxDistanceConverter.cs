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
    [ValueConversion(typeof(float), typeof(string))]
    public class MaxDistanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                double item = (float)value;
                return "MaxDistance in V3Main collection: " + item.ToString();
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}