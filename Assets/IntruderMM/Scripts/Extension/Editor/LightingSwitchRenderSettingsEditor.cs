using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(LightingSwitchRenderSettingsData))]
public class LightingSwitchRenderSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightingSwitchRenderSettingsData settingsData = (LightingSwitchRenderSettingsData)target;

        // Draw the default inspector for LightingSwitchRenderSettingsData
        DrawDefaultInspector();

        // Add a space before the custom buttons
        EditorGUILayout.Space();

        // Add a button to set the current scene's RenderSettings to the ScriptableObject
        if (GUILayout.Button("Set Current Lighting"))
        {
            settingsData.Set();
            EditorUtility.SetDirty(settingsData); // Mark the ScriptableObject as dirty so changes are saved
        }

        // Add a button to apply the ScriptableObject's RenderSettings to the current scene
        if (GUILayout.Button("Apply Lighting"))
        {
            settingsData.Apply();
        }

        // Now, search for a LightningSwitchGroup component in the active scene or in the context
        LightingSwitchGroup group = FindObjectOfType<LightingSwitchGroup>();
        if (group != null)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Lightning Switch Group", EditorStyles.boldLabel);

            // Draw fields for LightningSwitchGroup
            group.groupName = EditorGUILayout.TextField("Group Name", group.groupName);
            group.skyboxReflection = (Cubemap)EditorGUILayout.ObjectField("Skybox Reflection", group.skyboxReflection, typeof(Cubemap), false);
            group.clockTimeOffsetRange = EditorGUILayout.Vector2Field("Clock Time Offset Range", group.clockTimeOffsetRange);
            group.disableBakedLightsOnStart = EditorGUILayout.Toggle("Disable Baked Lights On Start", group.disableBakedLightsOnStart);

            // Display list of baked lights
            SerializedProperty bakedLightsProp = serializedObject.FindProperty("bakedLights");
            EditorGUILayout.PropertyField(bakedLightsProp, true);

            // Display list of lightmaps
            SerializedProperty lightmapsProp = serializedObject.FindProperty("lightmaps");
            EditorGUILayout.PropertyField(lightmapsProp, true);

            // Display list of lightprobes
            SerializedProperty lightprobesProp = serializedObject.FindProperty("lightprobes");
            EditorGUILayout.PropertyField(lightprobesProp, true);

            // Display list of reflection maps
            SerializedProperty reflectionMapsProp = serializedObject.FindProperty("reflectionMaps");
            EditorGUILayout.PropertyField(reflectionMapsProp, true);

            // Apply modifications
            if (GUI.changed)
            {
                EditorUtility.SetDirty(group);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
