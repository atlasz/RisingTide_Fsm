using System.Collections.Generic;

namespace RisingTide.Fsm
{
    public class FSMState
    {
        protected int m_id;
		protected string m_name;
        protected bool m_bDefault;
        protected List<FSMAction> m_actions = new List<FSMAction>();
        protected Dictionary<string, int> m_transition = new Dictionary<string, int>();
        protected FSM m_owner;

        public FSMState(string name, FSM owner, bool bDefault = false)
        {
			m_name = name;
			m_id = name.GetHashCode();
            m_owner = owner;
            m_bDefault = bDefault;
        }

        public int id
        {
            get { return m_id; }
        }

		public string name
		{
			get{ return m_name; }
		}

        public bool IsDefault()
        {
            return m_bDefault; 
        }

        public FSM fsm
        {
            get { return m_owner; }
        }

        public void AddTransition(string evt, int stateId)
        {
            m_transition[evt] = stateId;
        }

        public void DispatchEvent(string evt)
        {
            if(m_transition.ContainsKey(evt))
            {
                m_owner.EnterState(m_transition[evt]);
            }
        }

        public void AddAction(FSMAction action)
        {
            m_actions.Add(action);
        }

        public virtual void OnAwake()
        {
            for (int i = 0; i < m_actions.Count; i++)
            {
                m_actions[i].OnAwake();
            }
        }

        public virtual void OnEnter()
        {
            for (int i = 0; i < m_actions.Count; i++)
            {
                m_actions[i].OnEnter();
            }
        }

        public virtual void OnUpdate()
        {
            for(int i = 0; i < m_actions.Count; i++)
            {
                m_actions[i].OnUpdate();
            }
        }

        public virtual void OnExit()
        {
            for (int i = 0; i < m_actions.Count; i++)
            {
                m_actions[i].OnExit();
            }
        }

        public virtual void OnStateComplete()
        {
            for (int i = 0; i < m_actions.Count; i++)
            {
                m_actions[i].OnStateComplete();
            }
        }
    }
}

