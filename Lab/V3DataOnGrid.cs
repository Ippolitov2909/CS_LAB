using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab;

namespace Lab
{
    class V3DataOnGrid : V3Data
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
                    res += "\n " + i.ToString() + ' ' + j.ToString() + ' ' + values[i, j].ToString();
                }
            }
            return res;
        }
        public override string ToString()
        {
            return "V3DataOnGrid " + base.ToString() + ' ' + x.ToString() + ' ' + y.ToString();
        }
    }

}
