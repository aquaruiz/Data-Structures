﻿class Launcher
{
    static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();

        list.AddFirst(1);
        list.AddLast(2);

        foreach (var item in list)
        {
            System.Console.WriteLine(item);
        }
    }
}
