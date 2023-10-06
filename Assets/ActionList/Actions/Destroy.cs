using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class Destroy : Action
    {
        public Destroy(GameObject attatchedObject_, float duration_, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        { }

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    UnityEngine.Object.Destroy(attatchedObject);
                }
            }
        }
    }
}
