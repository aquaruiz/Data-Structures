using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ArrayStack<T>
{
    private T[] elements;

    public int Count { get; private set; }

    private const int InitialCapacity = 16;

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
        this.Count = 0;
    }

    public void Push(T element)
    {
        if (this.Count == this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        this.Count--;
        var element = this.elements[this.Count];

        return element;
    }

    public T[] ToArray()
    {
        T[] arrReturn = new T[this.Count];

        for (int i = 0; i < this.Count; i++)
        {
            arrReturn[i] = this.elements[this.Count - 1 - i];
        }

        return arrReturn;
    }

    private void Grow()
    {
        T[] newArr = new T[2 * this.elements.Length];
        Array.Copy(this.elements, newArr, this.Count);
        this.elements = newArr;
    }
}