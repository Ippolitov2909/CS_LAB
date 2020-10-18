
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;


struct DataItem
{
    public System.Numerics.Vector2 vec { get; set; }
    public double value { get; set; }
    public DataItem(System.Numerics.Vector2 vec_, double value_) { vec = vec_; value = value_; }
    public override string ToString()
    {
        return vec.X.ToString() + ' ' + vec.Y.ToString() + " value " + value.ToString();
    }
}
struct Grid1D
{
    public float step { get; set; }
    public int num { get; set; }
    public Grid1D(float step_, int num_) { step = step_; num = num_; }
    public override string ToString()
    {
        return "step " + step.ToString() + "\nnum " + num.ToString();
    }
}
abstract class V3Data
{
    public string str { get; set; }
    public DateTime date_time { get; set; }
    public V3Data(string str_, DateTime date_time_) 
    {
        str = str_; date_time = date_time_;
    }
    public abstract System.Numerics.Vector2[] Nearest(System.Numerics.Vector2 v);
    public abstract string ToLongString();
    public override string ToString()
    {
        return str + ' ' + date_time.ToString();
    }
}

class V3DataOnGrid : V3Data
{
    public Grid1D x { get; set; }
    public Grid1D y { get; set; }
    public double[,] values { get; set; }
    public V3DataOnGrid(Grid1D x_, Grid1D y_, string str_, DateTime date_time_): base (str_, date_time_)
    {
        x = x_;
        y = y_;
        values = new double[x.num, y.num];
    }
    public void InitRandom (double minValue, double maxValue)
    {
        Random rnd = new Random();
        for (int i = 0; i < x.num; i++)
        {
            for (int j = 0; j < y.num; j++)
            {
                values[i, j] = rnd.NextDouble()*(maxValue - minValue) + minValue;
            }
        }
    }
    public override System.Numerics.Vector2[] Nearest (System.Numerics.Vector2 v)
    {
        Vector2[] tmp = new Vector2[5];
        int count = 0;
        double mindist = Math.Pow(v.X, 2) + Math.Pow(v.Y, 2);
        for (int i = 0; i < x.num; i++)
        {
            for (int j = 0; j < y.num;j++)
            {
                double curdist = Math.Pow(v.X - i * x.step, 2) + Math.Pow(v.Y - j * y.step, 2);
                if (curdist < mindist)
                {
                    count = 1;
                    tmp[0] = new Vector2(i * x.step, j * y.step);
                    mindist = curdist;
                } else if (curdist == mindist)
                {
                    tmp[count] = new Vector2(i * x.step, j * y.step);
                    count++;
                }
            }
        }
        Vector2[] res = new Vector2[count];
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
        return "V3DataOnGrid " + base.ToString() + ' ' +  x.ToString() + ' ' + y.ToString();
    }
}

class V3DataCollection: V3Data
{
    public System.Collections.Generic.List<DataItem> collect { get; set; }
    public V3DataCollection (string str_, DateTime date_time_): base(str_, date_time_)
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
        foreach(DataItem current in collect)
        {
            double curdist = Math.Pow(v.X - current.vec.X, 2) + Math.Pow(v.Y - current.vec.Y, 2);
            int cmp = curdist.CompareTo(mindist);
            if (cmp == 0)
            {
                res[count] = current.vec;
                count++;
            } else if (cmp < 0)
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
class V3MainCollection: IEnumerable
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
        foreach(V3Data cur in collect)
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
        int n_elems = 4;
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
        foreach(V3Data cur in collect)
        {
            res += cur.ToString();
        }
        return res;
    }
}

class TextClass
{
    static int Main()
    {
        V3DataOnGrid dog = new V3DataOnGrid(new Grid1D(0.5f, 10), new Grid1D(0.5f, 10), "first_example", new DateTime(2020, 10, 10));
        dog.InitRandom(0.0f, 10.0f);
        Console.WriteLine(dog.ToLongString());
        V3DataCollection dc = (V3DataCollection)dog;
        Console.WriteLine(dc.ToLongString());
        V3MainCollection mc = new V3MainCollection();
        mc.AddDefaults();
        Vector2 vec = new Vector2(1, 2.5f);
        foreach(V3Data cur in mc)
        {
            Vector2[] res = cur.Nearest(vec);
            foreach(Vector2 elem in res)
            {
                Console.WriteLine(elem);
            }
            Console.WriteLine(res);
        }
        return 0;
    }

}

