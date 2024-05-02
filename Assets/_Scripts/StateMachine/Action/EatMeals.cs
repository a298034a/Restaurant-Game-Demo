using UnityEngine;

namespace Restaurant
{
    public class EatMeals : FSMAction
    {
        [SerializeField]
        private float _time = 3;
        public override void PrePerform()
        {
            _agent.EmoteAnimator.Play(Constant.ANIMATE_EMOTE_SATISFY);
            _agent.SetTimer(_time);
        }
        public override void PostPerform()
        {
            _agent.OrderInfo.DishesUnit.Consumed();
            _agent.OrderInfo.SetState(OrderInfo.OrderState.DiningCompleted);
        }
    }
}
