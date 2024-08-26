#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[InitializeOnLoad]
	public class Preferences
	{
		private static bool prefsLoaded = false;

		// The Preferences

		// SceneGUI
		public static Color textSceneGUIColor { get; private set; }
		public static Color textGUIColor { get; private set; }
		public static bool renderLabelBackgrounds { get; private set; }

		public static bool renderLines { get; private set; }
		public static bool renderArrow { get; private set; }
		public static bool renderLabel { get; private set; }
		public static bool renderBall { get; private set; }
		public static bool renderNestedActivator { get; private set; }

		public static float alphaLines { get; private set; }
		public static float alphaArrows { get; private set; }
		public static float alphaBall { get; private set; }

		public static int textSize { get; private set; }
		public static float lineWidth { get; private set; }
		public static float ballSize { get; private set; }
		public static float arrowSize { get; private set; }

		public static bool useWorldSpaceLabelSceneGUI { get; private set; }
		public static bool fadeLabelAtDistance { get; private set; }
		public static Vector2 fadeLabelDistances { get; private set; }

		// Toolbar
		public static bool showSceneToolbar { get; private set; }
		public static float alphaToolbar { get; private set; }
		public static float toolbarScale { get; private set; }

		// Activator
		public static bool drawEnable { get; private set; }
		public static bool drawDisable { get; private set; }
		public static bool drawRandomObjectsToEnable { get; private set; }
		public static bool drawToAnimate { get; private set; }
		public static bool drawToStopAnimate { get; private set; }
		public static bool drawCustomDoorsToUnlock { get; private set; }
		public static bool drawCustomDoorsLock { get; private set; }
		public static bool drawTeleportLocations { get; private set; }

		// Doors
		public static bool showDoorPath { get; set; }

		static Preferences()
		{
			if (!prefsLoaded)
			{
				LoadPreferences();
			}
		}

		[PreferenceItem("IntruderMM")]
		public static void CustomPreferencesGUI()
		{
			// Copyright
			EditorGUILayout.LabelField("IntruderMM Created by Superboss Games", EditorStyles.centeredGreyMiniLabel);
			EditorTools.InspectorGUILine(2);

			// Scene Options
			EditorGUILayout.BeginVertical("box");
			{
				EditorGUILayout.LabelField("Scene View Options", EditorTools.preferencesHeaderBox);
				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.BeginVertical("box");
					{
						EditorGUILayout.LabelField("Label Options", EditorTools.preferencesSmallHeaderBox);
						renderLabel = EditorGUILayout.Toggle("Show SceneGUI Labels? ", renderLabel);
						EditorGUI.BeginDisabledGroup(!renderLabel);
						{
							EditorGUI.indentLevel++;
							textSize = EditorGUILayout.IntSlider("SceneGUI Text Size", textSize, 1, 32);
							textSceneGUIColor = EditorGUILayout.ColorField("SceneGUI Text Color", textSceneGUIColor);
							renderLabelBackgrounds = EditorGUILayout.Toggle("Show Label Background? ", renderLabelBackgrounds);
							useWorldSpaceLabelSceneGUI = EditorGUILayout.Toggle("Use Worldspace Label? ", useWorldSpaceLabelSceneGUI);
							// followCamera = EditorGUILayout.Toggle("Use Worldspace Label? ", useWorldSpaceLabelSceneGUI);
							fadeLabelAtDistance = EditorGUILayout.Toggle("Fade Label At Distance? ", fadeLabelAtDistance);
							EditorGUI.BeginDisabledGroup(!fadeLabelAtDistance);
							{
								EditorGUI.indentLevel++;
								fadeLabelDistances = EditorGUILayout.Vector2Field("Fade Distances", fadeLabelDistances);
								EditorGUILayout.LabelField("X = Close, Y = Far", EditorStyles.centeredGreyMiniLabel);
								EditorGUI.indentLevel--;
							}
							EditorGUI.EndDisabledGroup();
							EditorGUI.indentLevel--;
						}
						EditorGUI.EndDisabledGroup();

						textGUIColor = EditorGUILayout.ColorField("GUI Text Color", textGUIColor);
					}
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Toolbar Options", EditorTools.preferencesSmallHeaderBox);
					showSceneToolbar = EditorGUILayout.Toggle("Show Scene Toolbar? ", showSceneToolbar);
					toolbarScale = EditorGUILayout.Slider("Toolbar Scale", toolbarScale, 0.75f, 2);
					alphaToolbar = EditorGUILayout.Slider("Toolbar Alpha", alphaToolbar, 0.05f, 1);
				}
				EditorGUILayout.EndVertical();

				EditorGUILayout.EndVertical();

				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Show Toggles", EditorTools.preferencesSmallHeaderBox);
					renderLines = EditorGUILayout.Toggle("Show Guide Lines? ", renderLines);
					renderArrow = EditorGUILayout.Toggle("Show Guide Arrows? ", renderArrow);
					renderBall = EditorGUILayout.Toggle("Show Guide Balls? ", renderBall);
					renderNestedActivator = EditorGUILayout.Toggle("Show Nested GUI? ", renderNestedActivator);
				}
				EditorGUILayout.EndVertical();


				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Transparency Sliders", EditorTools.preferencesSmallHeaderBox);
					alphaLines = EditorGUILayout.Slider("Lines Alpha", alphaLines, 0.05f, 1);
					alphaArrows = EditorGUILayout.Slider("Arrows Alpha", alphaArrows, 0.05f, 1);
					alphaBall = EditorGUILayout.Slider("Ball Alpha", alphaBall, 0.05f, 1);
				}
				EditorGUILayout.EndVertical();


				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Size Sliders", EditorTools.preferencesSmallHeaderBox);
					lineWidth = EditorGUILayout.Slider("Line Width", lineWidth, 0.1f, 32);
					ballSize = EditorGUILayout.Slider("Ball Size", ballSize, 0.01f, 1);
					arrowSize = EditorGUILayout.Slider("Arrow Size", arrowSize, 0.01f, 2);
				}
				EditorGUILayout.EndVertical();
			}
			EditorGUILayout.EndVertical();



			// Activator Options
			EditorGUILayout.BeginVertical("box");
			{
				EditorGUILayout.LabelField("Activator Options", EditorTools.preferencesHeaderBox);
				EditorGUILayout.BeginVertical("box");
				{
					EditorGUILayout.LabelField("Show Toggles", EditorTools.preferencesSmallHeaderBox);
					drawEnable = EditorGUILayout.Toggle("Objects to Enable?", drawEnable);
					drawDisable = EditorGUILayout.Toggle("Objects to Disable?", drawDisable);
					drawRandomObjectsToEnable = EditorGUILayout.Toggle("Objects to Randomly Enable?", drawRandomObjectsToEnable);

					drawToAnimate = EditorGUILayout.Toggle("Objects to Animate?", drawToAnimate);
					drawToStopAnimate = EditorGUILayout.Toggle("Objects to Stop Animating?", drawToStopAnimate);

					drawCustomDoorsToUnlock = EditorGUILayout.Toggle("Doors to Unlock?", drawCustomDoorsToUnlock);
					drawCustomDoorsLock = EditorGUILayout.Toggle("Doors to Lock?", drawCustomDoorsLock);

					drawTeleportLocations = EditorGUILayout.Toggle("Teleport Locations?", drawTeleportLocations);
				}
				EditorGUILayout.EndVertical();

			}
			EditorGUILayout.EndVertical();

			if (GUILayout.Button("Reset Preferences"))
			{
				EditorPrefs.DeleteKey("showToolbar" + "Key");
				EditorPrefs.DeleteKey("renderLabelBackgrounds" + "Key");

				EditorPrefs.DeleteKey("textSceneGUIColor-R");
				EditorPrefs.DeleteKey("textSceneGUIColor-G");
				EditorPrefs.DeleteKey("textSceneGUIColor-B");
				EditorPrefs.DeleteKey("textSceneGUIColor-A");

				EditorPrefs.DeleteKey("textGUIColor-R");
				EditorPrefs.DeleteKey("textGUIColor-G");
				EditorPrefs.DeleteKey("textGUIColor-B");
				EditorPrefs.DeleteKey("textGUIColor-A");

				EditorPrefs.DeleteKey("renderLines" + "Key");
				EditorPrefs.DeleteKey("renderArrow" + "Key");
				EditorPrefs.DeleteKey("renderLabel" + "Key");
				EditorPrefs.DeleteKey("renderBall" + "Key");
				EditorPrefs.DeleteKey("renderNestedActivator" + "Key");

				EditorPrefs.DeleteKey("alphaLines" + "Key");
				EditorPrefs.DeleteKey("alphaArrows" + "Key");
				EditorPrefs.DeleteKey("alphaBall" + "Key");
				EditorPrefs.DeleteKey("alphaToolbar" + "Key");

				EditorPrefs.DeleteKey("textSize" + "Key");
				EditorPrefs.DeleteKey("lineWidth" + "Key");
				EditorPrefs.DeleteKey("ballSize" + "Key");
				EditorPrefs.DeleteKey("arrowSize" + "Key");

				EditorPrefs.DeleteKey("useWorldSpaceLabelSceneGUI" + "Key");
				EditorPrefs.DeleteKey("fadeLabelAtDistance" + "Key");
				EditorPrefs.DeleteKey("fadeLabelAtDistance" + "Key");

				EditorPrefs.DeleteKey("drawEnable" + "Key");
				EditorPrefs.DeleteKey("drawDisable" + "Key");
				EditorPrefs.DeleteKey("drawRandomObjectsToEnable" + "Key");
				EditorPrefs.DeleteKey("drawToAnimate" + "Key");
				EditorPrefs.DeleteKey("drawToStopAnimate" + "Key");
				EditorPrefs.DeleteKey("drawCustomDoorsToUnlock" + "Key");
				EditorPrefs.DeleteKey("drawCustomDoorsLock" + "Key");
				EditorPrefs.DeleteKey("drawTeleportLocations" + "Key");

				EditorPrefs.DeleteKey("showDoorPath" + "Key");
				EditorPrefs.DeleteKey("toolbarScale" + "Key");

				LoadPreferences();
			}

			// Bottom
			GUILayout.FlexibleSpace();

			if (GUI.changed)
			{
				EditorPrefs.SetBool("showToolbar" + "Key", showSceneToolbar);

				EditorPrefs.SetBool("renderLabelBackgrounds" + "Key", renderLabelBackgrounds);

				// Text Color Crap
				EditorPrefs.SetFloat("textSceneGUIColor-R", textSceneGUIColor.r);
				EditorPrefs.SetFloat("textSceneGUIColor-G", textSceneGUIColor.g);
				EditorPrefs.SetFloat("textSceneGUIColor-B", textSceneGUIColor.b);
				EditorPrefs.SetFloat("textSceneGUIColor-A", textSceneGUIColor.a);

				// Text Color Crap
				EditorPrefs.SetFloat("textGUIColor-R", textGUIColor.r);
				EditorPrefs.SetFloat("textGUIColor-G", textGUIColor.g);
				EditorPrefs.SetFloat("textGUIColor-B", textGUIColor.b);
				EditorPrefs.SetFloat("textGUIColor-A", textGUIColor.a);

				EditorPrefs.SetBool("renderLines" + "Key", renderLines);
				EditorPrefs.SetBool("renderArrow" + "Key", renderArrow);
				EditorPrefs.SetBool("renderLabel" + "Key", renderLabel);
				EditorPrefs.SetBool("renderBall" + "Key", renderBall);
				EditorPrefs.SetBool("renderNestedActivator" + "Key", renderNestedActivator);

				EditorPrefs.SetFloat("alphaLines" + "Key", alphaLines);
				EditorPrefs.SetFloat("alphaArrows" + "Key", alphaArrows);
				EditorPrefs.SetFloat("alphaBall" + "Key", alphaBall);
				EditorPrefs.SetFloat("alphaToolbar" + "Key", alphaToolbar);

				EditorPrefs.SetInt("textSize" + "Key", textSize);
				EditorPrefs.SetFloat("lineWidth" + "Key", lineWidth);
				EditorPrefs.SetFloat("ballSize" + "Key", ballSize);
				EditorPrefs.SetFloat("arrowSize" + "Key", arrowSize);

				EditorPrefs.SetBool("useWorldSpaceLabelSceneGUI" + "Key", useWorldSpaceLabelSceneGUI);
				EditorPrefs.SetFloat("fadeLabelAtDistance" + "KeyX", fadeLabelDistances.x);
				EditorPrefs.SetFloat("fadeLabelAtDistance" + "KeyY", fadeLabelDistances.y);

				EditorPrefs.SetBool("drawEnable" + "Key", drawEnable);
				EditorPrefs.SetBool("drawDisable" + "Key", drawDisable);
				EditorPrefs.SetBool("drawRandomObjectsToEnable" + "Key", drawRandomObjectsToEnable);
				EditorPrefs.SetBool("drawToAnimate" + "Key", drawToAnimate);
				EditorPrefs.SetBool("drawToStopAnimate" + "Key", drawToStopAnimate);
				EditorPrefs.SetBool("drawCustomDoorsToUnlock" + "Key", drawCustomDoorsToUnlock);
				EditorPrefs.SetBool("drawCustomDoorsLock" + "Key", drawCustomDoorsLock);
				EditorPrefs.SetBool("drawTeleportLocations" + "Key", drawTeleportLocations);

				EditorPrefs.SetBool("showDoorPath" + "Key", showDoorPath);
				EditorPrefs.SetFloat("toolbarScale" + "Key", toolbarScale);
			}
		}

		public static void InspectorGUIPreferences()
		{
			EditorGUILayout.BeginVertical("Box");
			Preferences.CustomPreferencesGUI();
			EditorGUILayout.EndVertical();
		}

		private static void LoadPreferences()
		{
			showSceneToolbar = EditorPrefs.GetBool("showToolbar" + "Key", true);

			// Four floats for each RGBA
			textSceneGUIColor = new Color(EditorPrefs.GetFloat("textSceneGUIColor-R", 1), EditorPrefs.GetFloat("textSceneGUIColor-G", 1), EditorPrefs.GetFloat("textSceneGUIColor-B", 1), EditorPrefs.GetFloat("textSceneGUIColor-A", 1));
			textGUIColor = new Color(EditorPrefs.GetFloat("textGUIColor-R", 1), EditorPrefs.GetFloat("textGUIColor-G", 1), EditorPrefs.GetFloat("textGUIColor-B", 1), EditorPrefs.GetFloat("textGUIColor-A", 1));

			renderLabelBackgrounds = EditorPrefs.GetBool("renderLabelBackgrounds" + "Key", true);

			renderLines = EditorPrefs.GetBool("renderLines" + "Key", true);
			renderArrow = EditorPrefs.GetBool("renderArrow" + "Key", true);
			renderLabel = EditorPrefs.GetBool("renderLabel" + "Key", true);
			renderBall = EditorPrefs.GetBool("renderBall" + "Key", true);
			renderNestedActivator = EditorPrefs.GetBool("renderNestedActivator" + "Key", true);

			alphaLines = EditorPrefs.GetFloat("alphaLines" + "Key", 0.75f);
			alphaArrows = EditorPrefs.GetFloat("alphaArrows" + "Key", 0.25f);
			alphaBall = EditorPrefs.GetFloat("alphaBall" + "Key", 0.5f);
			alphaToolbar = EditorPrefs.GetFloat("alphaToolbar" + "Key", 1f);

			textSize = EditorPrefs.GetInt("textSize" + "Key", 8);
			lineWidth = EditorPrefs.GetFloat("lineWidth" + "Key", 3);
			ballSize = EditorPrefs.GetFloat("ballSize" + "Key", 0.1f);
			arrowSize = EditorPrefs.GetFloat("arrowSize" + "Key", 0.6f);

			useWorldSpaceLabelSceneGUI = EditorPrefs.GetBool("useWorldSpaceLabelSceneGUI" + "Key", true);
			fadeLabelAtDistance = EditorPrefs.GetBool("fadeLabelAtDistance" + "Key", true);
			fadeLabelDistances = new Vector2(EditorPrefs.GetFloat("fadeLabelAtDistance" + "KeyX", 5), EditorPrefs.GetFloat("fadeLabelAtDistance" + "KeyY", 15));

			drawEnable = EditorPrefs.GetBool("drawEnable" + "Key", true);
			drawDisable = EditorPrefs.GetBool("drawDisable" + "Key", true);
			drawRandomObjectsToEnable = EditorPrefs.GetBool("drawRandomObjectsToEnable" + "Key", true);
			drawToAnimate = EditorPrefs.GetBool("drawToAnimate" + "Key", true);
			drawToStopAnimate = EditorPrefs.GetBool("drawToStopAnimate" + "Key", true);
			drawCustomDoorsToUnlock = EditorPrefs.GetBool("drawCustomDoorsToUnlock" + "Key", true);
			drawCustomDoorsLock = EditorPrefs.GetBool("drawCustomDoorsLock" + "Key", true);
			drawTeleportLocations = EditorPrefs.GetBool("drawTeleportLocations" + "Key", true);

			showDoorPath = EditorPrefs.GetBool("showDoorPath" + "Key", false);

			toolbarScale = EditorPrefs.GetFloat("toolbarScale" + "Key", 1f);

			prefsLoaded = true;
		}
	}
}

#endif