using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class PlaceDish : FSMAction
    {
        private bool _reachCounter = false;
        private Counter _counter;
        public override void PrePerform()
        {
            _reachCounter = false;
            Vector3 agentPos = _agent.transform.position;
            _counter = BusinessManager.Ins.GetClosestCounter(agentPos);

            Vector3 closestPos = Vector3.zero;
            float closestDistance = float.MaxValue;

            foreach (Vector3 target in _counter.Positions)
            {
                float distance = Vector3.Distance(agentPos, target);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPos = target;
                }
            }

            _agent.SetTarget(closestPos);
        }
        public override void OnUpdate()
        {
            if (_agent.ReachEndOfPath() && !_reachCounter)
            {
                _reachCounter = true;

                switch (_counter.StageBuidUnitInfo.Direction)
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

                _agent.OrderInfo.SetDishesUnit(BusinessManager.Ins.GetDishesUnit(_agent.OrderInfo.DishesName, _counter.PlacingPosition));
                _agent.OrderInfo.DishesUnit.SetCounter(_counter);
                _counter.Occupied = true;

                _agent.SetTimer(1);
            }
        }
        public override void PostPerform()
        {
            _agent.OrderInfo.SetState(OrderInfo.OrderState.PreparationCompleted);
        }
    }
}