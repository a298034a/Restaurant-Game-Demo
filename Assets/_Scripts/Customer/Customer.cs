using UnityEngine;

namespace Restaurant
{
    public class Customer : Agent
    {
        public Constant.CustomerIntention Intention => _intention;

        public Seat Seat => _seat;

        [Header("StateMachine")]
        [SerializeField]
        private StateMachine _stateMachine;

        [Header("Data")]
        [SerializeField]
        private int _money;

        private Constant.CustomerIntention _intention = Constant.CustomerIntention.None;
        private CustomerData _data;
        private int _direction;
        private Seat _seat;

        public void Init(CustomerData data)
        {
            _data = data;
            _money = _data.Money;
            CharacterAnimator.runtimeAnimatorController = _data.RuntimeAnimatorController;

            _stateMachine.Init(this);
        }
        public void SetIntention(Constant.CustomerIntention intention)
        {
            _intention = intention;
        }
        public void SetSeatDirection(int direction)
        {
            _direction = direction;
        }
        public void SetSeat(Seat seat)
        {
            _seat = seat;
        }
        public void OrderMeal()
        {
            OrderInfo orderInfo = new OrderInfo()
            {
                DishesName = GameStaticDatabase.DishesDataOverview.GetRandomDishesName(),
                Quantity = 1,
                Seat = _seat,
                State = OrderInfo.OrderState.NotReceived
            };

            BusinessManager.Ins.AddOrderInfo(orderInfo);

            SetOrderInfo(orderInfo);
        }
    }
}

