using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.States
{

    public abstract class AState
    {
        protected AI mainAi;


        public AState(AI mainAi)
        {
            this.mainAi = mainAi;
        }

        protected Stack<Node> FindPath(RoadNode from, RoadNode to)
        {
            Node nodeFrom = from.Node;
            Node nodeTo = to.Node;
            return mainAi.MaphGraph.GetMinPathFromTo(nodeFrom, nodeTo);
        }

        public abstract void Update();
        public abstract void OnCollisionEnter(Collision collision);
        public abstract void ChangeState();

    }
}
