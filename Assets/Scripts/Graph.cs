using System;
using System.Collections.Generic;
using Random = System.Random;
using System.Linq;
using Assets;

public class Graph
{
    public List<Node> nodes { get; private set; }
    public List<Verticie> verticies { get; private set; }

    public Graph()
    {
        nodes = new List<Node>();
        verticies = new List<Verticie>();
    }

    public Graph(List<Node> nodes, List<Verticie> verticies)
    {
        this.nodes = nodes;
        this.verticies = verticies;
    }

    public Pair<Graph, List<Verticie>> GetMST()
    {
        Graph mst = new Graph();
        Random randomizer = new Random();
        List<Node> notUsedNodes = new List<Node>(nodes);
        List<Node> usedNodes = new List<Node>();
        List<Verticie> notUsedVerticies = new List<Verticie>(verticies);
        List<Verticie> usedVertivies = new List<Verticie>();
        var node = notUsedNodes.GetRandomElement();
        usedNodes.Add(node);
        notUsedNodes.Remove(node);
        for (int i = 0; i < 5; ++i)
            notUsedVerticies.Shuffle();
        while (notUsedNodes.Count > 0)
        {
            var tempVerticies = GetConnected(notUsedVerticies, usedNodes, notUsedNodes);
            var verticie = tempVerticies.GetRandomElement();
            notUsedVerticies.Remove(verticie);
            usedVertivies.Add(verticie);
            if (usedNodes.Contains(verticie.first))
            {
                usedNodes.Add(verticie.second);
                notUsedNodes.Remove(verticie.second);
            }
            else
            {
                usedNodes.Add(verticie.first);
                notUsedNodes.Remove(verticie.first);
            }
        }
        int count = verticies.Count;
        mst.nodes = usedNodes;
        mst.verticies = usedVertivies;
        var delta = CreateLoops(notUsedVerticies, count);
        Pair<Graph, List<Verticie>> pair = new Pair<Graph, List<Verticie>>(mst, delta);
        System.GC.Collect();
        return pair;
    }

    private List<Verticie> GetConnected(List<Verticie> _verticies, List<Node> used, List<Node> notUsed)
    {
        List<Verticie> temp = new List<Verticie>();
        foreach (var vert in _verticies)
        {
            if ((used.Contains(vert.first) && !used.Contains(vert.second)) || (!used.Contains(vert.first) && used.Contains(vert.second)))
            {
                temp.Add(vert);
            }

        }
        return temp;
    }

    private List<Verticie> CreateLoops(List<Verticie> verticies, int count)
    {
        int percentage = (int)((float)count * 3 / 100);
        List<Verticie> newVert = new List<Verticie>();
        for (int i = 0; i < 5; ++i)
            verticies.Shuffle();
        for (int i = 0; i < percentage; ++i)
        {
            var vert = verticies.GetRandomElement();
            newVert.Add(vert);
            verticies.Remove(vert);
        }
        return newVert;
    }

    public Stack<Node> GetMinPathFromTo(Node from, Node to)
    {
        Stack<Node> path = new Stack<Node>();
        Queue<Node> wave = new Queue<Node>();
        List<Node> _data = new List<Node>();
        var _verticies = new List<Verticie>(verticies);
        wave.Enqueue(from);
        while (true)
        {
            Node curent = wave.Dequeue();
            if (curent == to)
                break;
            var count = _verticies.Count;
            for (int i = 0; i < count; ++i)
            {
                var verticie = verticies[i];
                verticie.second.parent = curent;
                if (verticie.first == curent)
                {
                    wave.Enqueue(verticie.second);
                }
                else if (verticie.second == curent)
                {
                    wave.Enqueue(verticie.first);
                }
                count--;
                _verticies.RemoveAt(i);
                i--;
                _data.Add(curent);
                curent = wave.Dequeue();
            }
        }
        var par = to.parent;
        path.Clear();
        while (par != from)
        {
            path.Push(par);
            _data.Add(par);
            par = par.parent;
        }
        while (wave.Count > 0)
        {
            wave.Dequeue().parent = null;
        }
        foreach (var element in _data)
        {
            element.parent = null;
        }
        wave.Clear();
        _data.Clear();
        return path;
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }

    }
}
