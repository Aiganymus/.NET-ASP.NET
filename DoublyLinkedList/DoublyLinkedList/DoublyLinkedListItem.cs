using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    class Node
    {
        public string value;
        public int key;

        public Node next;
        public Node prev;

        public Node(string value, int key)
        {
            this.value = value;
            this.key = key;
        }
    }

    class DoublyLinkedList
    {
        Node first;
        Node last;
        int size = 0;

        public void InsertFront(string value, int key)
        {
            Node node = new Node(value, key);
            if (first == null)
            {
                first = node;
                first.next = last;
            }
            else
            {
                first.prev = node;
                node.next = first;
                first = node;
            }
            size++;
        }

        public void InsertEnd(string value, int address)
        {
            Node node = new Node(value, address);
            if (last == null)
            {
                last = node;
                last.prev = first;
                first.next = last;
            }
            else
            {
                last.next = node;
                node.prev = last;
                last = node;
            }
            size++;
        }

        public void Insert(string value, int key, int index)
        {
            Node node = new Node(value, key);

            Node currentItem = first;

            while(currentItem.key != index)
            {
                currentItem = currentItem.next;
            }

            node.next = currentItem.next;
            currentItem.next.prev = node;

            node.prev = currentItem;
            currentItem.next = node;

            size++;
        }

        public string DeleteFront()
        {
            string value = first.value;
            first = first.next;
            first.prev = null;

            size--;
            return value;
        }

        public string DeleteEnd()
        {
            string value = last.value;
            last = last.prev;
            last.next = null;

            size--;
            return value;
        }

        public string Delete(int index)
        {
            Node currentItem = first;

            while(currentItem.key != index)
            {
                currentItem = currentItem.next;
                if(currentItem == null)
                {
                    return "false";
                }
            }

            string value = currentItem.value;
            currentItem.prev.next = currentItem.next;
            currentItem.next.prev = currentItem.prev;

            size--;
            return value;
        }

        public static void Main(string[] args)
        {
            DoublyLinkedList list = new DoublyLinkedList();

            
            list.InsertFront("aiganym", 1);
            list.InsertEnd("hello", 23);
            list.InsertEnd("jdls", 3);
            list.Insert("slall", 5, 23);
            Console.WriteLine(list.size);
        }
    }
}
