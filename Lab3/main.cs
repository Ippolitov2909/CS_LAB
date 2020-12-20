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





class TestClass
{
    static int Main()
    {
        try
        {
            V3MainCollection example = new V3MainCollection();
            example.DataChanged += V3MainCollectionChangedHandler;
            V3DataOnGrid don1 = new V3DataOnGrid(new Grid1D(5.0f, 0), new Grid1D(5.0f, 0), "a", new DateTime(2020, 11, 20));
            V3DataCollection dc1 = new V3DataCollection("a", new DateTime(2020, 11, 20));
            V3DataOnGrid don2 = new V3DataOnGrid(new Grid1D(0.5f, 6), new Grid1D(0.5f, 6), "aa", new DateTime(2020, 11, 20));
            don2.InitRandom(0.0f, 1.0f);
            example.add(don1);
            example.add(dc1);
            example.add(don2);
            example[0] = dc1;
            example.Remove("aa", new DateTime(2020, 11, 20));
            example[1].Str = "new_value_of_test_string";

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return 0;
    }
    public static void V3MainCollectionChangedHandler(object source, DataChangedEventArgs args)
    {
        Console.WriteLine(args.ToString() + "=====\n");
    }
}

