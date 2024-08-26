#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine.Rendering;
using Sabresaurus.SabreCSG;

namespace Assets.IntruderMM.Editor
{
	[InitializeOnLoad]
	public static class ProjectSettings
	{
		static ProjectSettings()
		{
			AssignProjectSettings();
		}

		private static void AssignProjectSettings()
		{
			if (PlayerSettings.colorSpace == ColorSpace.Linear || EditorGraphicsSettings.GetTierSettings(BuildTargetGroup.Standalone, GraphicsTier.Tier3).renderingPath == RenderingPath.DeferredShading) { return; }

			Debug.Log("Assigning Project Settings!");

			PlayerSettings.productName = "IntruderMM";
			PlayerSettings.companyName = "Superboss Games";
			PlayerSettings.colorSpace = ColorSpace.Linear;

			// Change rendering to defered and stuff
			TierSettings settings = new TierSettings();
			settings.standardShaderQuality = ShaderQuality.High;
			settings.reflectionProbeBoxProjection = true;
			settings.reflectionProbeBlending = true;
			settings.detailNormalMap = true;
			settings.semitransparentShadows = true;
			settings.hdr = true;
			settings.hdrMode = CameraHDRMode.FP16;
			settings.cascadedShadowMaps = true;
			settings.enableLPPV = true;
			settings.realtimeGICPUUsage = RealtimeGICPUUsage.Medium;
			settings.renderingPath = RenderingPath.DeferredShading;
			EditorGraphicsSettings.SetTierSettings(BuildTargetGroup.Standalone, GraphicsTier.Tier3, settings);
			EditorPrefs.SetBool("projectSettingsAssingedKey", true);
		}
	}
}

#endif