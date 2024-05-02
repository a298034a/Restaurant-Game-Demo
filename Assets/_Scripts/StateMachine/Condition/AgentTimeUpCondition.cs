namespace Restaurant 
{
    public class AgentTimeUpCondition : Condition, Condition.INeedAgent
    {
        private Agent _agent;

        public void SetAgent(Agent agent)
        {
            _agent = agent;
        }
        public override bool GetCondition()
        {
            return _agent.TimeUp();
        }
    }
}