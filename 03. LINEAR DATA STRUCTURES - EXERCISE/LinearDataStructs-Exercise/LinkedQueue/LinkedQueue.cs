using System;

public class LinkedQueue<T>
{ 
    private class QueueNode<T>
    {
        public T Value { get; private set; }
        public QueueNode<T> NextNode { get; set; }
        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value)
        {
            this.Value = value;
        }
    }

    private QueueNode<T> head;
    private QueueNode<T> tail;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.tail = this.head = new QueueNode<T>(element);
        }
        else
        {
            var newTail = new QueueNode<T>(element);
            newTail.PrevNode = this.tail;
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var firstElement = this.head.Value;

        this.head = this.head.NextNode;

        this.Count--;
        return firstElement;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        int index = 0;
        var currentNode = this.head;

        while (currentNode != null)
        {
            arr[index++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }

        return arr;
    }
}