using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Restaurant
{
    public class StateTransitioner : MonoBehaviour
    {
        public State CurrentState => _currentState;

        private State _currentState;

        [SerializeField]
        private bool _debug;

        [SerializeField]
        private string _firstState;

        [Searchable]
        [SerializeField]
        private List<State> _states = new List<State>();

        private Agent _agent;
        public void Init(Agent agent)
        {
            State firstState = null;

            foreach (State state in _states)
            {
                state.Init(agent);

                if (string.Equals(state.Name, _firstState))
                {
                    firstState = state;
                }
            }

            _agent = agent;
            _currentState = firstState;
            _currentState.Action.PrePerform();
        }
        public void TryTransition()
        {
            foreach (Transition transitions in _currentState.Transitions)
            {
                if (transitions.ExpectedResult())
                {
                    TrasitionState(transitions.Name);
                    break;
                }
            }
        }
        public void TrasitionState(string stateName)
        {
            if (_debug) Debug.Log(stateName);
            State state = _states.Find(x => x.Name == stateName);

            if (state != null)
            { TrasitionState(state); }
        }
        public void TrasitionState(State state)
        {
            _agent.ResetOnStateChenge();
            _currentState.Action.PostPerform();
            _currentState = state;
            _currentState.Action.PrePerform();
        }

        [Serializable]
        public class State
        {
            public string Name;
            public FSMAction Action;
            public List<Transition> Transitions = new List<Transition>();

            public void Init(Agent agent)
            {
                foreach (Transition transition in Transitions)
                {
                    transition.Init(agent);
                }
            }
        }
        [Serializable]
        public class Transition
        {
            public string Name;

            public ConditionContainer ConditionContainer = new ConditionContainer();
            public void Init(Agent agent)
            {
                ConditionContainer.Init(agent);
            }
            public bool ExpectedResult()
            {
                return ConditionContainer.GetCondition();
            }
        }
    }
}