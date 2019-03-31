using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class HashTableD
    {
        public FileStream fs { get; set; }
        int count;
        int size;
        int tableSize;
        public HashTableD(string filename, int size)
        {
            if (size < 13)
            {
                throw new ArgumentException("Size can't be lower than 13", "original");
            }
            this.size = size;
            int tableSize = size;
            while (!IsPrime(tableSize))
            {
                tableSize++;
            }
            this.tableSize = tableSize;
            if (File.Exists(filename)) File.Delete(filename);
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
            {
                Byte[] x = new Byte[10];
                x = Encoding.ASCII.GetBytes("          ");
                for (int i = 0; i < tableSize; i++)
                {
                    writer.Write(x);
                    writer.Write(x);
                }
            }
            count = 0;
        }
        bool IsFull()
        {
            return (count == size);
        }
        int Hash1(string key)
        {
            int hash = key.GetHashCode();
            return Math.Abs(hash % tableSize);
        }
        int Hash2(string key)
        {
            int hash = key.GetHashCode();
            return Math.Abs(13 - (hash % 13));
        }
        public void Insert(string key, string value)
        {
            if (IsFull())
            {
                Console.WriteLine("Table full");
                return;
            }
            int hash1 = Hash1(key) * 20;
            int hash2 = Hash2(key) * 20;
            Byte[] x = new Byte[10];
            fs.Seek(hash1, SeekOrigin.Begin);
            fs.Read(x, 0, 10);
            string temp = Encoding.ASCII.GetString(x, 0, 10);
            while (temp != "          " && temp != key)
            {                
                hash1 += hash2;
                hash1 %= tableSize *20;
                fs.Seek(hash1, SeekOrigin.Begin);
                fs.Read(x, 0, 10);
                temp = Encoding.ASCII.GetString(x, 0, 10);
            }            
            fs.Seek(-10, SeekOrigin.Current);
            x = Encoding.ASCII.GetBytes(key);
            Array.Resize(ref x, 10);
            fs.Write(x, 0, 10);
            x = Encoding.ASCII.GetBytes(value);
            Array.Resize(ref x, 10);
            fs.Write(x, 0, 10);
            count++;
        }        
        public string Get(string key)
        {
            int index = -1;
            int counter = count;
            int hash1 = Hash1(key) * 20;
            int hash2 = Hash2(key) * 20;
            Byte[] x = new Byte[10];
            fs.Seek(hash1, SeekOrigin.Begin);
            fs.Read(x, 0, 10);
            string temp = Encoding.ASCII.GetString(x, 0 , 10).TrimEnd('\0');
            while (temp != "" && counter != 0)
            {
                if (temp.Equals(key))
                {                    
                    index = hash1;
                    break;
                }                    
                hash1 += hash2;
                hash1 %= tableSize * 20;
                counter--;
                fs.Seek(hash1, SeekOrigin.Begin);
                fs.Read(x, 0, 10);
                temp = Encoding.ASCII.GetString(x, 0, 10).TrimEnd('\0');
            }
            Byte[] y = new Byte[10];
            if (index != -1)
            {
                fs.Read(y, 0, 10);
                return Encoding.ASCII.GetString(y, 0, 10);
            }
            else
                return null;                
        }
        public void Display()
        {
            Byte[] x = new Byte[20];
            fs.Seek(0, SeekOrigin.Begin);
            string key;
            for (int i = 0; i < tableSize; i++)
            {
                fs.Read(x, 0, 20);
                key = Encoding.ASCII.GetString(x, 0, 10);
                if (key != "          ")
                {
                    Console.WriteLine("{0}->{1}", key.TrimEnd('\0'), Encoding.ASCII.GetString(x, 10, 10).TrimEnd('\0'));
                }
            }
        }
        public int GetCount()
        {
            return count;
        }
        public bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}
