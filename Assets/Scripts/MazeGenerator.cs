using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System.Threading;

public class MazeGenerator : MonoBehaviour
{

    public GameObject node;
    public GameObject verticie;
    public GameObject loop;
    private bool isPassed = false;
    private Graph.Pair<Graph, List<Verticie>> pair;
    private int[,] map;
    // Use this for initialization
    void Start()
    {
        map = new int[41, 41];
        Thread t = new Thread(DoId);
        t.Start();
    }

    void Update()
    {
        if (isPassed)
        {
            StartCoroutine(BuildMap());
            isPassed = false;
        }
    }

    private void DoId()
    {
        List<Node> nodes = new List<Node>();
        List<Verticie> verticies = new List<Verticie>();
        for (int i = 0; i < 20; ++i)
        {
            for (int j = 0; j < 20; ++j)
            {
                Node node = new Node(2*i, 2*j);
                nodes.Add(node);
            }
        }
        for (int i = 0; i < 20; ++i)
        {
            for (int j = 0; j < 20; ++j)
            {
                Verticie first = new Verticie();
                if (i != 19)
                {
                    first.first = nodes[i + 20 * j];
                    first.second = nodes[i + 20 * j + 1];
                    verticies.Add(first);
                }
                Verticie second = new Verticie();
                if (j != 19)
                {
                    second.first = nodes[i + 20 * j];
                    second.second = nodes[i + 20 * (j + 1)];
                    verticies.Add(second);
                }
            }

        }
        Graph graph = new Graph(nodes, verticies);
        pair = graph.GetMST();
        Graph mst = pair.First;
        GenerateMap(mst, pair.Second);
    }

    private void GenerateVerticies(List<Verticie> verticies)
    {
        for (int i = 0; i < verticies.Count; ++i)
        {
            var t =  verticies[i].MidPoint();
            map[(int)t.x+1, (int)t.z+1] = 2;
        }
        isPassed = true;
    }

    private void GenerateMap(Graph graph, List<Verticie> _verticies)
    {
        Vector3 _temp =  (new Vector3(graph.nodes[0].x, 0, graph.nodes[0].y));
        map[(int)_temp.x+1, (int)_temp.z+1] = 1;
        for (int i = 0; i < graph.verticies.Count; ++i)
        {
            Vector3 temp = new Vector3(graph.nodes[i + 1].x, 0, graph.nodes[i + 1].y);
            map[(int)temp.x+1, (int)temp.z+1] = 1;
            var t =  graph.verticies[i].MidPoint();
            map[(int)t.x+1, (int)t.z+1] = 2;
        }
        GenerateVerticies(_verticies);
    }

    private IEnumerator BuildMap()
    {
        for (int i = 0; i < 41; ++i)
        {
            for (int j = 0; j < 41; ++j)
            {
                if (map[i, j] == 0)
                {
                    var tempPos = new Vector3(i, 0, j);
                    Instantiate(node, tempPos, Quaternion.identity);
                }
                else if (map[i, j] == 1)
                {
                    var tempPos = new Vector3(i, -1, j);
                    Instantiate(verticie, tempPos, Quaternion.identity);
                }
                else if (map[i, j] == 2)
                {
                    var tempPos = new Vector3(i, -1, j);
                    Instantiate(loop, tempPos, Quaternion.identity);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
