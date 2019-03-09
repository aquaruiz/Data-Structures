using System; 
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class ShoppingCenter
{
    private Dictionary<string, OrderedBag<Product>> byProducer;
    private Dictionary<string, Bag<Product>> byNameAndProducer;
    private Dictionary<string, OrderedBag<Product>> byName;
    private OrderedDictionary<double, Bag<Product>> byPrice;

    public ShoppingCenter()
    {
        this.byProducer = new Dictionary<string, OrderedBag<Product>>();
        this.byNameAndProducer = new Dictionary<string, Bag<Product>>();
        this.byName = new Dictionary<string, OrderedBag<Product>>();
        this.byPrice = new OrderedDictionary<double, Bag<Product>>();
        // this.byPrice = new OrderedBag<Product>((x, y) => x.Price.CompareTo(y.Price));
    }

    public void Add(Product product)
    {
        this.byProducer.AppendValueToKey(product.Producer, product);

        this.byNameAndProducer.AppendValueToKey(product.Name + product.Producer, product);

        this.byName.AppendValueToKey(product.Name, product);

        this.byPrice.AppendValueToKey(product.Price, product);
    }

    public int DeleteProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return 0;
        }

        IEnumerable<Product> productsToRemove = this.byProducer[producer];
        this.byProducer.Remove(producer);

        int count = 0;

        foreach (var product in productsToRemove)
        {
            string name = product.Name;
            this.byName[name].Remove(product);
            this.byNameAndProducer[product.Name + product.Producer].Remove(product);
            this.byPrice[product.Price].Remove(product);
            count++;
        }

        return productsToRemove.Count();
    }

    public int DeleteProductsByProducerAndName(string productName, string producer)
    {
        var key = productName + producer;

        if (!this.byNameAndProducer.ContainsKey(key))
        {
            return 0;
        }

        var productsToRemove = this.byNameAndProducer[key];
        int count = 0;
        this.byNameAndProducer.Remove(key);

        foreach (var p in productsToRemove)
        {
            string name = p.Name;
            this.byName[name].Remove(p);
            this.byPrice[p.Price].Remove(p);
            this.byProducer[producer].Remove(p);
            count++;
        }

        return productsToRemove.Count; 
    }

    public IEnumerable<Product> FindProductsByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byName[name];
    }

    public IEnumerable<Product> FindProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byProducer[producer];
    }

    public IEnumerable<Product> FindProductsByPriceRange(double low, double high)
    {
        var range =  this.byPrice.Range(low, true, high, true).Values;

        var result = new OrderedBag<Product>();

        foreach (var bag in range)
        {
            foreach (var product in bag)
            {
                result.Add(product);
            }
        }

        return result;
    }
}