using System;
using UnityEngine;

namespace DataStrucuture
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> head;

        public LinkedList()
        {
            head = null;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                LinkedListNode<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }
    }

    public class Queue<T>
    {
        private LinkedList<T> list;

        public Queue()
        {
            list = new LinkedList<T>();
        }

        // ť�� ��Ҹ� �߰��մϴ�.
        public void Enqueue(T data)
        {
            list.Add(data);
        }

        // ť���� ��Ҹ� �����ϰ� ��ȯ�մϴ�.
        public T Dequeue()
        {
            if (list.head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T data = list.head.Data;
            list.head = list.head.Next;
            return data;
        }

        public int Size()
        {
            int count = 0;
            LinkedListNode<T> current = list.head;
            
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            
            return count;
        }
    }

    public class Stack<T>
    {
        private Queue<T> q1;
        private Queue<T> q2;
        private Queue<T> temp;

        public Stack()
        {
            q1 = new Queue<T>();
            q2 = new Queue<T>();
            temp = new Queue<T>();
        }

        // ���ÿ� ��Ҹ� �߰��մϴ�.
        public void Push(T data)
        {
            // ���ο� ��Ҹ� q2�� �߰�
            q2.Enqueue(data);

            // q1�� ��� ��Ҹ� q2�� �̵�
            while (q1.Size() > 0)
            {
                q2.Enqueue(q1.Dequeue());
            }

            // q1�� q2�� ��ȯ�Ͽ� q1�� �׻� ������ �ֽ� ���¸� �����ϵ��� �մϴ�.
            temp = q1;
            q1 = q2;
            q2 = temp;
        }

        // ���ÿ��� ��Ҹ� �����ϰ� ��ȯ�մϴ�.
        public T Pop()
        {
            if (q1.Size() == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }

            return q1.Dequeue();
        }
    }

}
