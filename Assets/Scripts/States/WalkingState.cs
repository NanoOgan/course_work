using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class WalkingState : AState
    {
        private Vector3 targetPos;
        private float speed = 0.75f;

        public WalkingState(AI mainAi) : base(mainAi)
        {

        }

        public override void ChangeState()
        {
            mainAi.ChangeState(new MovingState(mainAi));
        }

        public override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("CrossRoad"))
            {
                var isWalls = mainAi.IsWalls();
                Transform tr = collision.gameObject.transform;
                Node node = new Node((int)tr.position.x, (int)tr.position.z);
                RoadNode roadNode = new RoadNode(tr.position.ToString(), tr.position, node);
                var realNode = mainAi.RoadNodes.FirstOrDefault(x => x.Name.Equals(roadNode.Name));
                if (realNode != null)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        if (isWalls[i])
                            ProcessWall(i, tr, node, roadNode);
                    }
                    mainAi.RoadNodes.Add(roadNode);
                    mainAi.CurNode = roadNode;
                    if (isWalls[0])
                        targetPos = tr.position + new Vector3(1, 0, 0);
                    else if (isWalls[1])
                        targetPos = tr.position + new Vector3(0, 0, 1);
                    else if (isWalls[2])
                        targetPos = tr.position + new Vector3(-1, 0, 0);
                    else if (isWalls[3])
                        targetPos = tr.position + new Vector3(0, 0, -1);
                }
                else
                {
                    Vector3 diff = mainAi.CurNode.Position - realNode.Position;
                    if (diff == new Vector3(1, diff.y, 0))
                    {
                        mainAi.CurNode.WasInNode(2);
                        realNode.WasInNode(0);
                    }
                    else if (diff == new Vector3(0, diff.y, 1))
                    {
                        mainAi.CurNode.WasInNode(3);
                        realNode.WasInNode(1);
                    }
                    else if (diff == new Vector3(-1, diff.y, 0))
                    {
                        mainAi.CurNode.WasInNode(0);
                        realNode.WasInNode(2);
                    }
                    else if (diff == new Vector3(0, diff.y, -1))
                    {
                        mainAi.CurNode.WasInNode(1);
                        realNode.WasInNode(3);
                    }
                    mainAi.CurNode = realNode;
                    var nodes = mainAi.RoadNodes.Where(x => x.HasFreeNode() != -1);
                    if (nodes.Count() > 0)
                    {
                        RoadNode nearestNode = null;
                        float dist = float.MaxValue;
                        foreach (var _node in nodes)
                        {
                            if (Vector3.Distance(_node.Position, realNode.Position) < dist)
                                nearestNode = _node;
                        }
                        mainAi.Path = FindPath(realNode, nearestNode);
                        ChangeState();
                    }
                    mainAi.ChangeState(new IdleState(mainAi));
                }
            }
        }

        private void ProcessWall(int i, Transform transform, Node node, RoadNode roadNode)
        {
            mainAi.CurNode.IsNotWall(i);
            roadNode.IsNotWall((i + 2) % 4);
            Verticie vert = new Verticie();
            vert.first = node;
            vert.second = mainAi.CurNode.Node;
            mainAi.MaphGraph.nodes.Add(node);
            mainAi.MaphGraph.verticies.Add(vert);
        }



        public override void Update()
        {
            mainAi.transform.position = Vector3.MoveTowards(mainAi.transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
