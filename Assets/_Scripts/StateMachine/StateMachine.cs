using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class StateMachine : MonoBehaviour
    {      
        [SerializeField]
        private StateTransitioner _transitioner;

        public void Init(Agent agent)
        {
            _transitioner.Init(agent);
        }
        private void Update()
        {
            if (_transitioner.CurrentState == null) return;

            _transitioner.TryTransition();
            _transitioner.CurrentState?.Action.OnUpdate();
        }        
    }
}

