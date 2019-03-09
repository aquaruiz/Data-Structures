using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

class Program
{
    static void Main()
    {
        int commandsNumber = int.Parse(Console.ReadLine());
        ShoppingCenter shoppingCenter = new ShoppingCenter();

        for (int i = 0; i < commandsNumber; i++)
        {
            string line = Console.ReadLine();
            int firstSpace = line.IndexOf(" ");

            string command = line.Substring(0, firstSpace);
            string[] tokens = line.Substring(firstSpace + 1).Split(';');

            switch (command)
            {
                case "AddProduct":
                    string name = tokens[0];
                    double price = double.Parse(tokens[1]);
                    string producer = tokens[2];

                    Product p = new Product(name, price, producer);

                    shoppingCenter.Add(p);
                    Console.WriteLine("Product added");
                    break;
                case "DeleteProducts":
                    if (tokens.Length == 1)
                    {
                        producer = tokens[0];
                        int countDeleted = shoppingCenter.DeleteProductsByProducer(producer);

                        if (countDeleted == 0)
                        {
                            Console.WriteLine("No products found");
                        }
                        else
                        {
                            Console.WriteLine(countDeleted.ToString() + " products deleted");
                        }
                    }
                    else
                    {
                        name = tokens[0];
                        producer = tokens[1];
                        int countDeleted = shoppingCenter.DeleteProductsByProducerAndName(name, producer);

                        if (countDeleted == 0)
                        {
                            Console.WriteLine("No products found");
                        }
                        else
                        {
                            Console.WriteLine(countDeleted.ToString() + " products deleted");
                        }
                    }

                    break;
                case "FindProductsByName":
                    name = tokens[0];
                    List<Product> result = shoppingCenter.FindProductsByName(name).ToList();
                    if (result.Count != 0)
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                    }
                    else
                    {
                        Console.WriteLine("No products found");
                    }

                    break;
                case "FindProductsByProducer":
                    producer = tokens[0];
                    result = shoppingCenter.FindProductsByProducer(producer).ToList();
                    if (result.Count != 0)
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                    }
                    else
                    {
                        Console.WriteLine("No products found");
                    }

                    break;
                case "FindProductsByPriceRange":
                    double priceLow = double.Parse(tokens[0]);
                    double priceHigh = double.Parse(tokens[1]);
                    result = shoppingCenter.FindProductsByPriceRange(priceLow, priceHigh)
                            .ToList();

                    if (result.Count != 0)
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                    }
                    else
                    {
                        Console.WriteLine("No products found");
                    }

                    break;
            }
        }
    }
}