using UnityEngine;
using System;

public class HideOnStart : MonoBehaviour
{
	public void Start()
	{
		if (GetComponent<Renderer>() != null)
			GetComponent<Renderer>().enabled = false;
	}
}