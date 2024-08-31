using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightingSwitchGroupCreator : MonoBehaviour

{
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
