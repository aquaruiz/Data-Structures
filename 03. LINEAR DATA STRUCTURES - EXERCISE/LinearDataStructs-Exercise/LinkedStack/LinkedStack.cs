using System;

public class LinkedStack<T>
{
    private class Node<T>
    {
        public Node(T value, Node<T> nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public Node<T> NextNode { get; set; }
        public T Value { get; private set; }
    }

    private Node<T> firstNode;
    public int Count { get; private set; }

    public void Push(T value)
    {
        var currentNode = new Node<T>(value);

        if (this.Count == 0)
        {
            this.firstNode = currentNode;
        } 
        else
        {
            currentNode.NextNode = this.firstNode;
            this.firstNode = currentNode;
        }

        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var firstNode = this.firstNode;
        this.firstNode = this.firstNode.NextNode;

        this.Count--;

        return firstNode.Value;
    }

    public T[] ToArray()
    {
        var result = new T[this.Count];
        var nextNode = this.firstNode;

        for (int i = 0; i < this.Count; i++)
        {
            result[i] = nextNode.Value;
            nextNode = nextNode.NextNode;
        }

        return result;
    }
}