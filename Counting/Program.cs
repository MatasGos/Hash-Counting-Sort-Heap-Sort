using System;

namespace Counting
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            int[] count = new int[256];
            double[] arr = { 10.1, 5, 3 };
            count[1] = 100;
            //Console.WriteLine(count[]);

            //Test_Array(seed);
        }
        public static void Test_Array(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray();

            myArray.Print(myArray.Length);


            Console.WriteLine("Done");
            myArray.Print(myArray.Length);

            int[] count = new int[256];
            double[] arr = { 10.1, 5, 3 };
            count['e']=100;
            Console.WriteLine(count['e']);

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
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract void Swap(double a, double b);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5}", Next());
            Console.WriteLine();
        }
    }
}
