using UnityEngine;

namespace Restaurant
{
    public class GetSeat : FSMAction
    {
        private Customer _customer;
        public override void PrePerform()
        {
            _customer = _agent as Customer;
            _customer.SetSeat(BusinessManager.Ins.GetSeat());
            _agent.SetTarget(_customer.Seat.Transform);
        }
        public override void PostPerform()
        {
            _agent.transform.position = _customer.Seat.Transform.position;
            switch (_customer.Seat.Direction)
            {
                case 0:
                    _agent.CharacterAnimator.Play(Constant.ANIMATE_SIT_DOWN);
                    break;
                case 1:
                    _agent.CharacterAnimator.Play(Constant.ANIMATE_SIT_LEFT);
                    break;
                case 2:
                    _agent.CharacterAnimator.Play(Constant.ANIMATE_SIT_UP);
                    break;
                case 3:
                    _agent.CharacterAnimator.Play(Constant.ANIMATE_SIT_RIGHT);
                    break;
            }
        }
    }
}

