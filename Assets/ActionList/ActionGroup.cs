using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionListSystem
{
    public class ActionGroup
    {
        protected List<Action> actionGroup;
        protected List<Action> removeList;
        bool blocking = false;
        bool done = false;
        public bool deleteOnCompletion = false;
        protected float duration = 0.0f;

        public ActionGroup(bool blocking_ = false, bool deleteOnCompletion_ = true)
        {
            actionGroup = new List<Action>();
            removeList = new List<Action>();
            blocking = blocking_;
            deleteOnCompletion = deleteOnCompletion_;
        }

        public void Update(float dt)
        {
            duration += dt;

            foreach (Action action in actionGroup)
            {
                // Check to see if it's done and needs to be erased
                if (!action.IsDone())
                {
                    action.Update(dt);
                    //action.CheckIfDone();
                    if (action.IsDone())
                    {
                        action.Exit();
                    }
                    // If the action is blocking we stop updating at that action
                    if (action.IsBlocking())
                    {
                        break;
                    }
                }
            }

            foreach (Action action in removeList)
            {
                actionGroup.Remove(action);
            }
            removeList.Clear();
        }

        public void FixedUpdate(float dt)
        {
            foreach (Action action in actionGroup)
            {
                // Check to see if it's done and needs to be erased
                if (!action.IsDone())
                {
                    action.FixedUpdate(dt);
                    //action.CheckIfDone();
                    if (action.IsDone())
                    {
                        action.Exit();
                    }
                    // If the action is blocking we stop updating at that action
                    if (action.IsBlocking())
                    {
                        break;
                    }
                }
            }
        }

        public void Reverse()
        {
            //for (int i = 0; i < actionGroup.Count / 2; i++)
            //{
            //    Action.SwapDelay(actionGroup[i], actionGroup[actionGroup.Count - i - 1]);
            //}

            foreach (Action action in actionGroup)
            {
                float newDelay = Mathf.Max(duration - (action.InitialDelay() + action.InitialDuration()), 0f);

                action.SetDelay(newDelay);

                action.Reverse();
            }

            duration = 0f;
            done = false;
            //actionGroup.Reverse();
        }

        public void Add(Action action)
        {
            actionGroup.Add(action);
        }

        public Action Get(int index)
        {
            return actionGroup[index];
        }

        public void Remove(Action action)
        {
            removeList.Add(action);
        }

        public bool CheckIfDone()
        {
            foreach (Action action in actionGroup)
            {
                if (!action.IsDone())
                {
                    return false;
                }
            }

            return done = true;
        }

        public bool IsDone()
        {
            return done;
        }

        public bool IsBlocking()
        {
            return blocking;
        }
    }
}