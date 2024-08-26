using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ActivatorTeam
{
	Both = 0,
	Guards = 1,
	Intruders = 2
}

public enum GoalWinner
{
	Guards = 1,
	Intruders = 2,
	Draw = 0
}

public class Activator : MonoBehaviour
{
	public bool triggerByUse = false;
	public bool triggerByShoot = false;
	public bool triggerByExplosion = false;
	public float explosionTriggerDistance = 2.0f;
	public bool explosionMustBeDirectHit = false;
	public int hp = -1;

	public bool triggerByEnter = false;
	public bool triggerByRagdollEnter = false;

	public UnityEvent activateEvent;
	public UnityEvent resetEvent;

	public GameObject[] objectsToAnimate;
	public GameObject[] objectsToStop;
	public string[] animationNames;

	public GameObject[] objectsToEnable;
	public GameObject[] objectsToDisable;
	public GameObject[] randomObjectsToEnable;
    public string randomObjectsVariableName = "{randomPlace}";
    public List<string> randomObjectsNames = new List<string>();
    public bool autoPopulateRandomNames;

    public DoorProxy[] doorsToUnlock;
	public DoorProxy[] doorsToLock;
	public CustomDoorProxy[] customDoorsToUnlock;
	public CustomDoorProxy[] customDoorsToLock;

	public float delayTime = 0.0f;

	public bool enabledByDefault = true;
	public bool canRedo = false;
	public float redoTime = 1.0f;

	public float enabledAfterTime = 0.0f;
	public float activateAfterTime = 0.0f; //Automatically activate after round reset after this time

	public ActivatorTeam activatorTeam;

	public bool goal = false;
	public string goalMessage = "Guards Win!";
	public GoalWinner goalWinner;

	public string localMessage = "";
	public string allMessage = "";

	public Transform[] possibleTeleportDestinations;

	public ItemProxy[] neededItems;
	public string dontHaveAllItemsMessage;
	public bool useUpItems = false;
	public float resetDelay = -1.0f;
	public float allowActivateDelay = 0.0f;
	public bool resetActivateDelayOnEnable = true;
    public bool disableMeInThisMode;
    private bool _loadedMapObjectActiveState;
    private bool _loadedMapObjectActiveInHierarchyState;
    private bool _ready;
    public bool localOnly;
	public bool DebugMe;

	public void Activate() { }
}
