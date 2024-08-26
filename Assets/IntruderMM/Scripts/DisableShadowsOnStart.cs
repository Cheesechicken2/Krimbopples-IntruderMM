using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShadowsOnStart : MonoBehaviour
{
	// Use this for initialization
	public bool getChildren = true;

	void Start()
	{
		if (getChildren)
		{
			Renderer[] renderers = GetComponentsInChildren<Renderer>();

			foreach (var rend in renderers)
			{
				rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			}
		}
		else
		{
			Renderer rend = GetComponent<Renderer>();

			if (rend != null)
			{
				rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			}
		}
	}
}
