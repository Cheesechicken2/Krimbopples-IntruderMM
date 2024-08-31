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

    }
