#if UNITY_EDITOR || RUNTIME_CSG

using System.Collections;
using System.Collections.Generic;
using Sabresaurus.SabreCSG;
using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor.SabreCSGVolumes
{
	public class CSGDeathzoneVolume : Volume
	{
		public override Material BrushPreviewMaterial
		{
			get
			{
				return AssetDatabase.LoadAssetAtPath<Material>("Assets/IntruderMM/Materials/Volume/mat_Volume_DeathZone.mat");
			}
		}

		public override void OnCreateVolume(GameObject volume)
		{
			volume.GetComponent<Collider>().isTrigger = true;
			ObjectTagger comp = volume.AddComponent<ObjectTagger>();
			comp.objectTag = "Deathzone";
		}
	}
}

#endif
