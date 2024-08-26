using UnityEngine;
using System;

public class WindProxy : MonoBehaviour
{
	public Vector3 windMin = new Vector3(-.3f, 0.0f, -.3f);
	public Vector3 windMax = new Vector3(.3f, 0.0f, .3f);
	public float timerResetMin = 20.0f;
	public float timerResetMax = 60.0f;

	public Cloth[] flags;
}