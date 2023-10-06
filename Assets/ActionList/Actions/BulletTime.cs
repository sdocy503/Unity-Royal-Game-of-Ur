using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class BulletTime : Action
    {
        protected float scale;
        protected float startingScale;

        public BulletTime(GameObject attatchedObject_, float duration_, float scale_, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
            base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
            => (scale) = (scale_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startingScale = Time.timeScale;
                }

                Time.timeScale = Mathf.Lerp(startingScale, scale, (duration - timer) / duration);
            }
        }

        public override void Reverse()
        {
            base.Reverse();
            float temp = startingScale;
            startingScale = scale;
            scale = temp;
        }
    }
}