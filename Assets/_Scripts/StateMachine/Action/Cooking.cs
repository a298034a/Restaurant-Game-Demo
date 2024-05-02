using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class Cooking : FSMAction
    {
        private DishesData _dishesData;
        private StageBuidUnitInfo _closestBuidUnitInfo;

        private bool _reachCookTop = false;
        public override void PrePerform()
        {
            _reachCookTop = false;
            _dishesData = GameStaticDatabase.DishesDataOverview.GetData(_agent.OrderInfo.DishesName);

            StageBuidUnitInfo[] stageBuidUnitInfos = RestaurantInfoProvider.Ins.Info.GetCookTops();
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
            if (_agent.ReachEndOfPath() && !_reachCookTop)
            {
                _reachCookTop = true;

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

                _agent.SetTimer(_dishesData.CookingTime);
            }
        }
        public override void PostPerform()
        {
            _agent.EmoteAnimator.Play(Constant.ANIMATE_EMOTE_HAPPY);
        }
    }
}

