#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(ActivateActivator)), CanEditMultipleObjects]
	public class ActivateActivatorEditor : UnityEditor.Editor
	{
		ActivateActivator activateActivatorTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		private void OnEnable()
		{
			activateActivatorTarget = (ActivateActivator)target;
		}

		public override void OnInspectorGUI()
		{

			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Activate Activator", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("activators"), new GUIContent("Activators", "What the variable says"), true);
						EditorGUI.indentLevel--;

						if (activateActivatorTarget.activators == null || activateActivatorTarget.activators.Length == 0 || activateActivatorTarget.activators[0] == null) { EditorGUILayout.HelpBox("Make sure to add some activators to activate", MessageType.Warning); }
						EditorGUILayout.HelpBox("You can use this script with Unity Events, use the ActivateAll() method or ActivateIndex(int index) method to activate the activators!", MessageType.Info);
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
			Handles.SphereHandleCap(0, activateActivatorTarget.transform.position, activateActivatorTarget.transform.rotation, 0.125f, EventType.Repaint);

			if (activateActivatorTarget.activators == null || activateActivatorTarget.activators.Length == 0) { return; }
			EditorTools.DrawLinesToObjects(activateActivatorTarget.activators, "Activate Activator on ", Color.cyan, activateActivatorTarget, 1);

			if (!Preferences.renderNestedActivator) { return; }
			foreach (Activator item in activateActivatorTarget.activators)
			{
				ActivatorEditor.RenderSceneGUI(item, EditorTools.SUB_ACTIVATOR_ALPHA);
			}
		}
	}
}

#endif