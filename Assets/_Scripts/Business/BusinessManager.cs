using UnityEngine;

namespace Restaurant
{
    public class BusinessManager
    {
        private BusinessSystem _system;
        private BusinessController _controller;

        public void StartBusiness()
        {
            _system.CalculateCounters();
            _system.CalculateSeats();
            _system.DemoStartBusiness();
        }
        public bool HasSeat()
        {
            return _system.HasSeat;
        }
        public Seat GetSeat()
        {
            return _system.GetSeat();
        }
        public void AddSeat(Seat seat)
        {
            _system.AddSeat(seat);
        }
        public bool HasOreder()
        {
            return _system.HasOreder;
        }
        public OrderInfo GetOrderInfo(OrderInfo.OrderState orderState)
        {
            return _system.GetOrderInfo(orderState);
        }
        public void AddOrderInfo(OrderInfo orderInfo)
        {
            _system.AddOrderInfo(orderInfo);
        }
        public Transform GetEntrance()
        {
            return _system.Entrance;
        }
        public Transform GetChefWaitingArea()
        {
            return _system.ChefWaitingArea;
        }
        public Transform GetWaiterWaitingArea()
        {
            return _system.WaiterWaitingArea;
        }
        public Counter GetClosestCounter(Vector3 selfPos)
        {
            return _system.GetClosestCounter(selfPos);
        }
        public DishesUnit GetDishesUnit(string name, Vector3 position)
        {
            return _system.InstantiateDishes(name, position);
        }

        #region Singleton
        private static BusinessManager _instance;
        public BusinessManager()
        {
            _system = GameObject.Find("BusinessSystem").GetComponent<BusinessSystem>();
            DishesUnitSpawner dishesUnitSpawner = new DishesUnitSpawner();
            CustomerSpawner customerSpawner = new CustomerSpawner();
            _system.Init(dishesUnitSpawner, customerSpawner);

            _controller = GameObject.Find("BusinessController").GetComponent<BusinessController>();
        }
        public static BusinessManager Ins
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BusinessManager();
                }
                return _instance;
            }
        }
        #endregion
    }

}
