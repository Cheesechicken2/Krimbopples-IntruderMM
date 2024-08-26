#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(NoteProxy)), CanEditMultipleObjects]
	public class NoteProxyEditor : UnityEditor.Editor
	{
		NoteProxy noteTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		private void OnEnable()
		{
			noteTarget = (NoteProxy)target;
		}

		public override void OnInspectorGUI()
		{
			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Note Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.PropertyField(serializedObject.FindProperty("message"), new GUIContent("Note Message", "The awesome string that gets displayed when using this note"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("activatorToActivate"), new GUIContent("Activate Activator", "Activates the activator when a player starts to read this note"));
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
			Handles.lighting = true;

			if (noteTarget.activatorToActivate == null) { return; }
			EditorTools.RenderPoints(noteTarget.gameObject, noteTarget.activatorToActivate.gameObject, Color.cyan, "Activate Activator on  ", 1f);

			if (!Preferences.renderNestedActivator) { return; }
			ActivatorEditor.RenderSceneGUI(noteTarget.activatorToActivate, EditorTools.SUB_ACTIVATOR_ALPHA);
		}
	}
}

#endif