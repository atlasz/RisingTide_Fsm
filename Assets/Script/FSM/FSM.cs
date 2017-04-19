using System.Collections.Generic;

namespace RisingTide.Fsm
{

    public class FSM
    {
        protected Dictionary<string, int> m_transition = new Dictionary<string, int>();
        private Dictionary<int, FSMState> m_dicStates = new Dictionary<int, FSMState>();
        private Dictionary<string, SharedVariable> m_dicBlackboard = new Dictionary<string, SharedVariable>();
        private FSMState m_curState;

        private bool m_bInit = false;
        private bool m_bPause = false;
        //private bool m_bActive = false;// if we have no manager, we don't need active

        public void Initialize()
        {
            m_bInit = true;
        }

        public void AddTransition(string evt, int stateId)
        {
            m_transition[evt] = stateId;
        }

        public void AddState(FSMState state)
        {
            m_dicStates[state.id] = state;
        }

        public FSMState GetCurrentState()
        {
            return m_curState;
        }

		//todo
		//temp function
		public Dictionary<int, FSMState> GetAllStates()
		{
			return m_dicStates;
		}

		public Dictionary<string, int> GetAllGlobalTransition()
		{
			return m_transition;
		}

        public void DispatchEvent(string evt)
        {
            if (m_transition.ContainsKey(evt))
            {
                EnterState(m_transition[evt]);
            }
        }

        public void EnableFSM()
        {
            //m_bActive = true;
            foreach(FSMState state in m_dicStates.Values)
            {
                state.OnAwake();
                if(state.IsDefault())
                {
                    m_curState = state;
                }
            }
            m_curState.OnEnter();
        }

        public void DisableFSM()
        {
			if(m_curState != null)
			{
				m_curState.OnExit();
			}
            //m_bActive = false;
            foreach (FSMState state in m_dicStates.Values)
            {
                state.OnStateComplete();
            }
        }

        public void Pause()
        {
            m_bPause = true;
        }

        public void Resume()
        {
            m_bPause = false;
        }

        public void EnterState(int id)
        {
            if(!m_bInit)
            {
                return;
            }

            if(id == m_curState.id)
            {
                return;
            }

            FSMState state;
            if(m_dicStates.TryGetValue(id, out state))
            {
                m_curState.OnExit();
                m_curState = state;
                m_curState.OnEnter();
            }
        }

        public void Tick()
        {
            if(m_bPause || !m_bInit)
            {
                return;
            }

            m_curState.OnUpdate();
        }

        public SharedVariable GetSharedValue(string key)
        {
            if(m_dicBlackboard.ContainsKey(key))
            {
                return m_dicBlackboard[key];
            }
            return null;
        }

        public void SetSharedValue(string key, SharedVariable val)
        {
            m_dicBlackboard[key] = val;
        }
    }
}

