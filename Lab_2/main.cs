
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Lab;

struct DataItem
{
    public System.Numerics.Vector2 vec { get; set; }
    public double value { get; set; }
    public DataItem(System.Numerics.Vector2 vec_, double value_) { vec = vec_; value = value_; }
    public override string ToString()
    {
        return vec.X.ToString() + ' ' + vec.Y.ToString() + " value " + value.ToString();
    }
    public string ToString(string format)
    {
        string res = vec.ToString(format) + "   value : " + value.ToString(format) + "\n";
        return res;
    }
}
struct Grid1D
{
    public float step { get; set; }
    public int num { get; set; }
    public Grid1D(float step_, int num_) { step = step_; num = num_; }
    public override string ToString()
    {
        return "step " + step.ToString() + "\nnum " + num.ToString() + '\n';
    }
    public string ToString(string format)
    {
        string res = "step: " + step.ToString(format) + "\nnum: " + num.ToString() + '\n';
        return res;
    }
}





class TextClass
{
    static int Main()
    {
        try
        {
            V3DataOnGrid dog = new V3DataOnGrid(new Grid1D(0.5f, 5), new Grid1D(0.6f, 3), "first_example", new DateTime(2020, 10, 10));
            dog.InitRandom(0.0f, 10.0f);
            Console.WriteLine(dog.ToLongString());
            V3DataCollection dc = (V3DataCollection)dog;
            Console.WriteLine(dc.ToLongString());
            V3DataCollection test = new V3DataCollection("../../test.txt");
            Console.WriteLine(test.ToLongString("F4"));
            V3MainCollection mc = new V3MainCollection();
            mc.AddDefaults();
            Console.WriteLine(mc.ToString());
            Console.WriteLine("max_count: " + mc.max_count.ToString());
            Console.WriteLine("max_distance: " + mc.max_distance.ToString());
            var high_freq = (from dataitem in mc.high_freq_DataItem select dataitem.ToString());
            string res = "";
            foreach (DataItem dataitem in mc.high_freq_DataItem)
            {
                res += dataitem.ToString() + "\n";
            }
            Console.WriteLine("high_freq_DataItem: " + res);
            Console.WriteLine("______\n");
            foreach (DataItem dataitem in dc)
            {
                Console.WriteLine(dataitem.ToString() + "\n");
            }
            Console.WriteLine("_____\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return 0;
    }
}

