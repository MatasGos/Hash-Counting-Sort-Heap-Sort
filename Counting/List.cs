using System;
using System.Collections.Generic;
using System.Text;

namespace Counting
{
    class MyDataList : DataList
    {
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public int data { get; set; }
            public MyLinkedListNode(int data)
            {
                this.data = data;
            }
        }
        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;
        public MyDataList(int n, int seed, int max)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.Next(max));
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.Next(max));
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }
        public MyDataList(int n)
        {
            length = n;
            headNode = new MyLinkedListNode(0);
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(0);
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }
        public override int Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }
        public override int Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            return currentNode.data;
        }
        public override void Swap(int a, int b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }
        public void Set(int index, int data)
        {
            if(index > length)
            {
                Console.WriteLine("klaida" + index);
                //return;
            }
            currentNode = headNode;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.nextNode;
            }
            currentNode.data = data;
        }
        public void Increase(int index)
        {
            if (index > length)
            {
                Console.WriteLine("klaida increase" + index + ">" + length);
                //return;
            }
            currentNode = headNode;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.nextNode;
            }
            currentNode.data++;
        }
    }
}
