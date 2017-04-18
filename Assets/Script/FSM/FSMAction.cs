using System.Collections;

namespace RisingTide.Fsm
{
    public class FSMAction
    {
        protected FSMState m_owner;

        public void SetOwner(FSMState owner)
        {
            m_owner = owner;
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
