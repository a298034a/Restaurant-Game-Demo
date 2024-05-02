using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class ClearTable : FSMAction
    {
        private OrderInfo _orderInfo;
        private bool _reachTable;
        public override void PrePerform()
        {
            _reachTable = false;
            _orderInfo = BusinessManager.Ins.GetOrderInfo(OrderInfo.OrderState.DiningCompleted);

            Vector3Int closestPosition = Vector3Int.zero;
            float closestDistance = float.MaxValue;
            foreach (var position in _orderInfo.Seat.TableUnitInfo.Positions)
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

                Vector3 tablePos = _orderInfo.Seat.TableUnitInfo.Unit.transform.position;
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
            _orderInfo.SetState(OrderInfo.OrderState.TableClearingCompleted);
            BusinessManager.Ins.AddSeat(_orderInfo.Seat);
            Destroy(_orderInfo.DishesUnit.gameObject);
        }
    }
}