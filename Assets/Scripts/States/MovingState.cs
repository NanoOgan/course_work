using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.States
{
    public class MovingState : AState
    {
        Stack<Node> path;
        Transform transform;
        Vector3 nextPos;
        private float speed = 0.75f;

        public MovingState(AI mainAi) : base(mainAi)
        {
            path = mainAi.Path;
            transform = mainAi.transform;
            Node next = path.Pop();
            nextPos = new Vector3(next.x, transform.position.y, next.y);
        }

        public override void ChangeState()
        {
            mainAi.ChangeState(new WalkingState(mainAi));
        }

        public override void OnCollisionEnter(Collision collision)
        {
            
        }

        public override void Update()
        {
            if (Vector3.Distance(transform.position, nextPos) < float.Epsilon)
            {
                if (path.Count > 0)
                {
                    Node node = path.Pop();
                    nextPos = new Vector3(node.x, transform.position.y, node.y);
                }
                else
                    ChangeState();
            }
            else
            {
                transform.position = Vector3.MoveTowards(mainAi.transform.position, nextPos, speed * Time.deltaTime);
            }
        }
    }
}
