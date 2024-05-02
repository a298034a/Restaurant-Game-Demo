using UnityEngine;

namespace Restaurant
{
    public class ServeMeals : FSMAction
    {
        private StageBuidUnitInfo _table;
        private bool _reachTable;
        private OrderInfo _orderInfo;
        public override void PrePerform()
        {
            _reachTable = false;
            _orderInfo = BusinessManager.Ins.GetOrderInfo(OrderInfo.OrderState.BeingDelivered);            

            _table = _orderInfo.Seat.TableUnitInfo;
            Vector3Int closestPosition = Vector3Int.zero;
            float closestDistance = float.MaxValue;
            foreach (var position in _table.Positions)
            {
                float distance = Vector3.Distance(position, _agent.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPosition = position;
                }
            }

            _agent.SetTarget(closestPosition);
        }
        public override void OnUpdate()
        {
            if (_agent.ReachEndOfPath() && !_reachTable)
            {
                _reachTable = true;

                Vector3 tablePos = _table.Unit.transform.position;
                Vector3 agentPos = this.transform.position;

                bool largestDistanceIsX = (Mathf.Abs(agentPos.x - tablePos.x) > Mathf.Abs(agentPos.y - tablePos.y));

                if (largestDistanceIsX)
                {
                    if (agentPos.x > tablePos.x)
                    {
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_LEFT);
                    }
                    else
                    {
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_RIGHT);
                    }
                }
                else
                {
                    if (agentPos.y > tablePos.y)
                    {
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_DOWN);
                    }
                    else
                    {
                        _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_UP);
                    }
                }

                _agent.SetTimer(1);
            }
        }
        public override void PostPerform()
        {
            Vector3 mealPos = Vector3.zero;

            switch (_orderInfo.Seat.Direction)
            {
                case 0:
                    mealPos = _orderInfo.Seat.Transform.position + new Vector3(0, -1, 0);
                    break;
                case 1:
                    mealPos = _orderInfo.Seat.Transform.position + new Vector3(-1, 0, 0);
                    break;
                case 2:
                    mealPos = _orderInfo.Seat.Transform.position + new Vector3(0, 1, 0);
                    break;
                case 3:
                    mealPos = _orderInfo.Seat.Transform.position + new Vector3(1, 0, 0);
                    break;
            }

            _orderInfo.DishesUnit.SetPostion(mealPos);
            _orderInfo.DishesUnit.Delivered();
            _orderInfo.SetState(OrderInfo.OrderState.Delivered);
        }
    }
}


