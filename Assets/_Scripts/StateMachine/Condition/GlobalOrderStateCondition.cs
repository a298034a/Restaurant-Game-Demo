using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class GlobalOrderStateCondition : Condition
    {
        [SerializeField]
        private OrderInfo.OrderState _orderState;
        public override bool GetCondition()
        {
            if (!BusinessManager.Ins.HasOreder()) return false;

            OrderInfo orderInfo = BusinessManager.Ins.GetOrderInfo(_orderState);

            return orderInfo != null;
        }
    }
}
