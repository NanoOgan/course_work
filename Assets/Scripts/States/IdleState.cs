using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class IdleState : AState
    {
        
        public IdleState(AI mainAi):base(mainAi){}

        public override void ChangeState()
        {
            mainAi.ChangeState(new WalkingState(mainAi));
        }

        public override void OnCollisionEnter(Collision collision)
        {
            
        }

        public override void Update()
        {
            if (mainAi.RoadNodes.Count == 0)
                ChangeState();
        }
    }
}
