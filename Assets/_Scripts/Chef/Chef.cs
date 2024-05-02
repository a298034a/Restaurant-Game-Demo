using UnityEngine;

namespace Restaurant
{
    public class Chef : Agent
    {
        [Header("StateMachine")]
        [SerializeField]
        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine.Init(this);
        }
    }
}