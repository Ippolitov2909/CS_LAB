using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    abstract class V3Data
    {
        public string str { get; set; }
        public DateTime date_time { get; set; }
        public V3Data(string str_, DateTime date_time_)
        {
            str = str_; date_time = date_time_;
        }
        public V3Data()
        {

        }
        public abstract System.Numerics.Vector2[] Nearest(System.Numerics.Vector2 v);
        public abstract string ToLongString();
        public override string ToString()
        {
            return str + ' ' + date_time.ToString();
        }
        public abstract string ToLongString(string format);
        public abstract int MyCount();
        public abstract IEnumerable<DataItem> GetDataItemFrom();
    }

}
