using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        HeapSort(arr);
    }

    private static void HeapSort(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }
    }

    private static void HeapifyDown(T[] arr, int index, int length)
    {
        int parentIndex = index;

        while (parentIndex < length / 2)
        {
            int childIndex = parentIndex * 2 + 1;

            if (childIndex + 1 < length && IsGreater(arr, childIndex, childIndex + 1))
            {
                childIndex += 1;
            }

            int compare = arr[parentIndex].CompareTo(arr[childIndex]);

            if (compare < 0)
            {
                // child Element > parent element
                Swap(arr, childIndex, parentIndex);
            }

            parentIndex = childIndex;
        }
    }

    private static bool IsGreater(T[] heap, int left, int right)
    {
        return heap[left].CompareTo(heap[right]) < 0;
    }
    
    private static void Swap(T[] heap, int parent, int index)
    {
        T temp = heap[parent];
        heap[parent] = heap[index];
        heap[index] = temp;
    }


    private static void HeapifyUp(T[] heap, T item, int index)
    {
        int parent = (index - 1) / 2;

        if (parent < 0)
        {
            return;
        }

        int compare = heap[parent].CompareTo(heap[index]);

        if (compare < 0)
        {
            // index is the child's index
            // parent < this.heap[index]
            Swap(heap, parent, index);
            HeapifyUp(heap, heap[parent], parent);
        }
    }
}