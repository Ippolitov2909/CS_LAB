﻿using System;
using System.Collections;
using System.Collections.Generic;
using Lab;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{

    class V3MainCollection : IEnumerable
    {
        private System.Collections.Generic.List<V3Data> collect;
        public int Count
        {
            get
            {
                return collect.Count;
            }
        }
        public V3MainCollection()
        {
            collect = new List<V3Data>();
        }
        public IEnumerator GetEnumerator()
        {
            return collect.GetEnumerator();
        }

        void add(V3Data item)
        {
            collect.Add(item);
        }
        bool Remove(string id, DateTime date)
        {
            bool res = false;
            foreach (V3Data cur in collect)
            {
                if (cur.str == id && cur.date_time == date)
                {
                    collect.Remove(cur);
                    res = true;
                }
            }
            return res;
        }
        public void AddDefaults()
        {
            int n_elems = 3;
            string str = "a";
            for (int i = 0; i < n_elems; i++)
            {
                float step = i * 0.5f + 0.5f;
                str += "a";
                V3DataOnGrid new1 = new V3DataOnGrid(new Grid1D(step, 5), new Grid1D(step, 5), str, new DateTime(2020, 10, 10));
                V3DataCollection new2 = new V3DataCollection(str, new DateTime(2020, 10, 10));
                new1.InitRandom(0.0f, 10.0f);
                new2.InitRandom(5, step * 5, step * 5, 0.0f, 10.0f);
                collect.Add(new1);
                collect.Add(new2);
            }
        }
        public override string ToString()
        {
            string res = "";
            foreach (V3Data cur in collect)
            {
                res += cur.ToString();
            }
            return res;
        }
    }

}