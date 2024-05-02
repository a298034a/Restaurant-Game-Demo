namespace Restaurant
{
    public class WaitingToOrder : FSMAction
    {
        private Customer _customer;

        public override void PrePerform()
        {
            _customer = _agent as Customer;
            _customer.OrderMeal();

            _agent.EmoteAnimator.Play(Constant.ANIMATE_EMOTE_THINKING);
            _agent.SetTimer(50);
        }
    }
}

