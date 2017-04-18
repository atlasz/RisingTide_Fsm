using UnityEngine;
using System.Collections;

namespace RisingTide.Fsm
{
    public class SharedVariable
    {
        public virtual void SetValue(object val)
        {

        }


        public virtual object GeteValue()
        {
            return null;
        }

    }

    public class SharedBool: SharedVariable
    {
        private bool m_value;
        public bool Value { get { return m_value; } set { m_value = value; } }

        public override void SetValue(object val)
        {
            m_value = (bool)val;
        }

        public override object GeteValue()
        {
            return m_value;
        }
    }

    public class SharedFloat : SharedVariable
    {
        private float m_value;
        public float Value { get { return m_value; } set { m_value = value; } }

        public override void SetValue(object val)
        {
            m_value = (float)val;
        }

        public override object GeteValue()
        {
            return m_value;
        }
    }

    public class SharedInt : SharedVariable
    {
        private int m_value;
        public int Value { get { return m_value; } set { m_value = value; } }

        public override void SetValue(object val)
        {
            m_value = (int)val;
        }

        public override object GeteValue()
        {
            return m_value;
        }
    }

    public class SharedGameObject : SharedVariable
    {
        private GameObject m_value;
        public GameObject Value { get { return m_value; } set { m_value = value; } }

        public override void SetValue(object val)
        {
            m_value = (GameObject)val;
        }

        public override object GeteValue()
        {
            return m_value;
        }
    }
}

