namespace Restaurant
{
    [System.Serializable]
    public class OrderInfo
    {
        public string DishesName;
        public int Quantity;
        public Seat Seat;
        public OrderState State = OrderState.None;
        public DishesUnit DishesUnit;

        public void SetState(OrderState state)
        {
            State = state;
        }
        public void SetDishesUnit(DishesUnit dishesUnit)
        {
            DishesUnit = dishesUnit;
        }
        public enum OrderState
        {
            None,//生成時的預設狀態
            NotReceived,//未接單
            Received,//已接單
            BeingPrepared,//正在準備食物
            PreparationCompleted,//食物準備完成
            BeingDelivered,//正在送餐
            Delivered,//餐點已送達
            DiningCompleted,//用餐完畢
            TableClearingCompleted//收桌完畢
        }
    }
}