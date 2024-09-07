#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateLightingSwitchRenderSettingsData : MonoBehaviour

{
    [MenuItem("Assets/Create/LightingSwitchRenderSettingsData")]
    public static void CreateMyAsset()
    {
        LightingSwitchRenderSettingsData asset = ScriptableObject.CreateInstance<LightingSwitchRenderSettingsData>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/LightingSwitchRenderSettingsData.asset");
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
#endif
