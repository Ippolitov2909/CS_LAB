using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.ComponentModel;
using Lab;

namespace WpfApp1
{
    public class CustomDataCollection: IDataErrorInfo, INotifyPropertyChanged
    {
        public V3DataCollection collect;
        public float x;
        public float y;
        public double val;
        public event PropertyChangedEventHandler PropertyChanged;
        public CustomDataCollection(V3DataCollection collect_)
        {
            collect = collect_;
            x = 0;
            y = 0;
            val = 0;
        }
        public float X {
            get { return x; }
            set
            {
                x = value;
                PropertyChanged(this, new PropertyChangedEventArgs("X"));
                PropertyChanged(this, new PropertyChangedEventArgs("Y"));
            }
        }
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                PropertyChanged(this, new PropertyChangedEventArgs("X"));
                PropertyChanged(this, new PropertyChangedEventArgs("Y"));
            }
        }
        public double Val
        {
            get { return val; }
            set
            {
                val = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Val"));
            }
        }
        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "X":
                    case "Y":
                        if (collect != null)
                        {
                            foreach (DataItem item in collect)
                            {
                                if ((item.vec.X == x) && (item.vec.Y == y))
                                {
                                    msg = "Pair x and y should be unique in V3DataCollection";
                                    break;
                                }
                            }
                        }
                        break;
                    case "Val":
                        if (val <= 0) msg = "val should be > 0";
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }
        public void AddDataitem()
        {
            collect.collect.Add(new DataItem(new Vector2(x, y), Val));
            
        }
    }
}
