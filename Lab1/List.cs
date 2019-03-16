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
        public override void Swap(int n)
        {
            currentNode = headNode;
            while (n>0)
            {
                currentNode = currentNode.nextNode;
                n--;
            }
            double swap = currentNode.data;
            currentNode.data = headNode.data;
            headNode.data = swap; 
        }
        public void test()
        {
            currentNode=headNode;
            currentNode.nextNode.data = 0;
        }
        public void test2(int a)
        {
            int leftIndex = a;
            currentNode = headNode;
            while (leftIndex > 0)
            {
                currentNode = currentNode.nextNode;
                leftIndex--;
            }
            Console.WriteLine("test2"+headNode.data);
        }
        public void buildHeap(int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            MyLinkedListNode current = headNode;

            int largestIndex = i;
            while (largest > 0)
            {
                current = current.nextNode;
                largest--;
            }
            MyLinkedListNode left = current;
            MyLinkedListNode right = current;
            largest = i;
            double rootData = current.data;
            double largestData = current.data;
            // suranda kaires reiksme
            if (l < n)
            {
                int leftIndex = l-i;
                while (leftIndex > 0)
                {
                    left = left.nextNode;
                    leftIndex--;
                }         
            }
            // suranda desines reiksme
            if (r < n)
            {
                int rightIndex = r-i;
                while (rightIndex > 0)
                {
                    right = right.nextNode;
                    rightIndex--;
                }
            }
            Console.WriteLine(rootData + " " + left.data + " " + right.data);

            if(r < n)
                {
                if (left.data > right.data)
                {
                    if (current.data < left.data)
                    {
                        largest = l;
                        Console.WriteLine(left.data + " su "+current.data);
                        largestData = left.data;
                        left.data = current.data;
                        current.data = largestData;
                    }

                }
                else
                {
                    if (current.data < right.data)
                    {
                        largest = r;
                        Console.WriteLine(right.data + " su " + current.data);
                        largestData = right.data;
                        right.data = current.data;
                        current.data = largestData;
                    }
                }
            }
            else if (l < n)
            {

                if (current.data < left.data)
                {
                    largest = l;
                    Console.WriteLine(left.data + " su " + current.data);
                    largestData = left.data;
                    left.data = current.data;
                    current.data = largestData;
                }
            }
            // If largest is not root 
            if (largest != i)
            {
                buildHeap(n, largest);
            }
        }

    }
}
