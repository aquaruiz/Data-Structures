using System;
using System.Collections.Generic;
using System.Linq;

namespace CountOccurrences
{
    class CountOccurrences
    {
        static void Main(string[] args)
        {
            List<int> input = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            input.Sort();

            int count = 1;

            for (int i = 0; i < input.Count - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    count++; 
                }
                else if (input[i] != input[i + 1])
                {
                    Console.WriteLine(input[i] + " -> " + count + " times");
                    count = 1;
                }

                if (i == input.Count - 2)
                {
                    Console.WriteLine(input[i + 1] + " -> " + count + " times");
                }
            }

            if (input.Count == 1)
            {
                Console.WriteLine(input[0] + " -> " + count + " times");
            }
        }
    }
}
