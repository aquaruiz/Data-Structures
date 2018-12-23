using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestSubsequence
{
    class LongestSubsequence
    {
        static void Main(string[] args)
        {
            List<int> input = Console.ReadLine().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            int maxLenght = 0;
            int currentLenght = 1;

            List<int> output = new List<int>();

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] == input[i - 1])
                {
                    currentLenght++;
                }
                else
                {
                    if (currentLenght > maxLenght)
                    {
                        maxLenght = currentLenght;
                        currentLenght = 1;
                        output.Clear();
                        output.AddRange(Enumerable.Repeat(input[i - 1], maxLenght));
                    }
                }
            }

            if (currentLenght > maxLenght)
            {
                maxLenght = currentLenght;
                currentLenght = 1;
                output.Clear();
                output.AddRange(Enumerable.Repeat(input[input.Count - 1], maxLenght));
            }

            Console.WriteLine(String.Join(" ", output));
            // Happy, merry AI-based Christmas!
        }
    }
}
