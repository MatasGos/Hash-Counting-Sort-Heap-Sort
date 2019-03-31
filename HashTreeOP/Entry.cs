using System;
using System.Collections.Generic;
using System.Text;

namespace HashTable
{
    class Entry
    {
        public string key { get; set; }

        public string value { get; set; }
        public Entry(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}