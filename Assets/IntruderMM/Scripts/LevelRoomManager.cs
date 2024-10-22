using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelRoomManager : MonoBehaviour
{
	public static LevelRoomManager main;

	public List<Renderer> allRenderers = new List<Renderer>();

	public List<LevelRoom> allRooms = new List<LevelRoom>();

	public List<Renderer> allRoomRenderers = new List<Renderer>();

	public List<LevelRoomFinder> finders = new List<LevelRoomFinder>();

	public List<LevelRoomFinder> activeFinders = new List<LevelRoomFinder>();

	public HashSet<LevelRoom> masterRoomsToEnable = new HashSet<LevelRoom>();

	public HashSet<LevelRoom> masterRoomsToDisable = new HashSet<LevelRoom>();

	public List<LevelRoom> debugEnables = new List<LevelRoom>();

	public List<LevelRoom> debugDisables = new List<LevelRoom>();

	public List<LevelRoom> currentRoomsWithFinders = new List<LevelRoom>();

	public void Awake()
	{
		main = this;
		allRenderers = Enumerable.ToList(Object.FindObjectsOfType<Renderer>());
		allRooms = Enumerable.ToList(Object.FindObjectsOfType<LevelRoom>());
	}

	public void AddFinder(LevelRoomFinder finder)
	{
		if (!activeFinders.Contains(finder))
		{
			activeFinders.Add(finder);
		}
		currentRoomsWithFinders.Add(finder.myRoom);
		currentRoomsWithFinders.Remove(finder.previousRoom);
		UpdateFindersState();
	}

	public void RemoveFinder(LevelRoomFinder finder)
	{
		if (activeFinders.Contains(finder))
		{
			activeFinders.Remove(finder);
		}
		currentRoomsWithFinders.Remove(finder.previousRoom);
		UpdateFindersState();
	}

	private void UpdateFindersState()
	{
		masterRoomsToEnable.Clear();
		masterRoomsToDisable.Clear();
		foreach (LevelRoom currentRoomsWithFinder in currentRoomsWithFinders)
		{
			AddRooms(currentRoomsWithFinder.roomsToEnable, currentRoomsWithFinder.roomsToDisable);
		}
		masterRoomsToDisable.ExceptWith(masterRoomsToEnable);
		debugEnables = Enumerable.ToList(masterRoomsToEnable);
		debugDisables = Enumerable.ToList(masterRoomsToDisable);
		SetRendererStates();
	}

	private void AddRooms(List<LevelRoom> roomsToEnable, List<LevelRoom> roomsToDisable)
	{
		masterRoomsToEnable.UnionWith(roomsToEnable);
		masterRoomsToDisable.UnionWith(roomsToDisable);
	}

	public void SetRendererStates()
	{
		foreach (LevelRoom currentRoomsWithFinder in currentRoomsWithFinders)
		{
			if (currentRoomsWithFinder.turnAllOn)
			{
				TurnOnAll();
				return;
			}
		}
		SetListState(masterRoomsToEnable, state: true);
		SetListState(masterRoomsToDisable, state: false);
	}

	public void SetListState(HashSet<LevelRoom> rooms, bool state)
	{
		foreach (LevelRoom room in rooms)
		{
			if (room == null)
			{
				continue;
			}
			if (!state)
			{
				Debug.Log(room.name + " is off ");
			}
			foreach (Renderer item in room.renderersInRoom)
			{
				if (!(item == null))
				{
					item.enabled = state;
				}
			}
		}
	}

	[ContextMenu("Turn on all")]
	public void TurnOnAll()
	{
		if (allRenderers == null || allRenderers.Count == 0)
		{
			Awake();
			foreach (LevelRoom allRoom in allRooms)
			{
				if (allRoom != null)
				{
					allRoom.AddToManager(this);
				}
			}
		}
		foreach (Renderer allRoomRenderer in allRoomRenderers)
		{
			if (allRoomRenderer != null)
			{
				allRoomRenderer.enabled = true;
			}
		}
	}

	[ContextMenu("Find room renderers")]
	public void FindInAll()
	{
		allRooms = Enumerable.ToList(Object.FindObjectsOfType<LevelRoom>());
		allRoomRenderers.Clear();
		allRenderers = Enumerable.ToList(Object.FindObjectsOfType<Renderer>());
		allRenderers.RemoveAll((Renderer x) => !x.enabled);
		allRenderers.RemoveAll((Renderer x) => x.GetComponent<IgnoreShadowFinder>() != null);
		allRooms.ForEach(delegate(LevelRoom x)
		{
			x.FindRenderers();
		});
	}

	[ContextMenu("Load all room vis datas")]
	public void LoadAllRoomVis()
	{
		allRooms.ForEach(delegate(LevelRoom x)
		{
			x.LoadRoomVisData();
		});
	}
}
