#if UNITY_EDITOR

using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightProbeVolumeComponent : MonoBehaviour
{
	public static LightProbeGroup lightProbeGroup;
	public float density = 1f;
	public MeshCollider volumeCached;

	public void CreateProbePoints()
	{
		if (lightProbeGroup == null)
		{
			if (GameObject.FindObjectOfType<LightProbeGroup>() != null)
			{
				lightProbeGroup = GameObject.FindObjectOfType<LightProbeGroup>();
			}
			else
			{
				GameObject go = new GameObject("Light Probes");
				lightProbeGroup = go.AddComponent<LightProbeGroup>();
			}
		}

		List<Vector3> probePositions = new List<Vector3>();

		LightProbeVolumeComponent[] allVolumes = GameObject.FindObjectsOfType<LightProbeVolumeComponent>();
		foreach (LightProbeVolumeComponent item in allVolumes)
		{
			for (float rows = 1; rows <= item.volumeCached.bounds.size.z / item.density; rows++)
			{
				for (float cols = 1; cols <= item.volumeCached.bounds.size.x / item.density; cols++)
				{
					for (float height = 1; height <= item.volumeCached.bounds.size.y / item.density; height++)
					{
						Vector3 point = new Vector3(cols - 0.5f, height - 0.5f, rows - 0.5f) * item.density;
						point -= item.volumeCached.bounds.extents;
						probePositions.Add(item.volumeCached.gameObject.transform.position + point);
					}
				}
			}
		}

		lightProbeGroup.probePositions = probePositions.ToArray();
	}
}

#endif