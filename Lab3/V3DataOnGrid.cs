using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab;

namespace Lab
{
    class V3DataOnGridEnumerator : IEnumerator<DataItem>
    {
        DataItem[,] values;
        int position1 = 0;
        int position2 = -1;
        public V3DataOnGridEnumerator(double[,] values_, Grid1D x, Grid1D y)
        {
            values = new DataItem[x.num, y.num];
            for (int i = 0; i < x.num; i++)
            {
                for (int j = 0; j < y.num; j++)
                {
                    values[i, j] = new DataItem(new Vector2(i * x.step, j * y.step), values_[i, j]);
                }
            }
        }
        void IDisposable.Dispose() { }
        public bool MoveNext()
        {
            if (position2 < values.GetLength(1) - 1)
            {
                position2++;
                return true;
            }
            else if (position2 == values.GetLength(1) - 1 && position1 < values.GetLength(0) - 1)
            {
                position1++;
                position2 = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataItem Current
        {
            get
            {
                if (position1 >= 0 && position2 >= 0 && position1 <= values.GetLength(0) && position2 <= values.GetLength(1))
                    return values[position1, position2];
                {
                }
                throw new InvalidOperationException();
            }
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public void Reset()
        {
            position1 = 0;
            position2 = -1;
        }
    }
    class V3DataOnGrid : V3Data, IEnumerable<DataItem>
    {
        public Grid1D x { get; set; }
        public Grid1D y { get; set; }
        public double[,] values { get; set; }
        public V3DataOnGrid(Grid1D x_, Grid1D y_, string str_, DateTime date_time_) : base(str_, date_time_)
        {
            x = x_;
            y = y_;
            values = new double[x.num, y.num];
        }

        public IEnumerator<DataItem> GetEnumerator()
        {
            return new V3DataOnGridEnumerator(values, x, y);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void InitRandom(double minValue, double maxValue)
        {
            Random rnd = new Random();
            for (int i = 0; i < x.num; i++)
            {
                for (int j = 0; j < y.num; j++)
                {
                    values[i, j] = rnd.NextDouble() * (maxValue - minValue) + minValue;
                }
            }
        }
        public override System.Numerics.Vector2[] Nearest(System.Numerics.Vector2 v)
        {
            Vector2[] tmp = new Vector2[5];
            int count = 0;
            double mindist = Math.Pow(v.X, 2) + Math.Pow(v.Y, 2);
            for (int i = 0; i < x.num; i++)
            {
                for (int j = 0; j < y.num; j++)
                {
                    double curdist = Math.Pow(v.X - i * x.step, 2) + Math.Pow(v.Y - j * y.step, 2);
                    if (curdist < mindist)
                    {
                        count = 1;
                        tmp[0] = new Vector2(i * x.step, j * y.step);
                        mindist = curdist;
                    }
                    else if (curdist == mindist)
                    {
                        tmp[count] = new Vector2(i * x.step, j * y.step);
                        count++;
                    }
                }
            }
            System.Numerics.Vector2[] res = new Vector2[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = tmp[i];
            }
            return res;
        }
        public override string ToLongString()
        {
            string res = this.ToString();
            for (int i = 0; i < x.num; i++)
            {
                for (int j = 0; j < y.num; j++)
                {
                    res += "\n " + (i * x.step).ToString() + ' ' + (j * y.step).ToString() + ' ' + values[i, j].ToString();
                }
            }
            res += '\n';
            return res;
        }
        public override string ToString()
        {
            return "V3DataOnGrid " + base.ToString() + ' ' + x.ToString() + ' ' + y.ToString() + '\n';
        }

        public override string ToLongString(string format)
        {
            string res = "";
            foreach (DataItem elem in this)
            {
                res += elem.ToString(format) + ' ';
            }
            res += '\n';
            return res;
        }

        public override int MyCount()
        {
            return this.Count();
        }
        public override IEnumerable<DataItem> GetDataItemFrom()
        {

            return from dataitem in this select dataitem;
        }
    }

}
