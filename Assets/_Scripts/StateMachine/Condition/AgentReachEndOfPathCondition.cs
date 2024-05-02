namespace Restaurant 
{
    public class AgentReachEndOfPathCondition : Condition, Condition.INeedAgent
    {
        private Agent _agent;

        public void SetAgent(Agent agent)
        {
            _agent = agent;
        }
        public override bool GetCondition()
        {
            return _agent.ReachEndOfPath();
        }
    }
}