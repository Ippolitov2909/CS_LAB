using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Lab;

namespace Lab
{
    [ValueConversion(typeof(V3DataOnGrid), typeof(string))]
    public class GridConverter: IValueConverter
    {
        int a;
        public GridConverter() { a = 10; }
        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                V3DataOnGrid item = (V3DataOnGrid)value;
                return "nodes on Ox: " + item.x.num.ToString() + "\nnodes on Oy: " + item.y.num.ToString();
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
