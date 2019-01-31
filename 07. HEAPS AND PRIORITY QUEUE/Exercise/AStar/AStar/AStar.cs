using System;
using System.Collections.Generic;

public class AStar
{
    private readonly char[,] map;
    private readonly PriorityQueue<Node> priorityQueue;

    private readonly Dictionary<Node, Node> parents;
    private readonly Dictionary<Node, int> cost;

    public AStar(char[,] map)
    {
        this.map = map;
        this.priorityQueue = new PriorityQueue<Node>();
        this.parents = new Dictionary<Node, Node>();
        this.cost = new Dictionary<Node, int>();
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.priorityQueue.Enqueue(start);
        this.parents[start] = null;
        this.cost[start] = 0;

        while (this.priorityQueue.Count > 0)
        {
            var current = priorityQueue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            var neighbors = GetNeighbors(current);

            foreach (var neighbor in neighbors)
            {
                var newCost = this.cost[current] + 1;
                if (!this.cost.ContainsKey(neighbor) || newCost < this.cost[neighbor])
                {
                    this.cost[neighbor] = newCost;

                    neighbor.F = newCost + GetH(neighbor, goal);
                    this.priorityQueue.Enqueue(neighbor);
                    parents[neighbor] = current;
                }
            }
        }

        if (!parents.ContainsKey(goal))
        {
            return new List<Node>() { start };
        }
        return this.GetPath(goal);
    }

    private IEnumerable<Node> GetPath(Node goal)
    {
        Stack<Node> path = new Stack<Node>();
        path.Push(goal);

        var current = this.parents[goal];

        while (current != null)
        {
            path.Push(current);
            current = this.parents[current];
        }

        return path;
    }

    private IEnumerable<Node> GetNeighbors(Node current)
    {
        var neighbors = new List<Node>();

        this.AddIfValid(neighbors, current.Row + 1, current.Col);
        this.AddIfValid(neighbors, current.Row - 1, current.Col);
        this.AddIfValid(neighbors, current.Row, current.Col + 1);
        this.AddIfValid(neighbors, current.Row, current.Col - 1);

        return neighbors;
    }

    private void AddIfValid(List<Node> neighbors, int row, int col)
    {
        if (this.IsDestinationValid(row, col))
        {
            neighbors.Add(new Node(row, col));
        }
    }

    private bool IsDestinationValid(int row, int col)
    {
        return
               row < this.map.GetLength(0)
               && row >= 0
               && col < this.map.GetLength(1)
               && col >= 0
               && this.map[row, col] != 'W';
    }
}