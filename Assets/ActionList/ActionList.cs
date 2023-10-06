using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionListSystem
{
    public class ActionList : MonoBehaviour
    {
        List<ActionGroup> actionList = new List<ActionGroup>();
        List<ActionGroup> eraseList = new List<ActionGroup>();
        List<ActionGroup> reverseList = new List<ActionGroup>();
        public bool allDone = false;
        float dtMod = 1.5f;
        bool reverseAll = false;
        public bool pause = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        void FixedUpdate()
        {
            if (!pause)
            {
                bool allDoneTemp = true;

                // Update each action in the action list
                foreach (ActionGroup actionGroup in actionList)
                {
                    if (!actionGroup.IsDone())
                    {
                        allDoneTemp = false;
                        actionGroup.FixedUpdate(Time.fixedUnscaledDeltaTime * dtMod);

                        if (actionGroup.CheckIfDone() && actionGroup.deleteOnCompletion)
                        {
                            eraseList.Add(actionGroup);
                        }

                        if (actionGroup.IsBlocking())
                        {
                            break;
                        }
                    }
                }

                allDone = allDoneTemp;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!pause)
            {
                bool allDoneTemp = true;

                // Update each action in the action list
                foreach (ActionGroup actionGroup in actionList)
                {
                    if (!actionGroup.IsDone())
                    {
                        allDoneTemp = false;
                        actionGroup.Update(Time.unscaledDeltaTime * dtMod);

                        if (actionGroup.CheckIfDone() && actionGroup.deleteOnCompletion)
                        {
                            eraseList.Add(actionGroup);
                        }

                        if (actionGroup.IsBlocking())
                        {
                            break;
                        }
                    }
                }

                allDone = allDoneTemp;
            }

            // Go through the erase list and remove all the actions in it from the action list
            foreach (ActionGroup actionGroup in eraseList)
            {
                actionList.Remove(actionGroup);
            }
            eraseList.Clear();

            foreach (ActionGroup actionGroup in reverseList)
            {
                actionGroup.Reverse();
            }
            reverseList.Clear();
            if (reverseAll)
            {
                actionList.Reverse();
                reverseAll = false;
            }
        }

        public void ClearActions()
        {
            foreach (ActionGroup actionGroup in actionList)
            {
                eraseList.Add(actionGroup);
            }
        }

        public void AddToList(Action action, bool blocking = false, bool deleteOnCompletion = false)
        {
            ActionGroup actionGroup = new ActionGroup(blocking, deleteOnCompletion);

            actionGroup.Add(action);
            AddToList(actionGroup);
        }
        public void AddToList(ActionGroup actionGroup)
        {
            actionList.Add(actionGroup);
        }

        public void ReverseAll()
        {
            foreach (ActionGroup actionGroup in actionList)
            {
                reverseList.Add(actionGroup);
            }
            reverseAll = true;
        }

        public void Reverse(ActionGroup group)
        {
            reverseList.Add(group);
        }

        public void Remove(ActionGroup group)
        {
            eraseList.Add(group);
        }

        public void SetDTMod(float dtMod_)
        {
            dtMod = dtMod_;
        }

        public float GetDTMod()
        {
            return dtMod;
        }
    }
}