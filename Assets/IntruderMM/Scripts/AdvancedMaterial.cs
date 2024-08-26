using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedMaterial : MonoBehaviour
{
	// Use this for initialization
	public bool destroyBulletOnHit = true;
	public float penetrationMultiplier = 1;
	public bool spawnSmokePuff = true;
	public bool spawnSparkPuff;
	public bool shouldChunk;
	public bool transferForce;
	public float transferForceAmount = .005f;
	public bool noDecals;
	public bool allowColliderSetting;
}
