using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CountingD
{
    class MyFileArray : DataArray
    {
    int[] data;
        public MyFileArray(string filename, int n, int seed, int max)
        {
            int[] data = new int[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.Next(max);
            }
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public MyFileArray(string filename, int n)
        {
            int[] data = new int[n];
            length = n;
            if (File.Exists(filename)) File.Delete(filename);
            for (int i = 0; i < length; i++)
            {
                data[i] = 0;
            }
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override int this[int index]
        {
            get
            {
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data, 0);
                return result;
            }
        }
        public override void Swap(int i, int a)
        {
            Byte[] data = new Byte[4];
            BitConverter.GetBytes(a).CopyTo(data, 0);
            fs.Seek(4 * i, SeekOrigin.Begin);
            fs.Write(data, 0, 4);
        }
    }
}
