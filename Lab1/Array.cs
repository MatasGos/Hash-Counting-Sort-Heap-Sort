using System;
using System.Collections.Generic;
using System.Text;

namespace Sort
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
        public void buildHeap(int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            // If left child is larger than root 
            if (l < n && data[l] > data[largest])
                largest = l;

            // If right child is larger than largest so far 
            if (r < n && data[r] > data[largest])
                largest = r;

            // If largest is not root 
            if (largest != i)
            {
                double swap = data[i];
                data[i] = data[largest];
                data[largest] = swap;

                // Recursively heapify the affected sub-tree 
                buildHeap(n, largest);
            }
        }
    }

}
