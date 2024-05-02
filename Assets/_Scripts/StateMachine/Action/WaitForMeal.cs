using UnityEngine;

namespace Restaurant
{
    public class WaitForMeal : FSMAction
    {
        [SerializeField]
        private float _waitingThreshold = 10f;
        public override void PrePerform()
        {
            _agent.SetTimer(_waitingThreshold);
        }
        public override void PostPerform()
        {

        }
    }
}