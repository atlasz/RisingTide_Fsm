using UnityEngine;
using System.Collections;

namespace RisingTide.Fsm
{
    public class LogAction : FSMAction
    {

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("OnEnter: " + this.GetType().ToString());
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.Log("OnUpdate: " + this.GetType().ToString());
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("OnExit: " + this.GetType().ToString());
        }
    }

    public class CalcAction: FSMAction
    {
        private float m_startTime = 0;

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("OnEnter: " + this.GetType().ToString());
            m_startTime = 0;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Debug.Log("OnUpdate: " + this.GetType().ToString());
            Debug.Log("Calcate 1 + 1 =  2");
            m_startTime += Time.deltaTime;
            if (m_startTime > 2f)
            {
                m_owner.DispatchEvent("log");
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("OnExit: " + this.GetType().ToString());
        }
    }
}

