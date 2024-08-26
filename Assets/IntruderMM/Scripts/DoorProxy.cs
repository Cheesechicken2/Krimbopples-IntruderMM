using UnityEngine;
using System;


public class DoorProxy : MonoBehaviour
{
	public int doorIndex = 0;

	public bool alwaysLock = false;
	public bool neverLock = false;
	public bool canLockPick = true;
	public int maxDoorAngle = 150;

    [Range(0f, 1f)]
    public float startOpenPercent;

    public GameObject partnerDoor;
}