using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant 
{
    public class AgentOrderStateCondition : Condition, Condition.INeedAgent
    {
        [SerializeField]
        private OrderInfo.OrderState _orderState;

        private Agent _agent;
        public void SetAgent(Agent agent)
        {
            _agent = agent;
        }
        public override bool GetCondition()
        {
            if (_agent.OrderInfo == null) 
            { return false; }

            if (_agent.OrderInfo.State == _orderState)
            { return true; }

            return false;
        }
    }
}

