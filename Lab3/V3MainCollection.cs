using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Lab;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab
{
    enum ChangeInfo
    {
        ItemChanged,
        Add,
        Remove,
        Replace,
    }
    class DataChangedEventArgs
    {
        public ChangeInfo info { get; set; }
        public String str { get; set; }
        public DataChangedEventArgs( ChangeInfo info_, string str_) { info = info_;  str = str_; }
        public override String ToString()
        {
            return "string: " + str + "ChangeInfo: " + info + '\n';
        }
    }
    delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);
    class V3MainCollection : IEnumerable<V3Data>
    {
        private System.Collections.Generic.List<V3Data> collect;
        public int Count
        {
            get
            {
                return collect.Count;
            }
        }
        public event DataChangedEventHandler DataChanged;
        public void V3DataChangedHandler(object Source, PropertyChangedEventArgs args)
        {
            DataChanged?.Invoke(this, new DataChangedEventArgs(ChangeInfo.ItemChanged, args.PropertyName));
        }
        public V3MainCollection()
        {
            collect = new List<V3Data>();
        }
        public IEnumerator<V3Data> GetEnumerator()
        {
            return collect.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collect.GetEnumerator();
        }
        public int max_count
        {
            get
            {
                return this.Max(v3Data => v3Data.MyCount());
            }
        }
        public float max_distance
        {
            get
            {
                IEnumerable<DataItem> query = from v3data in this from dataitem in v3data.GetDataItemFrom() select dataitem;
                IEnumerable<float> query_of_distances = from dataitem1 in query from dataitem2 in query select Vector2.Distance(dataitem1.vec, dataitem2.vec);
                return query_of_distances.Max();
            }
        }
        public IEnumerable<DataItem> high_freq_DataItem
        {
            get
            {
                var query_of_dataitems = from v3data in this from dataitem in v3data.GetDataItemFrom() select dataitem;
                return from dataitem in query_of_dataitems where (query_of_dataitems.Where(dataitem_in_query => dataitem_in_query.vec == dataitem.vec).Count() > 1) select dataitem;
            }
        }
        public void add(V3Data item)
        {
            string old_count = this.Count.ToString();
            collect.Add(item);
            string current_count = this.Count.ToString();
            DataChanged?.Invoke(this, new DataChangedEventArgs(ChangeInfo.Add, "number of elements before adding: " + old_count + "\ncurrent number of elements: " + current_count + '\n'));
            item.PropertyChanged += V3DataChangedHandler;
        }
        public bool Remove(string id, DateTime date)
        {
            int before = collect.Count();
            foreach(V3Data elem in collect)
            {
                if (elem.Str == id && elem.Date_time == date)
                {
                    elem.PropertyChanged -= V3DataChangedHandler;
                }
            }
            bool res = collect.RemoveAll((V3Data elem) => elem.Str == id && elem.Date_time == date) > 0;
            DataChanged?.Invoke(this, new DataChangedEventArgs(ChangeInfo.Remove, "number of elements before deleting: " + before.ToString() + " \ncurrent number of elenments: " + collect.Count().ToString() + '\n'));
            return res;
        }
        public V3Data this[int index]
        {
            get
            {
                return collect[index];
            }
            set
            {
                collect[index] = value;
                DataChanged?.Invoke(this, new DataChangedEventArgs(ChangeInfo.Replace, "Replaced element at position " + index.ToString() + '\n'));
            }
        }
        public void AddDefaults()
        {
            V3DataOnGrid don1 = new V3DataOnGrid(new Grid1D(5.0f, 0), new Grid1D(5.0f, 0), "a", new DateTime(2020, 11, 20));
            V3DataCollection dc1 = new V3DataCollection("a", new DateTime(2020, 11, 20));
            V3DataOnGrid don2 = new V3DataOnGrid(new Grid1D(0.5f, 6), new Grid1D(0.5f, 6), "aa", new DateTime(2020, 11, 20));
            don2.InitRandom(0.0f, 1.0f);
            V3DataCollection dc2 = new V3DataCollection("aa", new DateTime(2020, 11, 20));
            dc2.InitRandom(10, 3.0f, 3.0f, 0.0f, 1.0f);
            V3DataOnGrid don3 = new V3DataOnGrid(new Grid1D(1.0f, 5), new Grid1D(0.5f, 7), "aaa", new DateTime(2020, 11, 20));
            don3.InitRandom(0.0f, 1.0f);
            V3DataCollection dc3 = (V3DataCollection)(don3);
            collect.Add(don1);
            collect.Add(dc1);
            collect.Add(don2);
            collect.Add(dc2);
            collect.Add(don3);
            collect.Add(dc3);
        }
        public override string ToString()
        {
            string res = "V3MainCollection";
            foreach (V3Data cur in collect)
            {
                res += cur.ToString();
            }
            res += " end of V3MainCollection";
            return res;
        }
        public string ToLongString(string format)
        {
            string res = "V3MainCollection";
            foreach (V3Data cur in collect)
            {
                res += cur.ToLongString(format);
            }
            res += " end of V3MainCollection";
            return res;

        }
    }

}
