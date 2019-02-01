using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    // !!!!!!!!!!!!!!!!!!!!
    private Dictionary<T, Node> nodesByValue;

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodesByValue = new Dictionary<T, Node>();
        this.nodesByValue.Add(this.root.Value, this.root);
    }

    public int Count
    {
        get
        {
            return this.nodesByValue.Count;
        }
    }

    public void Add(T parent, T child)
    {
        if (!this.nodesByValue.ContainsKey(parent))
        {
            throw new ArgumentException("Element does not exists");
        }

        if (this.nodesByValue.ContainsKey(child))
        {
            throw new ArgumentException("Child already exists");
        }

        Node parentNode = this.nodesByValue[parent];
        Node childNode = new Node(child, parentNode);

        this.nodesByValue.Add(child, childNode);
        parentNode.Children.Add(childNode);
    }

    public void Remove(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException("Element does not exist!");
        }

        Node node = this.nodesByValue[element];

        if (node.Parent == null)
        {
            // deleting root!!!!
            throw new InvalidOperationException("Can not Delete root node!");
        }

        Node parentNode = node.Parent;
        parentNode.Children.Remove(node);

        foreach (var child in node.Children)
        {
            parentNode.Children.Add(child);
            child.Parent = parentNode;
        }

        this.nodesByValue.Remove(element);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.nodesByValue.ContainsKey(item))
        {
            throw new ArgumentException("Element does not exist!");
        }

        Node parent = this.nodesByValue[item];
        return parent.Children.Select(x => x.Value);
    }

    public T GetParent(T item)
    {
        if (!this.nodesByValue.ContainsKey(item))
        {
            throw new ArgumentException("Element does not exist!");
        }

        Node child = this.nodesByValue[item];

        return child.Parent != null
                ? child.Parent.Value
                : default(T);
    }

    public bool Contains(T value)
    {
        return this.nodesByValue.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        HashSet<T> result = new HashSet<T>(this.nodesByValue.Keys);
        result.IntersectWith(new HashSet<T>(other.nodesByValue.Keys));
        return result;
    } 

    public IEnumerator<T> GetEnumerator()
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.root);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();

            foreach (var child in currentNode.Children)
            {
                queue.Enqueue(child);
            }

            yield return currentNode.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node
    {
        public Node Parent { get; set; }
        public T Value { get; set; }
        public List<Node> Children { get; private set; }

        public Node (T value, Node parent = null)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<Node>();
        }
    }
}