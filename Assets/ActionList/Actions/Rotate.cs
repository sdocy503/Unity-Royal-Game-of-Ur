using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class Rotate : Action
    {
        protected Vector3 startPos;
        protected Vector3 currentStartPos;
        protected Vector3 endPos;

        public Rotate(GameObject attatchedObject_, Vector3 endPos_, float duration_, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
            base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
            => (endPos) = (endPos_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startPos = attatchedObject.transform.rotation.eulerAngles;
                }

                attatchedObject.transform.rotation = Quaternion.Euler(Vector3.Lerp(startPos, endPos, completePercent));
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