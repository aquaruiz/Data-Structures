using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoveOddOccurrence
{
    class RemoveOddOccurrence
    {
        static void Main(string[] args)
        {
            List<int> input = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            List<int> output = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < input.Count; j++)
                {
                    if (input[i] == input[j])
                    {
                        count++;
                    }
                }

                if (count % 2 == 0)
                {
                    output.Add(input[i]);
                }
            }

            Console.WriteLine(String.Join(" ", output));
        }
    }
}
