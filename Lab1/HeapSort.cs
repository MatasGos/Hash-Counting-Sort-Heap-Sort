using System;
using System.IO;
using System.Diagnostics;

namespace Sort
{
    class Sort
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            //Test_Array(seed);
            //Test_List(seed);
            Console.WriteLine("Array test");
            for (int i = 2; i < 64; i=i*2)
            {
                Test_Array(seed,i*1000);
            }
            Console.WriteLine("List test");
            for (int i = 2; i < 64; i = i * 2)
            {
                Test_List(seed,i*1000);
            }
        }
        public static void Test_Array(int seed, int n)
        {
            var stopwatch = new Stopwatch();
            MyDataArray myArray = new MyDataArray(n, seed);
            stopwatch.Start();
            HeapSort(myArray);
            stopwatch.Stop();
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed, int n)
        {
            MyDataList myList = new MyDataList(n, seed);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            HeapSort(myList);
            stopwatch.Stop();
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_Array(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray();

            myArray.Print(myArray.Length);
            HeapSort(myArray);
            myArray.Print(myArray.Length);

        }
        public static void Test_List(int seed)
        {
            int n = 12;
            MyDataList myList = new MyDataList(n, seed);
            myList.Print(myList.Length);
            HeapSort(myList);
            myList.Print(myList.Length);
        }
        public static void HeapSort(MyDataList myList)
        {
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
        }
        public static void HeapSort(MyDataArray myArray)
        {
            for (int i = myArray.Length / 2 - 1; i >= 0; i--)
            {
                myArray.buildHeap(myArray.Length, i);
            }
            for (int i = myArray.Length - 1; i >= 0; i--)
            {
                myArray.Swap(i);
                myArray.buildHeap(i, 0);
            }
            Console.WriteLine("Done");
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
