#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(KeypadProxy)), CanEditMultipleObjects]
	public class KeypadProxyEditor : UnityEditor.Editor
	{

		KeypadProxy keypadTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		private void OnEnable()
		{
			keypadTarget = (KeypadProxy)target;
		}
		public override void OnInspectorGUI()
		{
			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Keypad Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Note Options", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("myNote"), new GUIContent("Keypad Passcode Note", "The note the passcode for this keypad gets shown on. On the note use {0} to signifiy where the keycode is on that note"));
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(serializedObject.FindProperty("otherNotes"), new GUIContent("Keypad Passcode Notes", "Same as the Keypad Passcode Note variable"), true);
						EditorGUI.indentLevel--;
					}
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Activator", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("myActivator"), new GUIContent("Activate Activator", "Activates the activator on keypad unlock"));
						EditorGUILayout.HelpBox("Its best just to use an activator for unlocking stuff", MessageType.None);
					}
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Unlock Options", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("myDoorProxy"), new GUIContent("Unlock Door", "Unlocks door on keypad unlock"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("myCustomDoor"), new GUIContent("Unlock Custom Door", "Unlocks custom door on keypad unlock"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("lockedObject"), new GUIContent("Unlock GameObject", "Unlocks GameObject on keypad unlock"));
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
			// Draws sphere at activator base
			Handles.SphereHandleCap(0, keypadTarget.transform.position, keypadTarget.transform.rotation, 0.125f, EventType.Repaint);

			if (keypadTarget.myCustomDoor != null)
				EditorTools.RenderPoints(keypadTarget.gameObject, keypadTarget.myCustomDoor.gameObject, Color.green, "Unlocks ", 1);

			if (keypadTarget.myDoorProxy != null)
				EditorTools.RenderPoints(keypadTarget.gameObject, keypadTarget.myDoorProxy.gameObject, Color.green, "Unlocks ", 1);

			if (keypadTarget.lockedObject != null)
				EditorTools.RenderPoints(keypadTarget.gameObject, keypadTarget.lockedObject.gameObject, Color.green, "Unlocks ", 1);

			if (keypadTarget.myNote != null)
				EditorTools.RenderPoints(keypadTarget.gameObject, keypadTarget.myNote.gameObject, Color.cyan, "Keypad Passcode Text on ", 1);

			if (keypadTarget.myActivator != null)
			{
				EditorTools.RenderPoints(keypadTarget.gameObject, keypadTarget.myActivator.gameObject, Color.magenta, "Activates ", 1f);
				if (Preferences.renderNestedActivator)
				{
					ActivatorEditor.RenderSceneGUI(keypadTarget.myActivator, EditorTools.SUB_ACTIVATOR_ALPHA);
				}
			}

			EditorTools.DrawLinesToObjects(keypadTarget.otherNotes, "Keypad Passcode Text on ", Color.cyan, keypadTarget, 1);
		}
	}

}

#endif