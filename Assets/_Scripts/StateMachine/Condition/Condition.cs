namespace Restaurant
{
    [System.Serializable]
    public abstract class Condition
    {
        public virtual bool GetCondition()
        {
            return true;
        }

        public interface INeedAgent
        {
            public void SetAgent(Agent agent);
        }
    }
}