using System;
using System.Collections.Generic;
using System.Linq;

namespace SortWords
{
    class SortWords
    {
        static void Main(string[] args)
        {
            List<string> input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            input.Sort();

            Console.WriteLine(String.Join(" ", input));
        }
    }
}
