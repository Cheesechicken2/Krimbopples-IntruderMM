using UnityEngine;
using UnityEngine.UI;

public class LevelRoomFinder : MonoBehaviour
{
	public LayerMask layerMask = 8388608;

	public LayerMask rendererCheckMask;

	public Text roomNameText;

	public LevelRoom myRoom;

	public LevelRoom previousRoom;

	private RaycastHit hit;

	public string roomName;

	public Transform myTransform;

	private Vector3 lastFindPos;

	public Camera cam;

	public LevelRoomManager levelRoomManager;

	private void Awake()
	{
		myTransform = base.transform;
	}

	private void Starter()
	{
		{
			base.enabled = false;
		}
	}

	private void OnEnabled()
	{
	}

	public void OnDisable()
	{
		if (!(LevelRoomManager.main == null))
		{
			RemoveRoom();
		}
	}

	public void RemoveRoom()
	{
		if (!(myRoom == null))
		{
			previousRoom = myRoom;
			myRoom = null;
			LevelRoomManager.main.RemoveFinder(this);
		}
	}

	public void Update()
	{
		if (LevelRoomManager.main == null)
		{
			return;
		}
		if (!cam.enabled)
		{
			RemoveRoom();
		}
		else if (Physics.Raycast(myTransform.position, Vector3.down, out hit, 400f, layerMask))
		{
			if (hit.transform.TryGetComponent<LevelRoom>(out var component))
			{
				if (component != myRoom)
				{
					previousRoom = myRoom;
					myRoom = component;
					LevelRoomManager.main.AddFinder(this);
				}
			}
			else
			{
				RemoveRoom();
			}
		}
		else
		{
			RemoveRoom();
		}
	}

	private void HighlightObjects()
	{
		foreach (Renderer item in myRoom.renderersInRoom)
		{
			Debug.DrawRay(item.transform.position, Vector3.up, Color.green, 2f);
		}
	}

	private void CheckVisibleRoomsFromPosition(Vector3 startPos)
	{
		if (!(myRoom != null))
		{
			return;
		}
		if (levelRoomManager == null)
		{
			if (LevelRoomManager.main == null)
			{
				levelRoomManager = Object.FindObjectOfType<LevelRoomManager>();
			}
			else
			{
				levelRoomManager = LevelRoomManager.main;
			}
		}
		foreach (LevelRoom allRoom in levelRoomManager.allRooms)
		{
			foreach (Renderer item in allRoom.renderersInRoom)
			{
				Vector3 direction = startPos - item.bounds.center;
				float maxDistance = Vector3.Distance(startPos, item.bounds.center);
				if (!Physics.Raycast(item.bounds.center, direction, out hit, maxDistance, rendererCheckMask))
				{
					AddToVisibleRooms(allRoom);
				}
			}
		}
	}

	private void AddToVisibleRooms(LevelRoom room)
	{
		if (!(room != null))
		{
			return;
		}
		if (myRoom.levelRoomVis != null)
		{
			if (!myRoom.levelRoomVis.visibleRooms.Contains(room.name))
			{
				myRoom.levelRoomVis.visibleRooms.Add(room.name);
			}
		}
		else
		{
			myRoom.visibleRooms.Add(room);
		}
	}
}
