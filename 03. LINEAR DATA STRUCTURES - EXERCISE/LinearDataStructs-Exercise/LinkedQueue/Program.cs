using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var linkedQueue = new LinkedQueue<int>();

            for (int i = 1; i <= 10; i++)
            {
                linkedQueue.Enqueue(i);
            }

            int[] toArray = linkedQueue.ToArray();

            Console.WriteLine(string.Join(", ", toArray));

            Console.WriteLine(linkedQueue.Dequeue());
            linkedQueue.Enqueue(50);
            Console.WriteLine(linkedQueue.Dequeue());

            toArray = linkedQueue.ToArray();

            Console.WriteLine(string.Join(", ", toArray));
        }
    }
}
