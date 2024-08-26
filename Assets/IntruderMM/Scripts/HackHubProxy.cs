using UnityEngine;
using UnityEngine.Events;

public class HackHubProxy : MonoBehaviour
{
	public int hackGoal = 5;
	public HackNodeProxy[] nodes;
	public bool autoGrabNodesFromChildren = true;
	public bool autoNameChildNodes = true;
	public bool showHackCompleteTransmitWarning = true;
	public string baseNodeName = "A";
	public UnityEvent hackCompleteEvent;
	public UnityEvent roundResetEvent;
	public Activator hackCompleteActivator;

	void Awake()
	{
		if (autoGrabNodesFromChildren)
		{
			nodes = transform.GetComponentsInChildren<HackNodeProxy>();
		}

		if (autoNameChildNodes && nodes != null)
		{
			int i = 1;

			foreach (HackNodeProxy node in nodes)
			{
				node.nodeName = baseNodeName + i;
				i++;
			}
		}
	}
}
