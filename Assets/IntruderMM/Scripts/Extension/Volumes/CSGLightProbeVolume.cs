#if UNITY_EDITOR || RUNTIME_CSG

using System.Collections;
using System.Collections.Generic;
using Sabresaurus.SabreCSG;
using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor.SabreCSGVolumes
{
	public class CSGLightProbeVolume : Volume
	{
		public float density = 1f;
		private GameObject volumeObj;
		private LightProbeVolumeComponent component;

		public override Material BrushPreviewMaterial
		{
			get
			{
				return AssetDatabase.LoadAssetAtPath<Material>("Assets/IntruderMM/Materials/Volume/mat_Volume_LightProbe.mat");
			}
		}

		public override bool OnInspectorGUI(Volume[] selectedVolumes)
		{
			bool invalidate = false;

			float oldDensity = 1f;
			density = EditorGUILayout.Slider("Light Probe Density", oldDensity = density, 0.5f, 2f);
			if (oldDensity != density)
			{
				invalidate = true;
			}

			if (GUILayout.Button("Generate Probes"))
			{
				foreach (LightProbeVolumeComponent item in GameObject.FindObjectsOfType<LightProbeVolumeComponent>())
				{
					item.CreateProbePoints();
				}

				//component.CreateProbePoints(volumeObj);
			}

			if (LightProbeVolumeComponent.lightProbeGroup == null)
			{
				EditorGUILayout.HelpBox("No generated probes!", MessageType.Error);
			}

			return invalidate;
		}

		public override void OnCreateVolume(GameObject volume)
		{
			volumeObj = volume;
			component = volumeObj.AddComponent<LightProbeVolumeComponent>();
			component.density = density;
			component.volumeCached = volume.GetComponent<MeshCollider>();
			Debug.Log("OnCreateVolume - Lightprobe Volume");
		}
	}
}
#endif

