using System;
using System.Collections.Generic;
using System.Text;

namespace Counting
{
    class MyDataArray : DataArray
    {
    int[] data;
        public MyDataArray(int n, int seed, int max)
        {
            data = new int[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
                data[i] =rand.Next(255);
        }
        public MyDataArray()
        {
            data = new int[] { 5, 9, 11, 20, 4, 1, 8, 13 };
            length = data.Length;
        }
        public override int this [int index]
        {
            get { return data[index]; }
        }
        public override void Swap(int i, int a)
        {
            data[i] = a;
        }
    }

}
