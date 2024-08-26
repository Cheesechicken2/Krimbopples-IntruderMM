using UnityEngine;
using System;


public class CustomDoorProxy : MonoBehaviour
{
	public GameObject doorHinge;
	public bool reverse = false;

	public bool alwaysLock = false;
	public bool neverLock = false;
	public bool canLockPick = true;
	public int maxDoorAngle = 150;
	[Range(0.0f, 1.0f)]
    public float startOpenPercent = 0.0f;

	public GameObject partnerDoor;

	public bool slidingDoor = false;
	public Vector3 slideDistance;
}