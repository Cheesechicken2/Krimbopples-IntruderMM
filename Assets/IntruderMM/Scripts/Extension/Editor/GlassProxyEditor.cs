#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(GlassProxy)), CanEditMultipleObjects]
	public class GlassProxyEditor : UnityEditor.Editor
	{
		// GlassProxy glassTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		// private void OnEnable()
		// {
		// 	glassTarget = (GlassProxy)target;
		// }

		public override void OnInspectorGUI()
		{
			currentToolbarButton = 0;//GUILayout.Toolbar(currentToolbarButton, new string[] { "Glass Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Glass Proxy", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("hp"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("showBrokenEdgeShards"), new GUIContent("Show Broken Edge Shards", "Show the edge shards after breaking?"));
					EditorGUILayout.EndVertical();
					break;

				case 1:
					Preferences.InspectorGUIPreferences();
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}

		// private void OnSceneGUI()
		// {
		// 	if (Preferences.renderArrow) Handles.ArrowHandleCap(0, glassTarget.transform.position, glassTarget.transform.rotation * Quaternion.Euler(0, 0, 90), 0.5f, EventType.Repaint);
		// 	if (Preferences.renderArrow) Handles.ArrowHandleCap(0, glassTarget.transform.position, glassTarget.transform.rotation * Quaternion.Euler(180, 0, -90), 0.5f, EventType.Repaint);
		// 	EditorTools.StaticRenderWorldGUI("Glass Direction ", glassTarget.transform.position, EditorTools.labelSceneGUI, 1);

		// }
	}
}

#endif