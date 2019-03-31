using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class HashTable
    {
        Entry[] table;
        public static int DEFAULT_INITIAL_CAPACITY = 16;
        public static float DEFAULT_LOAD_FACTOR = 0.75f;
        int size = 0;
        int rehashesCounter = 0;
        int lastUpdatedChain = 0;


        public HashTable(long initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                return;
            }
            this.table = new Entry[initialCapacity];
        }

        public int Size()
        {
            return size;
        }

        public bool contains(string key)
        {
            return get(key) != null;
        }
        public void put(string key, string value)
        {
            if (key == null || value == null)
            {
                throw new ArgumentException("Key or value is null in put(Key key, Value value)");
            }
            int index = findPosition(key);
            if (index == -1)
            {
                rehash();
                put(key, value);
                return;
            }
            if (table[index] == null)
            {
                table[index] = new Entry(key, value);
                size++;
                if (size > table.Length * DEFAULT_LOAD_FACTOR)
                {
                    rehash();
                }
            }
            else
            {
                table[index].value = value;
            }
            lastUpdatedChain = index;
            return;
        }

        public string get(string key)
        {
            if (key == null)
            {
                throw new ArgumentException("Key is null in get(Key key)");
            }
            int index = findPosition(key);
            if (index != -1 && table[index] != null)
                return table[index].value;
            else
                return null;

        }
        private void rehash()
        {
            if (table.Length * 2 < 0)
                return;
            HashTable hashTable
                    = new HashTable(table.Length * 2);
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    hashTable.put(table[i].key, table[i].value);
                }
            }
            table = hashTable.table;
            rehashesCounter++;
        }
        private int hash(string key)
        {
            int h = key.GetHashCode();  
            return Math.Abs(h) % table.Length;
        }
        public int getMaxChainSize()
        {
            if (size == 0)
                return 0;
            else
                return 1;
        }
        public int getRehashesCounter()
        {
            return rehashesCounter;
        }
        public int getTableCapacity()
        {
            return table.Length;
        }
        private int findPosition(string key)
        {
            int index = hash(key);
            int index0 = index;
            int i = 0;
            for (int j = 0; j < table.Length; j++)
            {
                if (table[index] == null || table[index].key==key)
                {
                    return index;
                }
                i++;
                index = (index0 + i * i) % table.Length;
            }
            return -1;
        }
    }
}