using System;
using System.IO;
using System.Diagnostics;

namespace SortD
{
    class Sort
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            //Console.WriteLine("Array");
            //Test_Array(seed);
            //Console.WriteLine("Linked");
            //Test_List(seed);

            Console.WriteLine("Array test");
            for (int i = 2; i < 64; i = i * 2)
            {
                Test_Array(seed, i * 25);
            }
            Console.WriteLine("List test");
            for (int i = 2; i < 64; i = i * 2)
            {
                Test_List(seed, i * 25);
            }
        }
        public static void Test_Array(int seed, int n)
        {
            string filename = @"mydataarray.dat";
            var stopwatch = new Stopwatch();
            MyFileArray myArray = new MyFileArray(filename, n, seed);
            using (myArray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                stopwatch.Start();
                HeapSort(myArray);
                stopwatch.Stop();
            }
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed, int n)
        {
            string filename = @"mydatalist.dat";
            MyFileList myList = new MyFileList(filename, n, seed);
            var stopwatch = new Stopwatch();
            using (myList.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                stopwatch.Start();
                HeapSort(myList);
                stopwatch.Stop();
            }
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed)
        {
            int n = 12;
            string filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                HeapSort(myfilelist);
                myfilelist.Print(n);
            }
        }
        public static void Test_Array(int seed)
        {
            int n = 12;
            string filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                HeapSort(myfilearray);
                myfilearray.Print(n);
            }
        }
        public static void HeapSort(MyFileList myList)
        {
            for (int i = myList.Length / 2 - 1; i >= 0; i--)
            {
                myList.Head();
                Heapify(myList.Length, i, ref myList);
            }
            for (int i = myList.Length - 1; i >= 0; i--)
            {
                double head = myList.Head();
                double end = head;
                for (int j = 0; j < i; j++)
                {
                    end = myList.Next();
                }
                myList.Head();
                myList.Swap(0, i, head, end );
                myList.Head();
                Heapify(i, 0, ref myList);
            }
            Console.WriteLine("Done");
        }
        public static void HeapSort(MyFileArray myArray)
        {
            for (int i = myArray.Length / 2 - 1; i >= 0; i--)
            {
                myArray.buildHeap(myArray.Length, i);
            }
            for (int i = myArray.Length - 1; i >= 0; i--)
            {
                myArray.Swap(i, myArray[0], myArray[i]);
                myArray.buildHeap(i, 0);
            }
            Console.WriteLine("Done");
        }
        public static void Heapify(int n, int i, ref MyFileList myList)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            double current = myList.Head();

            int largestIndex = i;
            while (largest > 0)
            {
                current = myList.Next();
                largest--;
            }
            double left = myList.Head();           
            largest = i;
            double rootData = current;
            double largestData = current;
            // suranda kaires reiksme
            if (l < n)
            {
                int leftIndex = l;
                while (leftIndex > 0)
                {
                    left = myList.Next();
                    leftIndex--;
                }
            }
            double right = myList.Head();
            // suranda desines reiksme
            if (r < n)
            {
                int rightIndex = r;
                while (rightIndex > 0)
                {
                    right = myList.Next();
                    rightIndex--;
                }
            }

            if (r < n)
            {
                if (left > right)
                {
                    if (current < left)
                    {
                        largest = l;
                        myList.Head();
                        myList.Swap(largest, i, left, current);
                    }

                }
                else
                {
                    if (current < right)
                    {
                        largest = r;
                        myList.Head();
                        myList.Swap(largest, i, right, current);
                    }
                }
            }
            else if (l < n)
            {

                if (current < left)
                {
                    largest = l;
                    myList.Head();
                    myList.Swap(largest, i, left, current);
                }
            }
            // If largest is not root 
            if (largest != i)
            {
                Heapify(n, largest, ref myList);
            }
        }

    }
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(int i, int j, double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for
           (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();

        }
    }
    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract double this[int index] { get; }
        public abstract void Swap(int i,int j, double a, double b);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0:F5} ", this[i]);
            Console.WriteLine();
        }
    }
}
