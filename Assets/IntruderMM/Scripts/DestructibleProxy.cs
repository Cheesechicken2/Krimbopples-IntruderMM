using UnityEngine;
using System;


public class DestructibleProxy : MonoBehaviour
{
	public int life = 4;
	public GameObject particleEffect;
	public GameObject destroyParticleEffect;

	public Vector2 pitch;

	public GameObject[] childrenToDestroy;
}