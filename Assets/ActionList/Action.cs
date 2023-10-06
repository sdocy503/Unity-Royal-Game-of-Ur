using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ActionListSystem
{
    public class Action
    {
        // Protected action related variables
        protected bool firstUpdate = false;           // Checks if we've done any updates yet
        protected float initialDelay = 0;
        protected float delay = 0;                  // Delay for action, 0 means no delay
        protected float duration = 0;               // Duration of action, any negative number means infinite
        protected float timer = 0;                  // Current timer of action, counts down from duration
        protected float completePercent = 0;        // From 0 to 1, 0 meaning at the begining and 1 meaning done
        protected bool blocking = false;            // Whether this action blocks other actions after it in the list
        protected GameObject attatchedObject = null;// The GameObject this action is attatched to   
        protected bool done = false;
        protected float reverseRandDelay = 0.0f;

        public Action() { }
        public Action(float duration_) =>
            (duration, blocking, timer) = (duration_, true, duration_);
        public Action(float duration_, float delay_, bool blocking_, GameObject attatchedObject_, float reverseRandDelay_) =>
            (duration, delay, blocking, attatchedObject, timer, initialDelay, reverseRandDelay) = (duration_, delay_, blocking_, attatchedObject_, duration_, delay_, reverseRandDelay_);

        public bool TimerCount(float dt)
        {
            if (duration >= 0.0f)
            {
                if (delay > 0.0f)
                {
                    delay -= dt;

                    if (delay > 0.0f)
                        return false;

                    //timer += delay;
                    delay = 0.0f;
                }
                else
                {
                    timer -= dt;
                }

                timer = Math.Max(0, timer);

                if (duration == 0f)
                {
                    completePercent = 1.0f;
                }
                else
                {
                    completePercent = (duration - timer) / duration;
                }

                if (completePercent >= 1.0f)
                {
                    done = true;
                }
            }

            return true;
        }

        protected bool FirstUpdate()
        {
            if (!firstUpdate)
            {
                firstUpdate = true;

                return true;
            }

            return false;
        }

        public virtual void Update(float dt)
        {
            FirstUpdate();
            TimerCount(dt);
        }

        public virtual void FixedUpdate(float dt)
        {

        }

        public virtual void Exit()
        { }

        public virtual void Reverse()
        {
            if (duration > 0f)
            {
                timer = duration - timer;
                completePercent = (duration - timer) / duration;
                if (completePercent < 1.0f)
                {
                    done = false;
                }
                else
                {
                    done = true;
                }
            }
            else
            {
                done = false;
            }
            //delay = initialDelay - delay;
            //delay = UnityEngine.Random.Range(0f, reverseRandDelay);

            //firstUpdate = false;
        }

        public bool IsBlocking()
        {
            return blocking;
        }

        public bool IsDone()
        {
            return done;
        }

        public float InitialDelay()
        {
            return initialDelay;
        }

        public float Delay()
        {
            return delay;
        }

        public float InitialDuration()
        {
            return duration;
        }

        public void SetDelay(float delay_)
        {
            delay = delay_;
        }

        public static void SwapDelay(Action action1, Action action2)
        {
            float temp = action1.initialDelay;
            action1.initialDelay = action2.initialDelay;
            action2.initialDelay = temp;
            temp = action1.delay;
            action1.delay = action2.delay;
            action2.delay = temp;
        }
    }

}