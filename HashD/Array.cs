using System;
using System.IO;
namespace SortD
{
    class MyFileArray : DataArray
    {
        public MyFileArray(string filename, int n, int seed)
        {
            double[] data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.NextDouble();
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
        public FileStream fs { get; set; }
        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                double result = BitConverter.ToDouble(data, 0);
                return result;
            }
        }
        public override void Swap(int i, int j, double a, double b)
        {
            Byte[] data1 = new Byte[8];
            Byte[] data2 = new Byte[8];
            BitConverter.GetBytes(a).CopyTo(data1, 0);
            BitConverter.GetBytes(b).CopyTo(data2, 0);
            fs.Seek(8 * i, SeekOrigin.Begin);
            fs.Write(data2, 0, 8);
            fs.Seek(8 * j, SeekOrigin.Begin);
            fs.Write(data1, 0, 8);            
        }
        public void Swap(int i, double a, double b)
        {
            Byte[] data1 = new Byte[8];
            Byte[] data2 = new Byte[8];
            BitConverter.GetBytes(a).CopyTo(data1, 0);
            BitConverter.GetBytes(b).CopyTo(data2, 0);
            fs.Seek(0, SeekOrigin.Begin);
            fs.Write(data2, 0, 8);
            fs.Seek(8 * i, SeekOrigin.Begin);
            fs.Write(data1, 0, 8);
        }
        public void buildHeap(int n, int i)
        {
            int largest = i; 
            int l = 2 * i + 1; 
            int r = 2 * i + 2; 

            if (l < n && this[l] > this[largest])
                largest = l;

            if (r < n && this[r] > this[largest])
                largest = r;

            if (largest != i)
            {
                Swap(i, largest, this[i], this[largest]);

                buildHeap(n, largest);
            }
        }
    }
}

