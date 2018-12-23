using System;
using System.Linq;
using System.Collections.Generic;

namespace Sum_Average
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            Console.WriteLine("Sum={0}; Average={1:F2}", input.Sum(), input.Average());
        }
    }
}
