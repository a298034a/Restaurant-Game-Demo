using UnityEngine;

namespace Restaurant
{
    public class Idle : FSMAction
    {
        [SerializeField]
        private WaitingArea _waitingArea = WaitingArea.None;

        private bool _reachWaitingArea;
        public override void PrePerform()
        {
            _reachWaitingArea = false;

            switch (_waitingArea)
            {
                case WaitingArea.None:
                    _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_DOWN);
                    break;

                case WaitingArea.Chef:
                    _agent.SetTarget(BusinessManager.Ins.GetChefWaitingArea());
                    break;

                case WaitingArea.Waiter:
                    _agent.SetTarget(BusinessManager.Ins.GetWaiterWaitingArea());
                    break;
            }
        }
        public override void OnUpdate()
        {
            if (_waitingArea != WaitingArea.None && _agent.ReachEndOfPath() && !_reachWaitingArea)
            {
                _reachWaitingArea = true;
                _agent.CharacterAnimator.Play(Constant.ANIMATE_IDLE_DOWN);
            }
        }
        enum WaitingArea
        {
            None,
            Chef,
            Waiter
        }
    }
}
