using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node<T>
{
    public T Data;
    public Node<T> Next;

    public Node(T data)
    {
            Data = data;
            Next = null;
    }
}
public class LinkedQueue<T>
{
    private Node<T> front;
    private Node<T> rear;

        public LinkedQueue()
        {
            front = null;
            rear = null;
        }

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (rear == null)
            {
                front = newNode;
                rear = newNode;
            }
            else
            {
                rear.Next = newNode;
                rear = newNode;
            }
        }

        public T Dequeue()
        {
            if (front == null)
            {
                throw new System.Exception("Queue is empty");
            }

            T data = front.Data;
            front = front.Next;

            if (front == null)
            {
                rear = null;
            }

            return data;
        }

        public T Peek()
        {
            if (front == null)
            {
                throw new System.Exception("Queue is empty");
            }

            return front.Data;
        }

        public bool IsEmpty()
        {
            return front == null;
        }
    }
public class Queue : MonoBehaviour
{ 

}  

