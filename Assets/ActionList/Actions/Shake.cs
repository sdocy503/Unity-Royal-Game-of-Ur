using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionListSystem
{
    public class Shake : Action
    {
        protected Vector3 shakeAmount;
        protected AnimationCurve curve;
        protected Vector3 startPos;

        public Shake(GameObject attatchedObject_, Vector3 shakeAmount_, float duration_, AnimationCurve curve_ = null, float delay_ = 0.0f, bool blocking_ = false, float reverseRandDelay_ = 0f) :
        base(duration_, delay_, blocking_, attatchedObject_, reverseRandDelay_)
        => (shakeAmount, curve) = (shakeAmount_, curve_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    startPos = attatchedObject.transform.position;
                }

                Vector3 shakePos = new Vector3();

                if (curve == null)
                {
                    shakePos.x = startPos.x + UnityEngine.Random.Range(-shakeAmount.x, shakeAmount.x) * completePercent;
                    shakePos.y = startPos.y + UnityEngine.Random.Range(-shakeAmount.y, shakeAmount.y) * completePercent;
                    shakePos.z = startPos.z + UnityEngine.Random.Range(-shakeAmount.z, shakeAmount.z) * completePercent;
                }
                else
                {
                    shakePos.x = startPos.x + UnityEngine.Random.Range(-shakeAmount.x, shakeAmount.x) * curve.Evaluate(completePercent);
                    shakePos.y = startPos.y + UnityEngine.Random.Range(-shakeAmount.y, shakeAmount.y) * curve.Evaluate(completePercent);
                    shakePos.z = startPos.z + UnityEngine.Random.Range(-shakeAmount.z, shakeAmount.z) * curve.Evaluate(completePercent);
                }

                attatchedObject.transform.position = shakePos;
            }
        }

        public override void Exit()
        {
            base.Exit();
            attatchedObject.transform.position = startPos;
        }

        public override void Reverse()
        {
            base.Reverse();
        }
    }
}