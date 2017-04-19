using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RisingTide.Fsm;

public class TestFsm : MonoBehaviour {

	private FSM m_fsm;
	public FSM fsm {get{return m_fsm;}}
	// Use this for initialization
	void Start () {
		m_fsm = new FSM();
		FSMState state1 = new FSMState("log", m_fsm, true);
		LogAction action1 = new LogAction();
		action1.SetOwner(state1);
		state1.AddAction(action1);
		state1.AddTransition("calc", 2);

		FSMState state2 = new FSMState("calc", m_fsm);
		CalcAction action2 = new CalcAction();
		action2.SetOwner(state2);
		state2.AddAction(action2);
		state2.AddTransition("log", state1.id);

		m_fsm.AddTransition("g_calc", state2.id);
		m_fsm.AddTransition("g_log", state1.id);

		m_fsm.AddState(state1);
		m_fsm.AddState(state2);

		m_fsm.Initialize();
		m_fsm.EnableFSM();

		StartCoroutine(SendGlobalEvent());

	}

	IEnumerator SendGlobalEvent()
	{
		yield return new WaitForSeconds(3.0f);
		m_fsm.DispatchEvent("g_calc");
		yield return new WaitForSeconds(3.0f);
		m_fsm.GetCurrentState().DispatchEvent("log");
	}
}
