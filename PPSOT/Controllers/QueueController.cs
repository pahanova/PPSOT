using System;

namespace PPSOT.Models
{
    public class Node
    {
        public string Data;
        public int Priority;
        public Node Next;
        public Node(string data, int priority, Node next = null)
        {
            Data = data;
            Priority = priority;
            Next = next;
        }
        public void setData(string data)
        {
            this.Data = data;
        }
        public string getData()
        {
            return this.Data;
        }
        public void setPriority(int priority)
        {
            this.Priority = priority;
        }
        public int getPriority()
        {
            return this.Priority;
        }

    }
    public class Queue //: IEnumerable<T>
    {
        private Node head; // головной/первый элемент
        private int size;
        public Queue()
        {
            Node head = null; // головной/первый элемент
            int size = 0;
        }
        public bool IsEmpty { get { return this.size == 0; } }
        // удаление из очереди
        public string delete()
        {
            if (size == 0)
                throw new InvalidOperationException();
            string output = head.Data;
            head = head.Next;
            size--;
            return output;
        }
        public string output()
        {
            Node currentNode = head;
            string output = "";
            while (currentNode != null)
            {
                output = output + currentNode.Priority.ToString() + currentNode.Data + '\n';
                currentNode = currentNode.Next;
            }
            return output;
        }
        public void add(string data, int priority)
        {
            Node node = new Node(data, priority);
            Node currentNode = head;
            if (head == null)
            {
                head = node;
                head.Next = null;
            }
            else if (head.Next == null)
            {
                if (node.Priority <= head.Priority)
                    head.Next = node;
                else
                {
                    Node tmp = head;
                    head = node;
                    head.Next = tmp;
                }
            }
            else
            {
                int tmpOne = 11;//значение которое больше максимального приоритета на 1
                Node tmpTwo = head;
                // находит элемент, перед которым нужно вставить элемент
                if (head.Priority < node.Priority)
                {
                    Node tmp = head;
                    head = node;
                    head.Next = tmp;
                }
                else
                {
                    while (currentNode.Next != null)
                    {
                        if ((node.Priority <= tmpOne) && (node.Priority > tmpTwo.Priority))
                        {
                            Node tmpnode = currentNode.Next;
                            currentNode.Next = node;
                            node.Next = tmpnode;
                            break;

                        }
                        currentNode = currentNode.Next;
                    }
                }
                size++;
            }
        }
    }
}