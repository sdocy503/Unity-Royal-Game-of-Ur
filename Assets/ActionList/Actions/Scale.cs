using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class Scale : Action
    {
        protected Vector2 startScale;
        protected Vector2 endScale;
        protected AnimationCurve curve;

        public Scale(GameObject attatchedObject_, Vector2 endScale_, float duration_, AnimationCurve curve_ = null, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (endScale, startScale, curve) = (endScale_, attatchedObject_.transform.localScale, curve_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startScale = attatchedObject.transform.localScale;
                }

                if (curve == null)
                {
                    attatchedObject.transform.localScale = Smooth(startScale, endScale, completePercent);
                }
                else
                {
                    attatchedObject.transform.localScale = Lerp(startScale, endScale, curve.Evaluate(completePercent));
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
            Vector3 swap = endScale;
            endScale = startScale;
            startScale = swap;
        }
    }
}