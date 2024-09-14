using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

[CustomEditor(typeof(LightingSwitchGroup))]
public class LightingSwitchGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LightingSwitchGroup data = (LightingSwitchGroup)target;

        // Draw the default inspector for LightingSwitchGroup
        DrawDefaultInspector();

        // Add a space before the custom buttons
        EditorGUILayout.Space();

        // Add a button to get the baked lightmaps in the scene
        if (GUILayout.Button("Get Lightmaps"))
        {
            if (data.name == "" || data.renderSettings == null)
            {
                Debug.Log("You need to setup the GroupName and the RenderSettings before you can get the Lightmaps");
            }
            else
            {
                data.lightmaps = new Texture2D[LightmapSettings.lightmaps.Length * 3];

                string _parentFolderPath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(data.renderSettings));
                string _folderName = data.name + "-Lightmaps";
                string folderPath = _parentFolderPath + "/" + _folderName;

                if (AssetDatabase.IsValidFolder(folderPath)) AssetDatabase.DeleteAsset(folderPath);
                AssetDatabase.CreateFolder(_parentFolderPath, _folderName);


                for (int i = 0; i < LightmapSettings.lightmaps.Length; i++)
                {
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(LightmapSettings.lightmaps[i].lightmapDir), folderPath + "/Lightmap-" + i.ToString() + "_comp_dir.png");
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(LightmapSettings.lightmaps[i].lightmapColor), folderPath + "/Lightmap-" + i.ToString() + "_comp_light.exr");

                    Texture2D lightmapDir = AssetDatabase.LoadAssetAtPath<Texture2D>(folderPath + "/Lightmap-" + i.ToString() + "_comp_dir.png");
                    Texture2D lightmapColor = AssetDatabase.LoadAssetAtPath<Texture2D>(folderPath + "/Lightmap-" + i.ToString() + "_comp_light.exr");

                    data.lightmaps[i * 3] = lightmapDir;
                    data.lightmaps[i * 3 + 1] = lightmapColor;
                    data.lightmaps[i * 3 + 2] = null;
                }
            }
        }

        // Add a button to get the baked lightprobes in the scene
        if (GUILayout.Button("Get Lightprobes"))
        {
            data.lightprobes = new SphericalHarmonicsL2[LightmapSettings.lightProbes.bakedProbes.Length];

            for (int i = 0; i < LightmapSettings.lightProbes.bakedProbes.Length; i++)
            {
                data.lightprobes[i] = LightmapSettings.lightProbes.bakedProbes[i];
            }
        }

        // Add a button to enable this light mode
        if (GUILayout.Button("Enable this light mode"))
        {
            // Set active only this object
            data.gameObject.SetActive(true);
            for (int i = 0; i < data.transform.parent.childCount; i++)
            {
                Transform child = data.transform.parent.GetChild(i);
                if (child != data.transform) child.gameObject.SetActive(false);
            }

            // Set the correct lightmaps
            LightmapData[] lightmaparray = new LightmapData[data.lightmaps.Length / 3];
            for (var i = 0; i < data.lightmaps.Length / 3; i++)
            {
                LightmapData mapdata = new LightmapData();

                mapdata.lightmapDir = data.lightmaps[i * 3];
                mapdata.lightmapColor = data.lightmaps[i * 3 + 1];

                lightmaparray[i] = mapdata;
            }
            LightmapSettings.lightmaps = lightmaparray;

            // Set the correct light probes
            for (int i = 0; i < data.lightprobes.Length; i++)
            {
                LightmapSettings.lightProbes.bakedProbes[i] = data.lightprobes[i];
            }

            // Set the correct render settings
            RenderSettings.fog = data.renderSettings.fog;
            RenderSettings.fogStartDistance = data.renderSettings.fogStartDistance;
            RenderSettings.fogEndDistance = data.renderSettings.fogEndDistance;
            RenderSettings.fogMode = data.renderSettings.fogMode;
            RenderSettings.fogColor = data.renderSettings.fogColor;
            RenderSettings.fogDensity = data.renderSettings.fogDensity;
            RenderSettings.ambientMode = data.renderSettings.ambientMode;
            RenderSettings.ambientSkyColor = data.renderSettings.ambientSkyColor;
            RenderSettings.ambientEquatorColor = data.renderSettings.ambientEquatorColor;
            RenderSettings.ambientGroundColor = data.renderSettings.ambientGroundColor;
            RenderSettings.ambientIntensity = data.renderSettings.ambientIntensity;
            RenderSettings.ambientLight = data.renderSettings.ambientLight;
            RenderSettings.subtractiveShadowColor = data.renderSettings.subtractiveShadowColor;
            RenderSettings.skybox = data.renderSettings.skybox;
            RenderSettings.sun = data.renderSettings.sun;
            RenderSettings.ambientProbe = data.renderSettings.ambientProbe;
            RenderSettings.customReflection = data.renderSettings.customReflection;
            RenderSettings.reflectionIntensity = data.renderSettings.reflectionIntensity;
            RenderSettings.reflectionBounces = data.renderSettings.reflectionBounces;
            RenderSettings.defaultReflectionMode = data.renderSettings.defaultReflectionMode;
            RenderSettings.defaultReflectionResolution = data.renderSettings.defaultReflectionResolution;
            RenderSettings.haloStrength = data.renderSettings.haloStrength;
            RenderSettings.flareStrength = data.renderSettings.flareStrength;
            RenderSettings.flareFadeSpeed = data.renderSettings.flareFadeSpeed;
        }
    }
}
