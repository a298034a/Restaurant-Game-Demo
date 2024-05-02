using UnityEngine;

namespace Restaurant
{
    public class TakeOrder : FSMAction
    {
        private OrderInfo _orderInfo;
        private StageBuidUnitInfo _table;
        private bool _reachTable;
        public override void PrePerform()
        {
            _reachTable = false;
            _orderInfo = BusinessManager.Ins.GetOrderInfo(OrderInfo.OrderState.NotReceived);

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

            _agent.EmoteAnimator.Play(Constant.ANIMATE_EMOTE_NOTICE);
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
            _orderInfo.SetState(OrderInfo.OrderState.Received);
        }
    }
}

