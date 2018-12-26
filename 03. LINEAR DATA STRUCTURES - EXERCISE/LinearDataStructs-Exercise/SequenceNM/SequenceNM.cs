using System;
using System.Collections.Generic;
using System.Linq;

namespace SequenceNM
{
    class SequenceNM
    {
        public class Item
        {
            public int Value { get; private set; }
            public Item PrevItem { get; set; }

            public Item(int value, Item prevItem)
            {
                this.Value = value;
                this.PrevItem = prevItem;
            }
        }

        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().ToArray();
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            if (n == m)
            {
                Console.WriteLine(n);
                return;
            }

            Queue<Item> numbersQueue = new Queue<Item>(new[] { new Item(n, null)});

            while (numbersQueue.Count != 0)
            {
                var e = numbersQueue.Dequeue();
                if (e.Value < m)
                {
                    numbersQueue.Enqueue(new Item(e.Value + 1, e));
                    numbersQueue.Enqueue(new Item(e.Value + 2, e));
                    numbersQueue.Enqueue(new Item(e.Value * 2,e));

                }
                else if (e.Value == m)
                {
                    var result = new List<int>();

                    while (e.PrevItem != null)
                    {
                        result.Insert(0, e.Value);
                        e = e.PrevItem;
                    }

                    result.Insert(0, n);

                    Console.WriteLine(String.Join(" -> ", result));

                    return;
                }
            }
        }    
    }
}