using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class LightingSwitchRenderSettingsData : ScriptableObject
{
    public void Set()
    {
        this.fog = RenderSettings.fog;
        this.fogStartDistance = RenderSettings.fogStartDistance;
        this.fogEndDistance = RenderSettings.fogEndDistance;
        this.fogMode = RenderSettings.fogMode;
        this.fogColor = RenderSettings.fogColor;
        this.fogDensity = RenderSettings.fogDensity;
        this.ambientMode = RenderSettings.ambientMode;
        this.ambientSkyColor = RenderSettings.ambientSkyColor;
        this.ambientEquatorColor = RenderSettings.ambientEquatorColor;
        this.ambientGroundColor = RenderSettings.ambientGroundColor;
        this.ambientIntensity = RenderSettings.ambientIntensity;
        this.ambientLight = RenderSettings.ambientLight;
        this.subtractiveShadowColor = RenderSettings.subtractiveShadowColor;
        this.skybox = RenderSettings.skybox;
        this.sun = RenderSettings.sun;
        this.ambientProbe = RenderSettings.ambientProbe;
        this.customReflection = RenderSettings.customReflection;
        this.reflectionIntensity = RenderSettings.reflectionIntensity;
        this.reflectionBounces = RenderSettings.reflectionBounces;
        this.defaultReflectionMode = RenderSettings.defaultReflectionMode;
        this.defaultReflectionResolution = RenderSettings.defaultReflectionResolution;
        this.haloStrength = RenderSettings.haloStrength;
        this.flareStrength = RenderSettings.flareStrength;
        this.flareFadeSpeed = RenderSettings.flareFadeSpeed;
    }

    public void Apply()
    {
        RenderSettings.fog = this.fog;
        RenderSettings.fogStartDistance = this.fogStartDistance;
        RenderSettings.fogEndDistance = this.fogEndDistance;
        RenderSettings.fogMode = this.fogMode;
        RenderSettings.fogColor = this.fogColor;
        RenderSettings.fogDensity = this.fogDensity;
        RenderSettings.ambientMode = this.ambientMode;
        RenderSettings.ambientSkyColor = this.ambientSkyColor;
        RenderSettings.ambientEquatorColor = this.ambientEquatorColor;
        RenderSettings.ambientGroundColor = this.ambientGroundColor;
        RenderSettings.ambientIntensity = this.ambientIntensity;
        RenderSettings.ambientLight = this.ambientLight;
        RenderSettings.subtractiveShadowColor = this.subtractiveShadowColor;
        RenderSettings.skybox = this.skybox;
        RenderSettings.sun = this.sun;
        RenderSettings.ambientProbe = this.ambientProbe;
        RenderSettings.customReflection = this.customReflection;
        RenderSettings.reflectionIntensity = this.reflectionIntensity;
        RenderSettings.reflectionBounces = this.reflectionBounces;
        RenderSettings.defaultReflectionMode = this.defaultReflectionMode;
        RenderSettings.defaultReflectionResolution = this.defaultReflectionResolution;
        RenderSettings.haloStrength = this.haloStrength;
        RenderSettings.flareStrength = this.flareStrength;
        RenderSettings.flareFadeSpeed = this.flareFadeSpeed;
    }

    public bool fog;

    public float fogStartDistance;

    public float fogEndDistance;

    public FogMode fogMode;

    public Color fogColor;

    public float fogDensity;

    public AmbientMode ambientMode;

    public Color ambientSkyColor;

    public Color ambientEquatorColor;

    public Color ambientGroundColor;

    public float ambientIntensity;

    public Color ambientLight;

    public Color subtractiveShadowColor;

    public Material skybox;

    public Light sun;

    public SphericalHarmonicsL2 ambientProbe;

    public Cubemap customReflection;

    public float reflectionIntensity;

    public int reflectionBounces;

    public DefaultReflectionMode defaultReflectionMode;

    public int defaultReflectionResolution;

    public float haloStrength;

    public float flareStrength;

    public float flareFadeSpeed;
}
