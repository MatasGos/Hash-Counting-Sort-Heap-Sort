using System;
using System.IO;
using System.Diagnostics;

namespace Sort
{
    class HeapSort
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            //Test_Array(seed);
            Test_List(seed);
        }
        public static void Test_Array(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray();

            myArray.Print(myArray.Length);
            for (int i = myArray.Length/2-1; i >= 0; i--)
            {
                myArray.buildHeap(myArray.Length, i);
            }
            for (int i = myArray.Length - 1; i >= 0; i--)
            {
                myArray.Swap(i);
                myArray.buildHeap(i, 0);
            }
            Console.WriteLine("Done");
            myArray.Print(myArray.Length);

        }
        public static void Test_List(int seed)
        {
            int n = 12;
            MyDataList myList = new MyDataList(n, seed);
            myList.Print(myList.Length);
            for (int i = myList.Length / 2 - 1; i >= 0; i--)
            {
                myList.buildHeap(myList.Length, i);
            }
            for (int i = myList.Length - 1; i >= 0; i--)
            {
                myList.Swap(i);
                myList.buildHeap(i, 0);
            }
            Console.WriteLine("Done");
            myList.Print(myList.Length);
        }

    }
    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int k);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(" {0:F5} ", this[i]);
            }
        }
    }
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length;  } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(int n);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5}", Next());
            Console.WriteLine();
        }
    }
}
