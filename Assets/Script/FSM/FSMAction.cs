using System.Collections;

namespace RisingTide.Fsm
{
    public class FSMAction
    {
        protected FSMState m_owner;
		protected FSM m_fsm;
		public FSMState owner{get{return m_owner;}}
		public FSM fsm{get{return m_fsm;}}

        public void SetOwner(FSMState owner)
        {
            m_owner = owner;
			m_fsm = m_owner.fsm;
        }

        public virtual void OnAwake()
        {

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void OnStateComplete()
        {

        }
    }
}
