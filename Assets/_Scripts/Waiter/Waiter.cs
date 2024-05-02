using Restaurant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant 
{
    public class Waiter : Agent
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