#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(ZiplineProxy)), CanEditMultipleObjects]
	public class ZiplineEditor : UnityEditor.Editor
	{
		ZiplineProxy ziplineTarget;
		Color handleColor;


		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		private void OnEnable()
		{
			ziplineTarget = (ZiplineProxy)target;
		}

		public override void OnInspectorGUI()
		{

			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Zipline Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					if (ziplineTarget.startPoint == null || ziplineTarget.endPoint == null) { EditorGUILayout.HelpBox("Please assign both start and end points", MessageType.Error); }
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Start and End Points", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("startPoint"), new GUIContent("Zipline Start Point", "The Start of the zipline"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("endPoint"), new GUIContent("Zipline End Point", "The end of the zipline"));
					}
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("zipSpeed"), new GUIContent("Zipline Travel Speed", "How fast you travel down the zipline"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfVertices"), new GUIContent("Number of Vertices", "The ammount of vertices that gets generated on the rope, the more vertices the more dangly you can have the zipline but with higher performance cost"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("maxGravityDangle"), new GUIContent("Max Gravity Dangle", "How much the zipline dips"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("forceDismountAtPercent"), new GUIContent("Force Dismount", "how fast until you slip and die"));
                    }
					EditorGUILayout.EndVertical();
					break;

				case 1:
					Preferences.InspectorGUIPreferences();
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void OnSceneGUI()
		{
			Tools.current = Tool.None;

			if (ziplineTarget.startPoint == null || ziplineTarget.endPoint == null) { return; }

			EditorTools.RenderPoints(ziplineTarget.startPoint, ziplineTarget.endPoint, Color.green, "Zipline ", 1);

			EditorGUI.BeginChangeCheck();
			Vector3 startPosNewMovePos = Handles.PositionHandle(ziplineTarget.startPoint.transform.position, ziplineTarget.startPoint.transform.rotation);
			Vector3 endtPosNewMovePos = Handles.PositionHandle(ziplineTarget.endPoint.transform.position, ziplineTarget.endPoint.transform.rotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(ziplineTarget, "Changed Zipline Transform");
				ziplineTarget.startPoint.transform.position = startPosNewMovePos;
				ziplineTarget.endPoint.transform.position = endtPosNewMovePos;
			}
		}
	}
}

#endif