using System;
using System.Collections.Generic;

namespace MassEffectGalaxyMap
{
    class Program
    {
        public static void Main(string[] args)
        {
            int systemsCont = int.Parse(Console.ReadLine());
            int reportsCount = int.Parse(Console.ReadLine());
            int side = int.Parse(Console.ReadLine());
            Rectangle space = new Rectangle(0, side, 0, side);
            List < Point2D> systems = new List<Point2D>();    

            for (int i = 0; i < systemsCont; i++)
            {
                // get solar systems
                string line = Console.ReadLine();
                string[] tokens = line.Split();
                double x = double.Parse(tokens[1]);
                double y = double.Parse(tokens[2]);

                Point2D newPoint = new Point2D(x, y);

                if (newPoint.IsInRectangle(space))
                {
                    systems.Add(newPoint);
                }
             }


            KdTree tree = new KdTree();
            //tree.BuildFromList(systems);

            systems.ForEach(tree.Insert);
            systems.Clear();

            for (int i = 0; i < reportsCount; i++)
            {
                // get reports
                string line = Console.ReadLine();
                string[] tokens = line.Split();
                double x = double.Parse(tokens[1]);
                double y = double.Parse(tokens[2]);
                double width = double.Parse(tokens[3]);
                double height = double.Parse(tokens[4]);

                Rectangle searchedRectangle = new Rectangle(x, x + width, y, y + height);
                tree.GetPoints(systems.Add, searchedRectangle, space);
                Console.WriteLine(systems.Count);
                systems.Clear();
            }
        }
    }
}
