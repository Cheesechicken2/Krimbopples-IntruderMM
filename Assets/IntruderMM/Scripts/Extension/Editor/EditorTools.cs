#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Sabresaurus.SabreCSG;
using System.Reflection;

namespace Assets.IntruderMM.Editor
{
	/// <summary>
	/// EditorTools.cs handles the scene toolbar and frequently used methods
	/// </summary>
	[InitializeOnLoad]
	public static class EditorTools
	{
		public const float SUB_ACTIVATOR_ALPHA = 0.5f;

		/// <summary>Has the cool Share Tech Mono font</summary>
		public static GUISkin intruderSkin;
		private static Texture2D boxTex;

		public static GenericMenu proxyMenu;

		static System.Type sceneViewToolbar = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.SceneView"); 

		public static Texture2D intruderLogo;
        public static Texture2D expandedLogo;

        private static Texture2D buttonTex;
		private static Texture2D buttonHoverTex;
		private static Texture2D buttonSelectTex;

		private static Texture2D sideButtonTex;
		private static Texture2D sideButtonHoverTex;

		private static bool infoBox = false;
		private static string infoBoxTitle;
		private static string infoBoxText;

		const int toolbarBottomRightHeight = 64;
		const int toolbarBottomLeftHeight = 88;
		const int toolbarActivatorPanelHeight = 150;

		/// <summary>Intruder styled GUIStyle box</summary>
		public static GUIStyle box;

		/// <summary>Intruder styled GUIStyle box for the preferences window</summary>
		public static GUIStyle preferencesHeaderBox;

		/// <summary>Intruder styled GUIStyle small box for the preferences window</summary>
		public static GUIStyle preferencesSmallHeaderBox;

		/// <summary>Intruder styled GUIStyle button with yellow at bottom</summary>
		public static GUIStyle bottomButton;

		/// <summary>Intruder styled GUIStyle button with yellow to left</summary>
		public static GUIStyle sideButton;

		/// <summary>Label Scene GUI with colors to preferences and is used for SceneGUI</summary>
		public static GUIStyle labelSceneGUI;

		/// <summary>Label GUI Style</summary>
		public static GUIStyle labelGUI;

		/// <summary>Very large plane used for Preview Window</summary>
		public static Mesh largePlane;

		/// <summary>Unlit Grid Material</summary>
		public static Material unlitGrid;

		static EditorTools()
		{
			OnEnable();
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}
		/// <summary>Called on initialize</summary>
		private static void OnEnable()
		{
			// Intruder GUI Skin
			intruderSkin = ScriptableObject.CreateInstance<GUISkin>();
			intruderSkin.font = AssetDatabase.LoadAssetAtPath<Font>(EditorToolsPath() + "GUI/Font/ShareTechMono-Regular.ttf");

			boxTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/box.png");

			intruderLogo = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/title.png");

            buttonTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/button.png");
			buttonHoverTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/buttonHover.png");
			buttonSelectTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/buttonSelect.png");

			sideButtonTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/buttonSide.png");
			sideButtonHoverTex = AssetDatabase.LoadAssetAtPath<Texture2D>(EditorToolsPath() + "GUI/buttonSideHover.png");

			largePlane = AssetDatabase.LoadAssetAtPath<Mesh>(EditorToolsPath() + "Models/mdl_LargePlane.fbx");
			unlitGrid = AssetDatabase.LoadAssetAtPath<Material>(EditorToolsPath() + "Materials/mat_UnlitGrid.mat");

			proxyMenu = new GenericMenu();
		}

		/// <summary>OnSceneGUI Assigned Delegate</summary>
		private static void OnSceneGUI(SceneView sceneView)
		{
			if (sceneView == null || BuildPipeline.isBuildingPlayer) { return; }
			if (CSGModel.GetActiveCSGModel() != null) { return; }

			Handles.BeginGUI();
			{
				GUI.skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game);
				GUI.skin.font = intruderSkin.font;

				GUI.backgroundColor = new Color(1, 1, 1, Preferences.alphaToolbar);

				box = new GUIStyle("Label");
				box.font = intruderSkin.font;
				box.fontSize = 15;
				box.normal.textColor = Preferences.textGUIColor;
				box.alignment = TextAnchor.MiddleCenter;
				box.normal.background = boxTex;
				box.padding = new RectOffset(8, 8, 8, 8);
				box.border = new RectOffset(4, 4, 4, 4);

				preferencesHeaderBox = new GUIStyle(box);
				preferencesHeaderBox.fontSize = 16;
				preferencesHeaderBox.fontStyle = FontStyle.Bold;

				preferencesSmallHeaderBox = new GUIStyle("Label");
				preferencesSmallHeaderBox.fontSize = 16;
				// preferencesSmallHeaderBox.fontStyle = FontStyle.Bold;
				preferencesSmallHeaderBox.alignment = TextAnchor.MiddleLeft;
				preferencesSmallHeaderBox.normal.textColor = Color.black;
				preferencesSmallHeaderBox.font = intruderSkin.font;

				bottomButton = new GUIStyle("Label");
				bottomButton.alignment = TextAnchor.MiddleCenter;

				bottomButton.normal.background = buttonTex;
				bottomButton.normal.textColor = Preferences.textGUIColor;

				bottomButton.active.background = buttonSelectTex;
				bottomButton.active.textColor = Preferences.textGUIColor;

				bottomButton.hover.background = buttonHoverTex;
				bottomButton.hover.textColor = Preferences.textGUIColor;

				bottomButton.fontSize = 16 * Mathf.FloorToInt(Preferences.toolbarScale);
				bottomButton.padding = new RectOffset(4, 4, 4, 4);
				bottomButton.border = new RectOffset(4, 5, 5, 5);

				bottomButton.font = intruderSkin.font;
				bottomButton.normal.textColor = Preferences.textGUIColor;

				sideButton = new GUIStyle(bottomButton);
				sideButton.normal.background = sideButtonTex;
				sideButton.hover.background = sideButtonHoverTex;
				sideButton.border = new RectOffset(5, 5, 5, 5);

				labelGUI = new GUIStyle("Label");
				labelGUI.fontSize = 13;
				labelGUI.margin = new RectOffset(-15, -15, -15, -15);
				labelGUI.fontStyle = FontStyle.Bold;
				labelGUI.alignment = TextAnchor.MiddleLeft;
				labelGUI.normal.textColor = Color.black;
				// labelGUI.font = intruderSkin.font;

				labelSceneGUI = new GUIStyle("Label");
				labelSceneGUI.fontSize = Preferences.textSize;
				labelSceneGUI.alignment = TextAnchor.MiddleCenter;
				labelSceneGUI.normal.textColor = Preferences.textSceneGUIColor;
				labelSceneGUI.font = intruderSkin.font;

				if (Preferences.renderLabelBackgrounds)
				{
					labelSceneGUI.normal.background = boxTex;
					labelSceneGUI.border = new RectOffset(4, 4, 4, 4);
					labelSceneGUI.padding = new RectOffset(0, 0, 4, 4);
				}

				// Toolbar
				if (Preferences.showSceneToolbar)
				{
					// BOTTOM LEFT ANCHOR
					GUILayout.BeginArea(new Rect(10, sceneView.position.height - toolbarBottomLeftHeight * Preferences.toolbarScale - 16 - 10, (120 * Preferences.toolbarScale) * 3, toolbarBottomLeftHeight * Preferences.toolbarScale));
					{
						GUILayout.Label("IntruderMM", box, GUILayout.MaxHeight(32 * Preferences.toolbarScale), GUILayout.MaxWidth(128 * Preferences.toolbarScale));
						GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
						{
							if (GUILayout.Button("Test Map", bottomButton, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
							{
								SceneTesting.CheckNameAndPlayInIntruder();
							}
							if (GUILayout.Button("Upload Map", bottomButton, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
							{
								ExportLevel.LoadUploadWindow();
							}
							if (GUILayout.Button("Discord", bottomButton, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
							{
								Application.OpenURL("www.superbossgames.com/chat");
							}
						}
						GUILayout.EndHorizontal();
					}
					GUILayout.EndArea();

					// BOTTOM RIGHT ANCHOR
					GUILayout.BeginArea(new Rect(sceneView.position.width - 10 - 160 * Preferences.toolbarScale, sceneView.position.height - toolbarBottomRightHeight * Preferences.toolbarScale - 16 - 10, 160 * Preferences.toolbarScale, toolbarBottomRightHeight * Preferences.toolbarScale));
					{
						if (GUILayout.Button("Spawn Proxy", bottomButton, GUILayout.MaxWidth(160 * Preferences.toolbarScale), GUILayout.ExpandHeight(true)))
						{
							CreateMenuItem(proxyMenu, "Intruder Spawn", "Assets/IntruderMM/Prefabs/SpawnB.prefab");
							CreateMenuItem(proxyMenu, "Guard Spawn", "Assets/IntruderMM/Prefabs/SpawnA.prefab");

							proxyMenu.AddItem(new GUIContent("Activator"), false, CreateActivator);

							CreateMenuItem(proxyMenu, "Gamemode/Raid/Briefcase", "Assets/IntruderMM/Prefabs/BriefcaseProxy.prefab");
							CreateMenuItem(proxyMenu, "Gamemode/Raid/Goal Point", "Assets/IntruderMM/Prefabs/GoalPointProxy.prefab");

							CreateMenuItem(proxyMenu, "Gamemode/Hack/Create Gamemode", "Assets/IntruderMM/Prefabs/HackModeProxy.prefab");
							CreateMenuItem(proxyMenu, "Gamemode/Hack/Hack Node", "Assets/IntruderMM/Prefabs/HackNodeProxy.prefab");

							CreateMenuItem(proxyMenu, "Generic/Pickup", "Assets/IntruderMM/Prefabs/Pickup.prefab");
							CreateMenuItem(proxyMenu, "Generic/Glass", "Assets/IntruderMM/Prefabs/GlassProxy.prefab");
							CreateMenuItem(proxyMenu, "Generic/Observe Camera", "Assets/IntruderMM/Prefabs/ObserveCamProxy.prefab");
							CreateMenuItem(proxyMenu, "Generic/Ladder", "Assets/IntruderMM/Prefabs/Ladder.prefab");
							CreateMenuItem(proxyMenu, "Generic/Note", "Assets/IntruderMM/Prefabs/NoteProxy.prefab");
							CreateMenuItem(proxyMenu, "Generic/Keypad", "Assets/IntruderMM/Prefabs/Keypad.prefab");
							CreateMenuItem(proxyMenu, "Generic/Mirror", "Assets/IntruderMM/Prefabs/Proxy/Mirror.prefab");
							CreateMenuItem(proxyMenu, "Generic/HeliGun", "Assets/IntruderMM/Prefabs/Proxy/HeliGun.prefab");
							proxyMenu.AddItem(new GUIContent("Generic/Compass Direction"), false, CreateCompassDirection);
							proxyMenu.ShowAsContext();
						}
					
					GUILayout.EndArea();

					// Activator Info
					if (infoBox)
					{
						GUIStyle descText = new GUIStyle("Label");
						descText.alignment = TextAnchor.UpperLeft;
						descText.fontSize = 14;
						GUILayout.BeginArea(new Rect(sceneView.position.width - 10 - 400, sceneView.position.height - toolbarActivatorPanelHeight - toolbarBottomRightHeight - 16 - 10 - 10, 400, toolbarActivatorPanelHeight));
						{
							EditorGUILayout.BeginVertical(box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
							GUILayout.Label(infoBoxTitle, labelGUI, GUILayout.ExpandHeight(false), GUILayout.MaxHeight(24));
							InspectorGUILine(1);
							GUILayout.Label(infoBoxText, descText, GUILayout.ExpandWidth(true));
							EditorGUILayout.EndVertical();
						}
						GUILayout.EndArea();
					}
					else { infoBox = false; }
				}

			}
			Handles.EndGUI();
		}

		public static GUIStyle HandleLabelStyle(GUIStyle input)
		{
			GUIStyle style = new GUIStyle(input);
			style.alignment = TextAnchor.MiddleCenter;
			return style;
		}

		/// <summary>Creates a line</summary>
		public static void InspectorGUILine(int height = 1)
		{
			Rect rect = EditorGUILayout.GetControlRect(false, height);
			rect.height = height;
			EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
		}

		/// <summary>Used for Spawn Proxy proxyMenu</summary>
		private static void CreateMenuItem(GenericMenu proxyMenu, string path, string proxy)
		{
			proxyMenu.AddItem((new GUIContent(path)), false, SpawnProxy, AssetDatabase.LoadAssetAtPath<GameObject>(proxy));
		}

		/// <summary>Used for creating activator</summary>
		private static void CreateActivator()
		{
			GameObject activator = new GameObject("Activator");
			activator.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(Vector3.forward * 5);
			activator.AddComponent<Activator>();
			Selection.activeGameObject = activator;
		}

		/// <summary>Used for creating a compass direction</summary>
		private static void CreateCompassDirection()
		{
			GameObject compassDirection = new GameObject("Compass Direction");
			compassDirection.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(Vector3.forward * 5);
			compassDirection.AddComponent<CompassDirection>();
			Selection.activeGameObject = compassDirection;
		}

		/// <summary>Used for Spawn Proxy proxyMenu</summary>
		private static void SpawnProxy(object obj)
		{
			GameObject gameObject = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)obj);
			gameObject.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(Vector3.forward * 5);
			Selection.activeGameObject = gameObject;
		}

		/// <summary>Creates an Info Box above the Proxy Spawner button</summary>
		/// <param name="title">Header text of info box</param>
		/// <param name="desc">Description of info box</param>
		public static void EnableInfoBox(string title, string desc)
		{
			infoBox = true;
			infoBoxTitle = title;
			infoBoxText = desc;
		}

		/// <summary>Disables info box</summary>
		public static void DisableInfoBox()
		{
			infoBox = false;
		}

		/// <returns>EditorTools Path</returns>
		public static string EditorToolsPath()
		{
			string[] guids = AssetDatabase.FindAssets("EditorTools t:Script");
			foreach (string guid in guids)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				string suffix = "EditorTools.cs";
				if (path.EndsWith(suffix))
				{
					path = path.Remove(path.Length - suffix.Length, suffix.Length);
					return path;
				}
			}
			return string.Empty;
		}

		/// <returns>GUI Helpbox for Superboss Games Copyright</returns>
		public static void Copyright()
		{
			// Copyright Superboss Games Inc. 2021
			// EditorGUILayout.HelpBox("IntruderMM - Superboss Games", MessageType.None);
		}

		/// <returns>Draw lines to objects from array</returns>
		static public void DrawLinesToObjects(Object[] objects, string textLabel, Color color, Component _target, float alphaMultiplier)
		{
			if (objects == null || _target == null)
			{
				return;
			}

			if (objects is GameObject[])
			{
				foreach (GameObject item in objects as GameObject[])
				{
					if (item == null) { continue; }
					RenderPoints(_target.gameObject, item, color, textLabel, alphaMultiplier);
				}

				return;
			}

			if (objects is Component[])
			{
				foreach (Component item in objects as Component[])
				{
					if (item == null) { continue; }
					RenderPoints(_target.gameObject, item.gameObject, color, textLabel, alphaMultiplier);
				}
			}
		}

		/// <returns>Draw lines between two points</returns>
		public static void RenderPoints(GameObject pos, GameObject targetPos, Color lineColor, string Prefix, float alphaMultiplier)
		{
			// float pointLerp =  Vector3.Distance(pos.transform.position, SceneView.lastActiveSceneView.camera.transform.position).Remap(0, Vector3.Distance(pos.transform.position, targetPos.transform.position), 0, 1);
			Vector3 labelPoint = Vector3.Lerp(pos.transform.position, targetPos.transform.position, 1);
			StaticRenderWorldGUI(Prefix + targetPos.name, labelPoint, labelSceneGUI, alphaMultiplier);

			lineColor.a = alphaMultiplier * Preferences.alphaLines;
			Handles.color = lineColor;
			if (Preferences.renderLines) Handles.DrawAAPolyLine(Preferences.lineWidth, pos.transform.position, targetPos.transform.position);
			lineColor.a = alphaMultiplier * Preferences.alphaBall;
			Handles.color = lineColor;
			if (Preferences.renderBall) Handles.SphereHandleCap(0, pos.transform.position, pos.transform.rotation, Preferences.ballSize, EventType.Repaint);

			// Draws arrow if to far of a distance
			float distance = Vector3.Distance(pos.transform.position, targetPos.transform.position);
			if (distance > 2 && Preferences.renderArrow)
			{
				Handles.color = new Color(lineColor.r, lineColor.g, lineColor.b, alphaMultiplier * Preferences.alphaArrows);
				Vector3 relativePos = targetPos.transform.position - pos.transform.position;
				Handles.ArrowHandleCap(0, Vector3.Lerp(pos.transform.position, targetPos.transform.position, 0.125f), Quaternion.LookRotation(relativePos), Preferences.arrowSize, EventType.Repaint);
				if (distance > 6) Handles.ArrowHandleCap(0, Vector3.Lerp(pos.transform.position, targetPos.transform.position, 0.75f), Quaternion.LookRotation(relativePos), Preferences.arrowSize, EventType.Repaint);
			}
		}

		/// <summary>For Screen Camera Renderer</summary>
		public static bool TestCone(Vector3 inputPoint, Transform outputPoint)
		{
			float cosAngle = Vector3.Dot((inputPoint - outputPoint.position).normalized, outputPoint.forward * 1);
			float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
			return angle < 90;
		}

		/// <summary>Add ignore sticky to gameObject</summary>
		[MenuItem("CONTEXT/Transform/Add Activator")]
		public static void AddActivator(MenuCommand command)
		{
			Transform trans = (Transform)command.context;
			trans.gameObject.AddComponent<Activator>();
		}

		/// <summary>Add ignore sticky to gameObject</summary>
		[MenuItem("CONTEXT/Transform/Add Ignore Sticky")]
		public static void AddIgnoreSticky(MenuCommand command)
		{
			Transform trans = (Transform)command.context;
			trans.gameObject.AddComponent<IgnoreSticky>();
		}

		/// <summary>Remap Value</summary>
		public static float Remap(this float value, float from1, float to1, float from2, float to2)
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

		/// <summary>New rendering for World space ScenGUI</summary>
		public static void StaticRenderWorldGUI(string text, Vector3 renderPos, GUIStyle style, float labelAlpha)
		{
			if (!Preferences.renderLabel) { return; }

			Camera sceneCamera = SceneView.lastActiveSceneView.camera;
			if (!TestCone(renderPos, sceneCamera.transform)) { return; }

			float distanceBetweenCamAndTarget = Vector3.Distance(renderPos, sceneCamera.transform.position);

			if (Preferences.fadeLabelAtDistance)
			{
				if (distanceBetweenCamAndTarget >= Preferences.fadeLabelDistances.y) { return; }
				GUI.color = new Color(1, 1, 1, distanceBetweenCamAndTarget.Remap(Preferences.fadeLabelDistances.x, Preferences.fadeLabelDistances.y, 1 * labelAlpha, 0));
			}

			// Vector3 worldSpacePosition = sceneCamera.WorldToScreenPoint(renderPos);
			if (Preferences.useWorldSpaceLabelSceneGUI)
			{
				Vector2 labelScaling = Preferences.textSize * style.CalcSize(new GUIContent(text)) / distanceBetweenCamAndTarget;

				GUIStyle correctedGUI = new GUIStyle(style);
				correctedGUI.fontSize = (int)labelScaling.y / 3;
				correctedGUI.padding = new RectOffset(0, 0, 0, 0);
				correctedGUI.margin = new RectOffset(0, 0, 0, 0);
				correctedGUI.fixedHeight = labelScaling.y;
				correctedGUI.fixedWidth = labelScaling.x / 1.25f;
				Handles.Label(renderPos, text, correctedGUI);
			}
			else
			{
				Handles.Label(renderPos, text, style);
			}
		}
	}
}
#endif