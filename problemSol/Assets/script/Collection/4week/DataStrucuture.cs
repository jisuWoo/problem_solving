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

        // 큐에 요소를 추가합니다.
        public void Enqueue(T data)
        {
            list.Add(data);
        }

        // 큐에서 요소를 제거하고 반환합니다.
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

        // 스택에 요소를 추가합니다.
        public void Push(T data)
        {
            // 새로운 요소를 q2에 추가
            q2.Enqueue(data);

            // q1의 모든 요소를 q2로 이동
            while (q1.Size() > 0)
            {
                q2.Enqueue(q1.Dequeue());
            }

            // q1과 q2를 교환하여 q1이 항상 스택의 최신 상태를 유지하도록 합니다.
            temp = q1;
            q1 = q2;
            q2 = temp;
        }

        // 스택에서 요소를 제거하고 반환합니다.
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
