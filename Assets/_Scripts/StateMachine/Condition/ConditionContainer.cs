using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Restaurant
{
    [System.Serializable]
    public class ConditionContainer
    {
        [SerializeField]
        private bool _reverseResult;

        [SerializeReference]
        public List<Condition> Conditions = new List<Condition>();

        public void Init(Agent agent)
        {
            foreach (Condition.INeedAgent iNeedAgent in Conditions.OfType<Condition.INeedAgent>())
            {
                iNeedAgent.SetAgent(agent);
            }
        }
        public bool GetCondition()
        {
            return _reverseResult ^ _CheckProcess();
        }
        private bool _CheckProcess()
        {
            int count = Conditions.Count;

            if (count == 0)
            { return false; }

            for (int i = 0; i < count; i++)
            {
                if (!Conditions[i].GetCondition())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
