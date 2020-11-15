
using System;
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





class TextClass
{
    static int Main()
    {
        V3DataOnGrid dog = new V3DataOnGrid(new Grid1D(0.5f, 3), new Grid1D(0.5f, 3), "first_example", new DateTime(2020, 10, 10));
        dog.InitRandom(0.0f, 10.0f);
        //Console.WriteLine(dog.ToLongString());
        V3DataCollection dc = (V3DataCollection)dog;
        //Console.WriteLine(dc.ToLongString());
        V3MainCollection mc = new V3MainCollection();
        mc.AddDefaults();
        Console.WriteLine(mc.ToString());
        //Vector2 vec = new Vector2(1, 2.5f);
        Vector2 vec = new Vector2(0.25f, 0.25f);

        foreach (V3Data cur in mc)
        {
            Vector2[] res = cur.Nearest(vec);
            foreach (Vector2 elem in res)
            {
                Console.WriteLine(elem);
            }
            Console.WriteLine('\n');
            Console.WriteLine(cur.ToLongString());
            Console.WriteLine('\n');
        }
        Console.WriteLine("\n=====");
        mc.Remove("aa", new DateTime(2020, 10, 10));
        Console.WriteLine(mc.ToString());

        return 0;
    }

}

