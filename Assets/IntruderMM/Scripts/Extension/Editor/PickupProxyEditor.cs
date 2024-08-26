/* using UnityEngine;
using UnityEditor;
namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(PickupProxy)), CanEditMultipleObjects]
	public class PickupProxyEditor : UnityEditor.Editor
	{

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		public override void OnInspectorGUI()
		{
			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Pickup Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Item", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupItem"), new GUIContent("Pickup Item", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupType"), new GUIContent("Pickup Type", "How many times the glass needs to get shot to break"));
					
					EditorGUILayout.LabelField("Pickup Options", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("addedAmmo"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("loadedAmmo"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupMessage"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("respawnTime"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("activatorToActivate"), new GUIContent("Health", "How many times the glass needs to get shot to break"));
					EditorGUILayout.EndVertical();
					break;

				case 1:
					Preferences.InspectorGUIPreferences();
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
} */
