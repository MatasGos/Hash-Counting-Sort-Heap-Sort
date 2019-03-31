using System;
using System.IO;
using System.Diagnostics;

namespace CountingD
{
    class Program
    {
        public const int max = 10;
        public const int n = 200;
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            Test_Array(seed);
            Test_List(seed);
            Console.WriteLine("Array test");
            for (int i = 2; i < 128; i = i * 2)
            {
                Test_Array(seed, i * 200);
            }
            Console.WriteLine("List test");
            for (int i = 2; i < 128; i = i * 2)
            {
                Test_List(seed, i * 200);
            }
        }
        public static void Test_Array(int seed, int n)
        {
            string filename = @"mydataarray.dat";
            var stopwatch = new Stopwatch();
            MyFileArray myArray = new MyFileArray(filename, n, seed, max);
            using (myArray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                stopwatch.Start();
                CountingSort(myArray);
                stopwatch.Stop();
            }
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed, int n)
        {
            string filename = @"mydatalist.dat";
            MyFileList myList = new MyFileList(filename, n, seed, max);
            var stopwatch = new Stopwatch();
            using (myList.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                stopwatch.Start();
                CountingSort(ref myList);
                stopwatch.Stop();
            }
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed)
        {
            string filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed, max);
            using (myfilelist.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                CountingSort(ref myfilelist);
                myfilelist.Print(n);
            }
        }
        public static void Test_Array(int seed)
        {
            string filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed, max);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                CountingSort(myfilearray);
                myfilearray.Print(n);
            }
        }
        public static void CountingSort(MyFileArray myArray)
        {
            string countFile = @"mycountarray.dat";
            string resultsFile = @"myresultsarray.dat";
            MyFileArray count = new MyFileArray(countFile, max);
            using (count.fs = new FileStream(countFile, FileMode.Open, FileAccess.ReadWrite))
            {
                for (int i = 0; i < n; i++)
                    count.Swap(myArray[i], count[myArray[i]] + 1);
                for (int i = 1; i < count.Length; i++)
                    count.Swap(i, count[i] + count[i - 1]);
                MyFileArray results = new MyFileArray(resultsFile, n);
                using (results.fs = new FileStream(resultsFile, FileMode.Open,
                FileAccess.ReadWrite))
                {
                    for (int i = n - 1; i >= 0; i--)
                    {
                        results.Swap(count[myArray[i]] - 1, myArray[i]);
                        count.Swap(myArray[i], count[myArray[i]] - 1);
                    }
                    for (int i = 0; i < n; i++)
                    {
                        myArray.Swap(i, results[i]);
                    }
                }
                Console.WriteLine("Done");
            }
        }
        public static void CountingSort(ref MyFileList myList)
        {
            string resultsFile = @"myresultsarray.dat";
            string countFile = @"mycountarray.dat";
            MyFileList count = new MyFileList(countFile, max);
            MyFileList sorted = new MyFileList(resultsFile, n);

            using (count.fs = new FileStream(countFile, FileMode.Open, FileAccess.ReadWrite))
            {
                //count[myArray[i]]++;
                for (int i = 0; i < n; i++)
                {
                    count.Set(myList.Get(i), count.Get(myList.Get(i)) + 1);
                }
                // count[i]+=count[i-1];;
                for (int i = 1; i < count.Length; i++)
                {
                    count.Set(i, count.Get(i-1) + count.Get(i));
                }
                using (sorted.fs = new FileStream(resultsFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    //(int i = myArray.Length - 1; i >= 0; i--)
                    //sorted[count[myArray[i]] - 1] = myArray[i];
                    //--count[myArray[i]];
                    for (int i = n - 1; i >= 0; i--)
                    {
                        sorted.Set(count.Get(myList.Get(i)) - 1, myList.Get(i));
                        count.Set(myList.Get(i), count.Get(myList.Get(i)) - 1);
                    }
                    for (int i = 0; i < n; i++)
                    {
                        myList.Set(i, sorted.Get(i));
                    }
                }
                Console.WriteLine("Done");
            }
        }
    }
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract int Head();
        public abstract int Next();
        public abstract void Swap(int i, int j, int a, int b);
        public void Print(int n)
        {
            Console.Write(" {0} ", Head());
            for
           (int i = 1; i < n; i++)
                Console.Write(" {0} ", Next());
            Console.WriteLine();

        }
    }
    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract int this[int index] { get; }
        public abstract void Swap(int i, int a);
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0} ", this[i]);
            Console.WriteLine();
        }
    }
}
