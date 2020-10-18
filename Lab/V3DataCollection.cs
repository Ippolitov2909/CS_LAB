using System;
using Lab;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class V3DataCollection : V3Data
{
    public System.Collections.Generic.List<DataItem> collect { get; set; }
    public V3DataCollection(string str_, DateTime date_time_) : base(str_, date_time_)
    {
        collect = new System.Collections.Generic.List<DataItem>();
    }
    public static explicit operator V3DataCollection(V3DataOnGrid old)
    {
        V3DataCollection res = new V3DataCollection(old.str, old.date_time);
        float stepx = old.x.step;
        float stepy = old.y.step;
        for (int i = 0; i < old.x.num; i++)
        {
            for (int j = 0; j < old.y.num; j++)
            {
                res.collect.Add(new DataItem(new Vector2(i * stepx, j * stepy), old.values[i, j]));
            }
        }
        return res;
    }
    public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
    {
        Random rnd = new Random();
        for (int i = 0; i < nItems; i++)
        {
            float randx = (float)rnd.NextDouble() * xmax;
            float randy = (float)rnd.NextDouble() * ymax;
            double randvalue = rnd.NextDouble() * (maxValue - minValue) + minValue;
            collect.Add(new DataItem(new System.Numerics.Vector2(randx, randy), randvalue));
        }
    }
    public override Vector2[] Nearest(Vector2 v)
    {
        Vector2[] res = new Vector2[this.collect.Count];
        int count = 0;
        double mindist = Math.Pow(v.X - collect[0].vec.X, 2) + Math.Pow(v.Y - collect[0].vec.Y, 2);
        foreach (DataItem current in collect)
        {
            double curdist = Math.Pow(v.X - current.vec.X, 2) + Math.Pow(v.Y - current.vec.Y, 2);
            int cmp = curdist.CompareTo(mindist);
            if (cmp == 0)
            {
                res[count] = current.vec;
                count++;
            }
            else if (cmp < 0)
            {
                res[0] = current.vec;
                count = 1;
                mindist = curdist;
            }
        }
        Vector2[] final = new Vector2[count];
        for (int i = 0; i < count; i++)
        {
            final[i] = res[i];
        }
        return final;
    }
    public override string ToString()
    {
        return "V3DataCollection " + base.ToString() + ' ' + collect.Count.ToString();
    }
    public override string ToLongString()
    {
        string res = this.ToString();
        foreach (DataItem cur in collect)
        {
            res += '\n' + cur.ToString();
        }
        return res;
    }
}