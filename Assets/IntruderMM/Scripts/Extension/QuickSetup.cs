#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;
#endif
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Assets.IntruderMM.Editor
{
	public class QuickSetup
	{
#if UNITY_POST_PROCESSING_STACK_V2
		[MenuItem("Intruder/Utilities/Create Post Processing")]
		public static void CreatePostProcessing()
		{
			GameObject go = new GameObject("Post Processing Volume");
			PostProcessVolume postFxVol = go.AddComponent<PostProcessVolume>();
			PostProcessProfile profile = postFxVol.sharedProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
			go.layer = 1;

			GameObject observeCam;
			PostProcessLayer layer;

			if (GameObject.FindObjectOfType<ObserveCamProxy>() != null)
			{
				observeCam = GameObject.FindObjectOfType<ObserveCamProxy>().gameObject;
				if (observeCam.gameObject.GetComponent<PostProcessLayer>() == null) layer = observeCam.gameObject.AddComponent<PostProcessLayer>();
				else layer = observeCam.gameObject.GetComponent<PostProcessLayer>();
			}
			else
			{
				observeCam = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/Proxy/ObserveCamProxy.prefab", typeof(GameObject)));
				layer = observeCam.gameObject.AddComponent<PostProcessLayer>();
			}

			layer.volumeLayer = LayerMask.GetMask("TransparentFX");
			AmbientOcclusion ao = profile.AddSettings<AmbientOcclusion>();
			ColorGrading colorGrading = profile.AddSettings<ColorGrading>();
			Bloom bloom = profile.AddSettings<Bloom>();

			// Assign AO Stuff
			// AO mode
			AmbientOcclusionModeParameter aoMode = new AmbientOcclusionModeParameter();
			aoMode.Override(AmbientOcclusionMode.ScalableAmbientObscurance);
			aoMode.overrideState = true;
			ao.mode = aoMode;

			// AO Quality
			AmbientOcclusionQualityParameter aoQuality = new AmbientOcclusionQualityParameter();
			aoQuality.Override(AmbientOcclusionQuality.Medium);
			aoQuality.overrideState = true;
			ao.quality = aoQuality;

			// AO Ambient Only
			BoolParameter ambientOnly = new BoolParameter();
			ambientOnly.value = true;
			ambientOnly.overrideState = true;
			ao.ambientOnly = ambientOnly;

			// Intensity
			FloatParameter aoIntensity = new FloatParameter();
			aoIntensity.value = 0.15f;
			aoIntensity.overrideState = true;
			ao.intensity = aoIntensity;

			// Assign Tonemapper Stuff
			TonemapperParameter tonemapper = new TonemapperParameter();
			tonemapper.Override(Tonemapper.ACES);
			tonemapper.overrideState = true;
			colorGrading.tonemapper = tonemapper;

			// Assign Tonemapper Stuff
			FloatParameter exposure = new FloatParameter();
			exposure.value = 1.25f;
			exposure.overrideState = true;
			colorGrading.postExposure = exposure;

			// Bloom
			FloatParameter bloomIntensity = new FloatParameter();
			bloomIntensity.value = 1f;
			bloomIntensity.overrideState = true;
			bloom.intensity = bloomIntensity;

			postFxVol.priority = 100;
			postFxVol.isGlobal = true;

			EditorSceneManager.SaveOpenScenes();
			if (EditorUtility.DisplayDialog("Reload Scene?", "Reloads the scene to initialize post procesing", "Yes", "No"))
			{
				Scene openScene = EditorSceneManager.GetActiveScene();
				EditorSceneManager.OpenScene(openScene.path, OpenSceneMode.Single);
			}
		}
#endif

		[MenuItem("Intruder/Utilities/Map Setup")]
		public static void SetupBasics()
		{
			GameObject.CreatePrimitive(PrimitiveType.Plane);
			GameObject spawnA = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/SpawnA.prefab", typeof(GameObject)));
			spawnA.transform.position = new Vector3(-2, 1, 0);
			GameObject spawnB = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/SpawnB.prefab", typeof(GameObject)));
			spawnB.transform.position = new Vector3(2, 1, 0);
		}
	}
}

#endif