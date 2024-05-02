using UnityEngine;

namespace Restaurant
{
    public class FSMAction: MonoBehaviour
    {
        protected Agent _agent;
        public void Awake() 
        {
            _agent = gameObject.transform.root.GetComponent<Agent>();
        }
        public virtual void PrePerform() { }
        public virtual void PostPerform() { }
        public virtual void OnUpdate() { }      
    }
}

