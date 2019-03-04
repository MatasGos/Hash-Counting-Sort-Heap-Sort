using System;
using System.Collections.Generic;
using System.Text;

namespace Counting
{
    class MyDataArray : DataArray
    {
    double[] data;
        public MyDataArray(int n, int seed)
        {
            data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
                data[i] = rand.NextDouble();
        }
        public MyDataArray()
        {
            data = new double[] { 5, 9, 11, 20, 4, 19.2, 8, 13 };
            length = data.Length;
        }
        public override double this [int index]
        {
            get { return data[index]; }
        }
        public override void Swap(int k)
        {
            double swap = data[k];
            data[k]=data[0];
            data[0] = swap;
        }
    }

}
