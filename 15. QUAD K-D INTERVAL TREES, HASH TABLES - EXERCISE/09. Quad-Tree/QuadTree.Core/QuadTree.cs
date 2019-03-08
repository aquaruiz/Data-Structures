using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        Rectangle itemBounds = item.Bounds;
        Node<T> current = this.root;

        if (!itemBounds.IsInside(current.Bounds))
        {
            return false;
        }

        int depth = 1;
        while (true)
        {
            int quandrant = this.GetQuadrant(current, itemBounds);

            if (quandrant == -1)
            {
                break;
            }

            current = current.Children[quandrant];
            depth++;
        }

        current.Items.Add(item);
        TrySplitNode(current, depth);
        this.Count++;
        return true;
    }

    private void TrySplitNode(Node<T> node, int depth)
    {
        if (!node.ShouldSplit || depth >= MaxDepth)
        {
            return;
        }

        int x1 = node.Bounds.X1;
        int y1 = node.Bounds.Y1;
        int leftWidth = node.Bounds.MidX - x1;
        int rightWidth = node.Bounds.Width - leftWidth;

        int upperHeight = node.Bounds.MidY - y1;
        int lowerHeight = node.Bounds.Height - upperHeight;

        if (node.Children == null)
        {
            Node<T> upperRight = new Node<T>(node.Bounds.MidX, y1, rightWidth, upperHeight);
            Node<T> upperLeft = new Node<T>(x1, y1, leftWidth, upperHeight);
            Node<T> lowerLeft = new Node<T>(x1, node.Bounds.MidY, leftWidth, lowerHeight);
            Node<T> lowerRight = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, lowerHeight);

            node.Children = new Node<T>[4];
            node.Children[0] = upperRight;
            node.Children[1] = upperLeft;
            node.Children[2] = lowerLeft;
            node.Children[3] = lowerRight;
        }

        // List<T> itemsToRemove = new List<T>();
        for (int i = node.Items.Count - 1; i >= 0; i--)
        {
            T item = node.Items[i];
            int quadrant = this.GetQuadrant(node, item.Bounds);

            if (quadrant != -1)
            {
                node.Children[quadrant].Items.Add(item);
                // itemsToRemove
                node.Items.RemoveAt(i);
            }
        }

        foreach (var sector in node.Children)
        {
            TrySplitNode(sector, depth + 1);
        }
    }

    private int GetQuadrant(Node<T> node, Rectangle itemBounds)
    {
        if (node.Children == null)
        {
            return -1;
        }

        for (int i = 0; i < 4; i++)
        {
            Rectangle nodeBounds = node.Children[i].Bounds;

            if (itemBounds.IsInside(nodeBounds))
            {
                return i;
            }
        }

        return -1;
    }

    public List<T> Report(Rectangle bounds)
    {
        List<T> result = new List<T>();
        this.ReportCollusions(this.root, result, bounds);
        return result;
    }

    private void ReportCollusions(Node<T> node, List<T> result, Rectangle bounds)
    {
        // if (node.Bounds.Intersects(bounds))
        //  {
        //    result.Add(node);
        // }
         
        int quadrant = this.GetQuadrant(node, bounds);

        if (quadrant == -1)
        {
            this.ForEachDfs(node, (items, depth, q) =>
            {
                result.AddRange(items);
            });
        }
        else
        {
            this.ReportCollusions(node.Children[quadrant], result, bounds);
            result.AddRange(node.Items);
        }
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}
