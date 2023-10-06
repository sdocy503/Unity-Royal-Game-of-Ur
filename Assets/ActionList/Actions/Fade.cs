using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ActionListSystem
{
    public class Fade : Action
    {
        protected float startAlpha;
        protected float endAlpha;
        protected Image sprite = null;
        protected TMP_Text text;

        public Fade(GameObject attatchedObject_, float endAlpha_, float duration_, float delay_ = 0.0f, bool blocking_ = false) :
            base(duration_, delay_, blocking_, attatchedObject_, 0f)
            => (endAlpha) = (endAlpha_);

        public override void Update(float dt)
        {
            if (TimerCount(dt))
            {
                if (FirstUpdate())
                {
                    sprite = attatchedObject.GetComponentInChildren<Image>();
                    text = attatchedObject.GetComponentInChildren<TMP_Text>();

                    if (sprite != null)
                    {
                        Color col = sprite.color;
                        startAlpha = col.a;
                    }
                    if (text)
                    {
                        Color col = text.color;
                        startAlpha = col.a;
                    }
                }

                float alpha = Mathf.Lerp(startAlpha, endAlpha, completePercent);

                if (sprite != null)
                {
                    Color col = sprite.color;
                    col.a = alpha;
                    sprite.color = col;
                }
                if (text)
                {
                    Color col = text.color;
                    col.a = alpha;
                    text.color = col;
                }
            }
        }
        public override void Reverse()
        {
            base.Reverse();
            float swap = endAlpha;
            endAlpha = startAlpha;
            startAlpha = swap;
        }
    }
}