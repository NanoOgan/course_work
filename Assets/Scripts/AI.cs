using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.States;
using Assets;

public class AI : MonoBehaviour
{

    private static AI _instance;
    private List<Raycaster> sensors;
    private List<RoadNode> nodes;
    private RoadNode curNode;
    private Graph mapGraph;
    private AState state;
    private Stack<Node> path;
    public List<RoadNode> RoadNodes
    {
        get
        {
            return nodes;
        }
    }
    public RoadNode CurNode
    {
        get
        {
            return curNode;
        }
        set
        {
            curNode = value;
        }
    }
    public Graph MaphGraph
    {
        get
        {
            return mapGraph;
        }
    }
    public Stack<Node> Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }

    public static AI Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    void Awake()
    {
        Instance = this;
        nodes = new List<RoadNode>();
        state = new IdleState(this);
        mapGraph = new Graph();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    public void AddSensor(Raycaster sensor)
    {
        this.sensors.Add(sensor);
    }

    public void OnCollisionEnter(Collision collision)
    {
        state.OnCollisionEnter(collision);
    }

    public void ChangeState(AState state)
    {
        this.state = state;
    }

    public bool[] IsWalls()
    {
        bool[] isWalls = new bool[4];
        foreach (Raycaster sensor in sensors)
        {
            if (sensor.RayDirection == Direction.Up)
                isWalls[0] = true;
            else
                isWalls[0] = false;
            if (sensor.RayDirection == Direction.Right)
                isWalls[1] = true;
            else
                isWalls[1] = false;
            if (sensor.RayDirection == Direction.Down)
                isWalls[2] = true;
            else
                isWalls[2] = false;
            if (sensor.RayDirection == Direction.Left)
                isWalls[3] = true;
            else
                isWalls[3] = false;
        }
        return isWalls;
    } 
}
