using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class Move : Action
    {
        protected Vector3 startPos;
        protected Vector3 endPos;
        protected Vector3 moveError;
        protected AnimationCurve curve;

        public Move(GameObject attatchedObject_, Vector3 endPos_, float duration_, Vector3 moveError_ = default(Vector3), AnimationCurve curve_ = null, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (endPos, moveError, startPos, curve) = (endPos_, moveError_, attatchedObject_.transform.position, curve_);

        public Move(GameObject attatchedObject_, GameObject endPos_, float duration_, Vector3 moveError_ = default(Vector3), AnimationCurve curve_ = null, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (endPos, moveError, startPos, curve) = (endPos_.transform.position, moveError_, attatchedObject_.transform.position, curve_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startPos = attatchedObject.transform.position;
                    moveError *= new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    endPos += moveError;
                }

                if (curve == null)
                {
                    attatchedObject.transform.position = Smooth(startPos, endPos, completePercent);
                }
                else
                {
                    attatchedObject.transform.position = Lerp(startPos, endPos, curve.Evaluate(completePercent));
                }
            }
        }

        Vector3 Lerp(Vector3 start, Vector3 end, float t)
        {
            float res1;
            float res2;
            float res3;
            float x1 = start.x;
            float x2 = end.x;
            float y1 = start.y;
            float y2 = end.y;
            float z1 = start.z;
            float z2 = end.z;

            res1 = Mathf.Lerp(x1, x2, t);
            res2 = Mathf.Lerp(y1, y2, t);
            res3 = Mathf.Lerp(z1, z2, t);

            return new Vector3(res1, res2, res3);
        }

        Vector3 Smooth(Vector3 start, Vector3 end, float t)
        {
            float res1;
            float res2;
            float res3;
            float x1 = start.x;
            float x2 = end.x;
            float y1 = start.y;
            float y2 = end.y;
            float z1 = start.z;
            float z2 = end.z;

            res1 = Mathf.SmoothStep(x1, x2, t);
            res2 = Mathf.SmoothStep(y1, y2, t);
            res3 = Mathf.SmoothStep(z1, z2, t);

            return new Vector3(res1, res2, res3);
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