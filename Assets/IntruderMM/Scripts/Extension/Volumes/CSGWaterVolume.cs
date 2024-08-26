#if UNITY_EDITOR || RUNTIME_CSG

using System.Collections;
using System.Collections.Generic;
using Sabresaurus.SabreCSG;
using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor.SabreCSGVolumes
{
	public class CSGWaterVolume : Volume
	{
		public override Material BrushPreviewMaterial
		{
			get
			{
				return AssetDatabase.LoadAssetAtPath<Material>("Assets/IntruderMM/Materials/Volume/mat_Volume_Water.mat");
			}
		}

		public override void OnCreateVolume(GameObject volume)
		{
			volume.GetComponent<Collider>().isTrigger = false;
			BoxCollider boxC = volume.AddComponent<BoxCollider>();
			MeshCollider meshC = volume.GetComponent<MeshCollider>();
			boxC.size = meshC.bounds.extents * 2;
			volume.AddComponent<WaterVolume>(); // USE THIS once the game is updated
			DestroyImmediate(meshC);
		}
	}
}

#endif