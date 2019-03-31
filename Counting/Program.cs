using System;
using System.Diagnostics;

namespace Counting
{
    class Program
    {
        const int max = 255;
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            //Test_List(seed);
            //Test_Array(seed);
            Console.WriteLine("Array test");
            for (int i = 2; i < 64; i = i * 2)
            {
                Test_Array(seed, i * 1000);
            }
            Console.WriteLine("List test");
            for (int i = 2; i < 64; i = i * 2)
            {
                Test_List(seed, i * 1000);
            }
        }
        public static void Test_Array(int seed, int n)
        {
            var stopwatch = new Stopwatch();
            MyDataArray myArray = new MyDataArray(n, seed, max);
            stopwatch.Start();
            CountingSort(myArray);
            stopwatch.Stop();
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_List(int seed, int n)
        {
            MyDataList myList = new MyDataList(n, seed, max);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            CountingSort(ref myList);
            stopwatch.Stop();
            Console.WriteLine("n = {0,-7} {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void Test_Array(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray(n, seed, max);

            myArray.Print(myArray.Length);
            CountingSort(myArray);
            myArray.Print(myArray.Length);
        }
        public static void CountingSort(MyDataArray myArray)
        {
            int[] count = new int[255];
            int[] sorted = new int[myArray.Length];
            for (int i = 0; i < 255; i++)
                count[i] = 0;
            for (int i = 0; i < myArray.Length; i++)
                count[myArray[i]]++;
            for (int i = 1; i < count.Length; i++)
                count[i]+=count[i-1];
            for (int i = myArray.Length - 1; i >= 0; i--)
            {
                sorted[count[myArray[i]] - 1] = myArray[i];
                --count[myArray[i]];
            }
            for (int i = 0; i < myArray.Length; i++)
                myArray.Swap(i, sorted[i]);

            Console.WriteLine("Done");
        }
        public static void CountingSort(ref MyDataList myList)
        {

            MyDataList count = new MyDataList(max);
            //count[i] = 0;count[i] = 0;
            MyDataList sorted = new MyDataList(myList.Length);
            //count[myArray[i]]++;
            int currentdata = myList.Head();
            count.Increase(currentdata);
            for (int i = 1; i < myList.Length; i++)
            {
                currentdata = myList.Next();
                count.Increase(currentdata);
            }           
            // count[i]+=count[i-1];
            int previousdata = count.Head();
            for (int i = 1; i < count.Length; i++)
            {
                currentdata = count.Next();
                previousdata += currentdata;
                count.Set(i, previousdata);
            }
            //(int i = myArray.Length - 1; i >= 0; i--)
            //sorted[count[myArray[i]] - 1] = myArray[i];
            //--count[myArray[i]];
            for (int i = myList.Length-1; i >= 0; i--)
            {
                currentdata = myList.Head();
                for (int j = 0; j < i; j++)
                {
                    //myArray[i]
                    currentdata = myList.Next();
                }                
                previousdata = count.Head();
                for (int j = 0; j < currentdata; j++)
                {
                    //count[myArray[i]]
                    previousdata = count.Next();
                }
                sorted.Set(previousdata - 1, currentdata);
                count.Set(currentdata, previousdata - 1);
            }
            myList = sorted;
            Console.WriteLine("Done");
        }
        public static void Test_List(int seed)
        {
            int n = 12;
            MyDataList myList = new MyDataList(n, seed, max);
            myList.Print(myList.Length);
            CountingSort(ref myList);
            myList.Print(myList.Length);
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
            {
                Console.Write(" {0} ", this[i]);
            }
        }
    }
    abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract int Head();
        public abstract int Next();
        public abstract void Swap(int a, int b);
        public void Print(int n)
        {
            Console.Write(" {0} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0}", Next());
            Console.WriteLine();
        }
    }
}
