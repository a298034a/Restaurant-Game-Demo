using UnityEngine;

namespace Restaurant
{
    public class PickUpMeals : FSMAction
    {
        private bool _reachMeal;
        private OrderInfo _orderInfo;

        public override void PrePerform()
        {
            _reachMeal = false;

            Vector3 agentPos = _agent.transform.position;
            _orderInfo = BusinessManager.Ins.GetOrderInfo(OrderInfo.OrderState.PreparationCompleted);
            _orderInfo.SetState(OrderInfo.OrderState.BeingDelivered);

            Vector3 closestPos = Vector3.zero;
            float closestDistance = float.MaxValue;

            foreach (Vector3 target in _orderInfo.DishesUnit.Counter.Positions)
            {
                float distance = Vector3.Distance(agentPos, target);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPos = target;
                }
            }

            _agent.EmoteAnimator.Play(Constant.ANIMATE_EMOTE_NOTICE);
            _agent.SetTarget(closestPos);
        }
        public override void OnUpdate()
        {
            if (_agent.ReachEndOfPath() && !_reachMeal)
            {
                _reachMeal = true;

                switch (_orderInfo.DishesUnit.Counter.StageBuidUnitInfo.Direction)
                {
                    case 0:
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_UP);
                        break;
                    case 1:
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_LEFT);
                        break;
                    case 2:
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_DOWN);
                        break;
                    case 3:
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_RIGHT);
                        break;
                }

                _orderInfo.DishesUnit.BeingServed();
                _agent.SetTimer(1);
            }
        }
    }
}


