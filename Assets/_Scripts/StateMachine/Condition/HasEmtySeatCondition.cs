namespace Restaurant
{
    public class HasEmtySeatCondition : Condition
    {
        public override bool GetCondition()
        {
            return BusinessManager.Ins.HasSeat();
        }
    }
}
