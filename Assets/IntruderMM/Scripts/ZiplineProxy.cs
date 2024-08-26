using UnityEngine;
using System;

public class ZiplineProxy : MonoBehaviour
{
	public GameObject startPoint;
	public GameObject endPoint;

	public float zipSpeed = .3f;

	public int numberOfVertices = 2;
	public float maxGravityDangle = 0.0f;
    public float forceDismountAtPercent = 0.99f;
}