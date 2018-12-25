
using System;
using System.Collections.Generic;

namespace CalculateSequence_Queue
{
    class CalculateSequence
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Queue<int> myQueue = new Queue<int>();

            int indexer = 50;
            myQueue.Enqueue(n);

            int next_n = myQueue.Dequeue();
            int n1 = next_n + 1;
            int n2 = 2 * next_n + 1;
            int n3 = next_n + 2;

            myQueue.Enqueue(n1);
            myQueue.Enqueue(n2);
            myQueue.Enqueue(n3);

            Console.Write(next_n);
            indexer--;

            while (indexer > 0)
            {
                next_n = myQueue.Dequeue();
                n1 = next_n + 1;
                n2 = 2 * next_n + 1;
                n3 = next_n + 2;

                myQueue.Enqueue(n1);
                myQueue.Enqueue(n2);
                myQueue.Enqueue(n3);

                Console.Write(", " + next_n);
                indexer--;
            }
        }
    }
}