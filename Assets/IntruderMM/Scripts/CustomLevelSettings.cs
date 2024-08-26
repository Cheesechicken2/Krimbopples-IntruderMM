using System;
using UnityEngine;

public class CustomLevelSettings : MonoBehaviour
{
	//No need to attach this to any object
	public bool fog = false;
	public Color fogColor;
	public FogMode fogMode;
	public float fogDensity;
	public float fogStartDistance;
	public float fogEndDistance;
	public Color ambientLight;
	public Material skybox;
	public float haloStrength;
	public float flareStrength;
	public float flareFadeSpeed;

	public LightProbes lightProbes;
	public bool forceFog = false;
	public string mmVersion = "";
	public string unityVersion = "";
	public void SetSettings()
	{
		this.hideFlags = HideFlags.HideInHierarchy;
		fog = RenderSettings.fog;
		fogColor = RenderSettings.fogColor;
		fogMode = RenderSettings.fogMode;
		fogDensity = RenderSettings.fogDensity;
		fogStartDistance = RenderSettings.fogStartDistance;
		fogEndDistance = RenderSettings.fogEndDistance;
		ambientLight = RenderSettings.ambientLight;
		skybox = RenderSettings.skybox;
		haloStrength = RenderSettings.haloStrength;
		flareStrength = RenderSettings.flareStrength;
		flareFadeSpeed = RenderSettings.flareFadeSpeed;
		// haloTexture = RenderSettings.haloTexture;
		// spotCookie = RenderSettings.spotCookie;
		lightProbes = LightmapSettings.lightProbes;
	}

	public void SetVersion(string mm)
	{
		mmVersion = mm;
		unityVersion = Application.unityVersion;
	}

	public void LoadSettings()
	{
		// RenderSettings.fog = fog;
		// RenderSettings.fogColor = fogColor;
		// RenderSettings.fogMode = fogMode;
		// RenderSettings.fogDensity = fogDensity;
		// RenderSettings.fogStartDistance = fogStartDistance;
		// RenderSettings.fogEndDistance = fogEndDistance;
		// RenderSettings.ambientLight = ambientLight;
		// RenderSettings.skybox = skybox;
		// RenderSettings.haloStrength = haloStrength;
		// RenderSettings.flareStrength = flareStrength;
		// RenderSettings.flareFadeSpeed = flareFadeSpeed;
		// LightmapSettings.lightProbes = lightProbes;
	}
}
