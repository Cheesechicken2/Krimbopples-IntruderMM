using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLevelRoomVis", menuName = "Create LevelRoomVis")]
public class LevelRoomVis : ScriptableObject
{
	public List<string> visibleRooms = new List<string>();
}
