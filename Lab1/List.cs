using System;
using System.Collections.Generic;
using System.Text;

namespace Sort
{
    class MyDataList : DataList
    {
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public double data { get; set; }
            public MyLinkedListNode(double data)
            {
                this.data = data;
            }
        }
        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;
        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.NextDouble());
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(rand.NextDouble());
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }
        public override double Head()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }
        public override double Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            return currentNode.data;
        }
        public override void Swap(double a, double b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }
        public void buildHeap(int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            MyLinkedListNode current = first;
            MyLinkedListNode left = first;
            MyLinkedListNode right = first;
            int ii = i;

            if (l < n)
            {
                while (l > 0)
                {
                    current = current.next();
                    l--;
                }
            }
            if (r < n)
            {
                while (r > 0)
                {
                    current = current.next();
                    r--;
                }
            }
            while (current != null && ii > 0)
            {
                current = current.next();
                ii--;
            }

            if (left > current)
                largest = l;

            // If right child is larger than largest so far 
            if (right > current)
                largest = r;

            // If largest is not root 
            if (largest != i)
            {
                current = first;
                while (i > 0)
                {
                    i--;
                    current.nextNode();
                }
                double swapfrom = current.data;
                current = first;
                while (largest>0)
                {
                    largest--;
                    current.nextNode();
                }
                double swapto = current.data;
                current.data = swapto;
                current = first;
                while (i > 0)
                {
                    i--;
                    current.nextNode();
                }
                current.data = swapfrom;
                data[i] = data[largest];
                data[largest] = swap;

                // Recursively heapify the affected sub-tree 
                buildHeap(n, largest);
            }
        }

    }
}
