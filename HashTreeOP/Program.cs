using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            //HashTestFromFile();
            //HashTestDFromFile();
            Console.WriteLine("Memory");
            for (int i = 2; i <= 64; i *= 2)
            {
                HashTest(i * 5000, seed);

            }
            Console.WriteLine("File");
            for (int i = 2; i <= 64; i *= 2)
            {
                HashTestD(i * 5000, seed);

            }
        }
        public static void HashTest(int n, int seed)
        {
            Random rand = new Random(seed);
            string[] keys = new string[n];
            string[] values = new string[n];
            HashTable map = new HashTable(n * 2);
            for (int i = 0; i < n; i++)
            {
                keys[i] = "KEY" + rand.Next(999999999);
                values[i] = "VALUE" + rand.Next(999999999);
                map.put(keys[i], values[i]);
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                map.get(keys[i]);
            stopwatch.Stop();
            Console.WriteLine("n = {0,-7}  {1,5} ms", n, stopwatch.ElapsedMilliseconds);
        }
        public static void HashTestD(int n, int seed)
        {
            string filename = "hash.txt";
            var stopwatch = new Stopwatch();
            Random rand = new Random(seed);
            string[] keys = new string[n];
            string[] values = new string[n];
            HashTableD map = new HashTableD(filename, n * 2);
            using (map.fs = new FileStream(filename, FileMode.Open,
            FileAccess.ReadWrite))
            {
                for (int i = 0; i < n; i++)
                {
                    keys[i] = "K" + rand.Next(999999999);
                    values[i] = "V" + rand.Next(999999999);
                    map.Insert(keys[i], values[i]);
                }

                stopwatch.Start();
                for (int i = 0; i < n; i++)
                    map.Get(keys[i]);
                stopwatch.Stop();
                Console.WriteLine("n = {0,-7}  {1,5} ms", n, stopwatch.ElapsedMilliseconds);
            }
        }
        public static void HashTestFromFile()
        {
            HashTable map = new HashTable(20000000);
            string[] key = new string[20000000];
            using (StreamReader reader = new StreamReader("tekstas.txt"))
            {
                string[] y;
                for (int i = 0; i < 1000000; i++)
                {
                    y = reader.ReadLine().Split();
                    key[i] = y[0];
                    map.put(y[0], y[1]);
                }
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                map.get(key[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Memory (from file)");
            Console.WriteLine("n = 1000000");
            Console.WriteLine("Time: {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Average search time: {0} ms", (double)stopwatch.ElapsedMilliseconds);
        }
        public static void HashTestDFromFile()
        {
            string filename = "test.txt";
            HashTableD map = new HashTableD(filename, 2000000);
            string[] key = new string[2000000];
            var stopwatch = new Stopwatch();
            using (map.fs = new FileStream(filename, FileMode.Open,
            FileAccess.ReadWrite))
            {
                using (StreamReader reader = new StreamReader("tekstas.txt"))
                {
                    string[] y;
                    for (int i = 0; i < 1000000; i++)
                    {
                        y = reader.ReadLine().Split();
                        key[i] = y[0];
                        map.Insert(y[0], y[1]);
                    }
                }

                stopwatch.Start();
                for (int i = 0; i < 1000000; i++)
                {
                    map.Get(key[i]);
                }
                stopwatch.Stop();
            }
            Console.WriteLine("Memory (from file)");
            Console.WriteLine("n = 1000000");
            Console.WriteLine("Time: {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Average search time: {0} ms", (double)stopwatch.ElapsedMilliseconds / 1000000);
        }
    }
}

