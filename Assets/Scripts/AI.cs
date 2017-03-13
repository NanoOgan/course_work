using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

public class AI : MonoBehaviour
{

    private static AI _instance;
    private List<Raycaster> sensors;
    private List<RoadNode> nodes;
    private RoadNode curNode;

    public static AI Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    void Awake()
    {
        Instance = this;
        nodes = new List<RoadNode>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSensor(Raycaster sensor)
    {
        this.sensors.Add(sensor);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("CrossRoad"))
        {
            Vector3 pos = collision.transform.position;
            foreach (var sensor in sensors)
            {
                if (nodes.Any(m => m.Name.Equals(pos.ToString())))
                {

                }
                else
                {
                    if (curNode != null)
                    {

                    }
                }
            }
        }
    }
}
