using UnityEngine;

namespace Restaurant
{
    public class Leave : FSMAction
    {
        public override void PrePerform()
        {
            _agent.SetTarget(BusinessManager.Ins.GetEntrance());
        }
        public override void PostPerform()
        {

        }
        public override void OnUpdate()
        {
            if (_agent.ReachEndOfPath())
            {
                Destroy(_agent.gameObject);
            }
        }
    }
}