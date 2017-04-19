using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using RisingTide.Fsm;

public class FsmNodeInfo
{
	public int id;
	public string name;
	public Rect rect;
}

public class FsmTransitionInfo
{
	public int srcId;
	public int dstId;
	public string name;
}

public class FsmDebugWindow: EditorWindow
{
	private List<FsmNodeInfo> m_lstNodes = new List<FsmNodeInfo>();
	private List<FsmTransitionInfo> m_lstTransition = new List<FsmTransitionInfo>();
	private FSM m_fsm;

	[MenuItem("Window/Node editor")]
	static void ShowEditor() {
		FsmDebugWindow editor = EditorWindow.GetWindow<FsmDebugWindow>();
		editor.Init();
	}

	public void Init() {
		
	}

	void OnGUI() {
		GetCurrentFsm();
		Vector2 size = new Vector2(100, 100);
		Vector2 origin = new Vector2(10, 10);
		float delta = 0;
		Color color = GUI.color;
		if(m_fsm != null)
		{
			Dictionary<int, FSMState> states = m_fsm.GetAllStates();
			Dictionary<string, int> transitions = m_fsm.GetAllGlobalTransition();
			m_lstNodes.Clear();
			m_lstTransition.Clear();
			foreach(FSMState state in states.Values)
			{
				FsmNodeInfo info = new FsmNodeInfo();
				info.id = state.id;
				info.name = state.name;
				info.rect = new Rect(origin.x + delta, origin.y, size.x, size.y);
				m_lstNodes.Add(info);
				delta += 200;
			}

			foreach(KeyValuePair<string, int> transition in transitions)
			{
				FsmTransitionInfo tran = new FsmTransitionInfo();
				tran.name = transition.Key;
				tran.dstId = transition.Value;
				tran.srcId = -1;
				m_lstTransition.Add(tran);
			}

			FsmNodeInfo globalEvent = new FsmNodeInfo();
			globalEvent.id = -1;
			globalEvent.name = "global";
			globalEvent.rect = new Rect(origin.x, origin.y + 200, size.x, 50);

			BeginWindows();
			for(int idxNode = 0; idxNode < m_lstNodes.Count; ++idxNode)
			{
				FsmNodeInfo node = m_lstNodes[idxNode];
				if(node.id == m_fsm.GetCurrentState().id)
				{
					GUI.color = Color.green;
				}
				else
				{
					GUI.color = color;
				}
				//Debug.LogWarning	("nodeId:" + node.id + " currentId: " + m_fsm.GetCurrentState().id);
				node.rect = GUI.Window(node.id, node.rect, DrawNodeWindow, node.name);
				for(int idxTran = 0; idxTran < m_lstTransition.Count; ++idxTran)
				{
					FsmTransitionInfo tran = m_lstTransition[idxTran];
					if(node.id == tran.dstId)
					{
						GUI.color = Color.red;
						string name = string.Format("{0} to {1}\n <{2}>", globalEvent.name, node.name, tran.name);
						DrawNodeCurve(globalEvent.rect, node.rect, name);
					}
				}
				FSMState state = states[node.id];
				Dictionary<string, int> trans = state.GetTransitions();
				foreach(KeyValuePair<string, int> localTran in trans)
				{
					int index = m_lstNodes.FindIndex(x=>x.id == localTran.Value);
					if(index != -1)
					{
						GUI.color = color;
						string name = string.Format("{0} to {1}\n <{2}>", node.name, m_lstNodes[index].name, localTran.Key);
						DrawNodeCurve(node.rect, m_lstNodes[index].rect, name);
					}
				}
			}

			GUI.color = Color.red;
			globalEvent.rect = GUI.Window(globalEvent.id, globalEvent.rect, DrawNodeWindow, globalEvent.name);

			EndWindows();
		}


//		DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows
//
//		BeginWindows();
//		Color color = GUI.color;
//		GUI.color = Color.green;
//		window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1");   // Updates the Rect's when these are dragged
//		GUI.color = color;
//		window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
//		EndWindows();

	}

	void Update()
	{
		if(EditorApplication.isPlaying && !EditorApplication.isPaused)
		{
			Repaint();
		}
	}
//	if (EditorApplication.isPlaying && !EditorApplication.isPaused)
//	{
//		RecordImages();
//		Repaint();
//	}

	private void GetCurrentFsm()
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		m_fsm = go.GetComponent<TestFsm>().fsm;
	}

	void DrawNodeWindow(int id) {
		//GUI.Button(new Rect(10, 20, 100, 20), "Can't drag me");
		GUI.DragWindow();
	}

	void DrawNodeCurve(Rect start, Rect end) {
		Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 50;
		Vector3 endTan = endPos + Vector3.left * 50;
		Color shadowCol = new Color(0, 0, 0, 0.06f);
		for (int i = 0; i < 3; i++) // Draw a shadow
			Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
	}

	void DrawNodeCurve(Rect start, Rect end, string name) {
		Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 50;
		Vector3 endTan = endPos + Vector3.left * 50;
		Color shadowCol = new Color(0, 0, 0, 0.06f);
		for (int i = 0; i < 3; i++) // Draw a shadow
			Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
		GUI.Label(new Rect(0.5f * (startPos.x + endPos.x), 0.5f * (startPos.y + endPos.y), 300, 30), name);
	}
}