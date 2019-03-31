using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CountingD
{
    class MyFileList : DataList
    {
        int prevNode;
        int currentNode;
        int nextNode;
        public MyFileList(string filename, int n, int seed, int max)
        {
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(rand.Next(max));
                        writer.Write((j + 1) * 8 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public MyFileList(string filename, int n)
        {
            length = n;
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        writer.Write(0);
                        writer.Write((j + 1) * 8 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override int Head()
        {
            Byte[] data = new Byte[8];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            currentNode = BitConverter.ToInt32(data, 0);
            prevNode = -1;
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            int result = BitConverter.ToInt32(data, 0);
            nextNode = BitConverter.ToInt32(data, 4);
            return result;
        }
        public override int Next()
        {
            Byte[] data = new Byte[8];
            fs.Seek(nextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            prevNode = currentNode;
            currentNode = nextNode;
            int result = BitConverter.ToInt32(data, 0);
            nextNode = BitConverter.ToInt32(data, 4);
            return result;
        }
        public void Replace(int a)
        {
            Byte[] data = new Byte[8];
            BitConverter.GetBytes(a).CopyTo(data, 0);
            fs.Seek(currentNode, SeekOrigin.Begin);
            fs.Write(data, 0, 4);
        }


        public override void Swap(int i, int j, int a, int b)

        {
            this.Head();
            while (i > 0)
            {
                this.Next();
                i--;
            }
            this.Replace(b); ;
            this.Head();
            while (j > 0)
            {
                this.Next();
                j--;
            }
            this.Replace(a);

        }
        public int Get(int i)
        {
            int ret = this.Head();
            while (i > 0)
            {
                ret = this.Next();
                i--;
            }
            return ret;
        }
        public void Swap(int i, int a)

        {
            this.Head();
            while (i > 0)
            {
                this.Next();
                i--;
            }
            this.Replace(a);
        }
        public void Set(int index, int data)
        {
            this.Head();
            for (int i = 0; i < index; i++)
            {
                this.Next();
            }
            this.Replace(data);
        }
    }
}
