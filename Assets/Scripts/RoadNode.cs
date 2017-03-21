using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoadNode
    {
        private string name;

        private Vector3 position;

        private List<bool> otherNodes;

        private List<bool> wasInNode;

        private Node node;

        public Node Node
        {
            get
            {
                return node;
            }
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }
        }

        public RoadNode(string name,Vector3 position , Node node)
        {
            this.name = name;
            this.position = position;
            otherNodes = new List<bool>(4);
            wasInNode = new List<bool>(4);
            for (int i = 0; i < 4; ++i)
            {
                otherNodes[i] = false;
                wasInNode[i] = false;
            }

            this.node = node;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void IsNotWall(int i)
        {
            otherNodes[i] = true;
        }

        public void WasInNode(int i)
        {
            wasInNode[i] = true;
        }

        public int HasFreeNode()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (otherNodes[i] && !wasInNode[i])
                    return i;
            }
            return -1;
        }
    }
}
