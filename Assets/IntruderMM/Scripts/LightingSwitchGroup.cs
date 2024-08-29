using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightingSwitchGroup : MonoBehaviour
{
    public string groupName;

    public Texture2D[] lightmaps;

    public SphericalHarmonicsL2[] lightprobes;

    public LightingSwitchRenderSettingsData renderSettings;

    public List<Texture> reflectionMaps = new List<Texture>();

    public Cubemap skyboxReflection;

    public Vector2 clockTimeOffsetRange;

    public bool disableBakedLightsOnStart = true;

    public List<Light> bakedLights = new List<Light>();

#if UNITY_EDITOR
	[MenuItem("Assets/Create/LightingSwitchRenderSettingsData")]
	public static void CreateMyAsset()
	{
		LightingSwitchRenderSettingsData asset = ScriptableObject.CreateInstance<LightingSwitchRenderSettingsData>();

		string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/LightingSwitchRenderSettingsData.asset");
		AssetDatabase.CreateAsset(asset, name);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
#endif

}
