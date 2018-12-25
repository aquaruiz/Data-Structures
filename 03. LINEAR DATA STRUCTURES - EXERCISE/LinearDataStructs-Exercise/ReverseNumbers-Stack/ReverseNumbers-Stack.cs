using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseNumbers_Stack
{
    class ReverseNumbers
    {
        static void Main(string[] args)
        {
            try
            {
                int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();

                Stack<int> myStack = new Stack<int>();
                foreach (var item in input)
                {
                    myStack.Push(item);
                }

                while (myStack.Count > 0)
                {
                    Console.Write(myStack.Pop() + " ");
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
