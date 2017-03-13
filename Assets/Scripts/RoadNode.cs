using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoadNode
    {
        private string name;

        private string position;

        private Dictionary<RoadNode, bool> otherNodes;

        public RoadNode(string name)
        {
            this.name = name;
            otherNodes = new Dictionary<RoadNode, bool>();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }


    }
}
