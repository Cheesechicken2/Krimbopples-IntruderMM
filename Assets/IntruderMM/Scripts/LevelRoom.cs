using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelRoom : MonoBehaviour
{
	public LayerMask layerMask;

	public List<Renderer> renderersInRoom = new List<Renderer>();

	public List<LevelRoom> roomsToEnable = new List<LevelRoom>();

	public List<LevelRoom> roomsToDisable = new List<LevelRoom>();

	public List<GameObject> excludedParents;

	public List<GameObject> includedParents;

	public bool turnAllOn;

	public HashSet<LevelRoom> visibleRooms = new HashSet<LevelRoom>();

	public LevelRoomVis levelRoomVis;

	public string folderPath = "Assets/RobStuff";

	public Renderer myRenderer;

	public void Awake()
	{
		if (TryGetComponent<Renderer>(out var component))
		{
			component.enabled = false;
		}
		if (TryGetComponent<Collider>(out var component2))
		{
			component2.isTrigger = false;
		}
		if (!roomsToEnable.Contains(this))
		{
			roomsToEnable.Add(this);
		}
	}

	[ContextMenu("Find stuff now")]
	public void FindRenderers()
	{
		LevelRoomManager levelRoomManager = Object.FindObjectOfType<LevelRoomManager>();
		if (levelRoomManager == null)
		{
			Debug.LogError("Couldn't find LevelRoomManager in FindRenderers, stopping");
			return;
		}
		layerMask = 1 << base.gameObject.layer;
		Renderer[] array = levelRoomManager.allRenderers.ToArray();
		renderersInRoom.Clear();
		Renderer[] array2 = array;
		foreach (Renderer renderer in array2)
		{
			if (renderer.enabled && IsChildOfIncludedParents(renderer) && !IsChildOfExcludedParents(renderer) && Physics.Raycast(renderer.bounds.center, Vector3.down, out var hitInfo, float.PositiveInfinity, layerMask) && hitInfo.collider.gameObject == base.gameObject)
			{
				renderersInRoom.Add(renderer);
				levelRoomManager.allRenderers.Remove(renderer);
				levelRoomManager.allRoomRenderers.Add(renderer);
			}
		}
	}

	public void AddToManager(LevelRoomManager lrm)
	{
		foreach (Renderer item in renderersInRoom)
		{
			lrm.allRoomRenderers.Add(item);
		}
	}

	[ContextMenu("Load room vis data")]
	public void LoadRoomVisData()
	{
		roomsToDisable.Clear();
		roomsToEnable.Clear();
		foreach (LevelRoom item in Enumerable.ToList(Object.FindObjectsOfType<LevelRoom>()))
		{
			bool flag = false;
			foreach (string visibleRoom in levelRoomVis.visibleRooms)
			{
				if (item.name == visibleRoom)
				{
					flag = true;
					if (!roomsToEnable.Contains(item))
					{
						roomsToEnable.Add(item);
					}
				}
			}
			if (!flag && !roomsToDisable.Contains(item))
			{
				roomsToDisable.Add(item);
			}
		}
	}

	private bool IsChildOfExcludedParents(Renderer renderer)
	{
		foreach (GameObject excludedParent in excludedParents)
		{
			if (renderer.transform.IsChildOf(excludedParent.transform))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsChildOfIncludedParents(Renderer renderer)
	{
		if (includedParents.Count == 0)
		{
			return true;
		}
		foreach (GameObject includedParent in includedParents)
		{
			if (renderer.transform.IsChildOf(includedParent.transform))
			{
				return true;
			}
		}
		return false;
	}
}
