using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class MoveVelocity : Action
    {
        protected Vector3 startPos;
        protected Vector3 endPos;
        protected Vector3 moveError;
        protected Rigidbody2D physics;

        public MoveVelocity(GameObject attatchedObject_, Vector3 endPos_, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(-1f, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (endPos, startPos) = (endPos_, attatchedObject_.transform.position);

        public MoveVelocity(GameObject attatchedObject_, GameObject endPos_, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(-1f, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (endPos, startPos) = (endPos_.transform.position, attatchedObject_.transform.position);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startPos = attatchedObject.transform.position;
                    moveError *= new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    endPos += moveError;
                    physics = attatchedObject.GetComponent<Rigidbody2D>();
                }

                if ((attatchedObject.transform.position - endPos).x > 0.1f)
                    physics.velocity = new Vector3(-5f, physics.velocity.y);
                else if ((attatchedObject.transform.position - endPos).x < -0.1f)
                    physics.velocity = new Vector3(5f, physics.velocity.y);
                else
                {
                    physics.velocity = new Vector3(0f, physics.velocity.y);
                    duration = 0f;
                }
            }
        }

        public override void Reverse()
        {
            base.Reverse();
            Vector3 swap = endPos;
            endPos = startPos;
            startPos = swap;
        }
    }
}