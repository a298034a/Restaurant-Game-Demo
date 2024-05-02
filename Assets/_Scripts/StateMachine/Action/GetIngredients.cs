using UnityEngine;

namespace Restaurant
{
    public class GetIngredients : FSMAction
    {
        private StageBuidUnitInfo _closestBuidUnitInfo;

        private bool _reachRefrigerator = false;
        public override void PrePerform()
        {
            _reachRefrigerator = false;
            _agent.SetOrderInfo(BusinessManager.Ins.GetOrderInfo(OrderInfo.OrderState.Received));
            _agent.OrderInfo.SetState(OrderInfo.OrderState.BeingPrepared);

            StageBuidUnitInfo[] stageBuidUnitInfos = RestaurantInfoProvider.Ins.Info.GetRefrigerators();
            float closestDistance = float.MaxValue;
            foreach (StageBuidUnitInfo info in stageBuidUnitInfos)
            {
                float distance = Vector3.Distance(info.Unit.transform.position, _agent.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestBuidUnitInfo = info;
                }
            }

            Vector3 position = _closestBuidUnitInfo.Unit.transform.position;
            switch (_closestBuidUnitInfo.Unit.Direction)
            {
                case 0:
                    position += new Vector3(0, -1, 0);
                    break;
                case 1:
                    position += new Vector3(1, 0, 0);
                    break;
                case 2:
                    position += new Vector3(0, 1, 0);
                    break;
                case 3:
                    position += new Vector3(-1, 0, 0);
                    break;
            }

            _agent.SetTarget(position);
        }
        public override void OnUpdate()
        {
            if (_agent.ReachEndOfPath() && !_reachRefrigerator)
            {
                _reachRefrigerator = true;

                switch (_closestBuidUnitInfo.Unit.Direction)
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

                _agent.SetTimer(2);
            }
        }
    }
}