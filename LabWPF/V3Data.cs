using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab
{
    [Serializable]
    public abstract class V3Data : INotifyPropertyChanged
    {
        private string str;
        private DateTime date_time;
        public string Str
        {
            get { return str; }
            set
            {
                str = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("new value of Str: " + str + '\n'));
            }
        }
        public DateTime Date_time
        {
            get
            {
                return date_time;
            }
            set
            {
                date_time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("new value of Time: " + date_time + '\n'));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
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
