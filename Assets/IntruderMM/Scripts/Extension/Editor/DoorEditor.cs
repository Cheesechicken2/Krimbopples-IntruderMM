#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(CustomDoorProxy)), CanEditMultipleObjects]
	public class DoorEditor : UnityEditor.Editor
	{
		CustomDoorProxy doorTarget;
		Color lineColor;
		Vector3 hingeLocalPos;

		bool cachedTransform = false;

		enum DoorType { Hinged, Sliding }
		enum LockType { RandomlyLock, AlwaysLocked, AlwaysUnlocked }

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		DoorType doorType;
		LockType lockType;

		private void OnEnable()
		{
			doorTarget = (CustomDoorProxy) target;
			if (doorTarget.doorHinge != null)
			{
				hingeLocalPos = doorTarget.doorHinge.transform.localPosition;
				cachedTransform = true;
			}

			doorType = doorTarget.slidingDoor ? DoorType.Sliding : DoorType.Hinged;
			EditorSceneManager.sceneSaving += OnSceneSaving;

			if (doorTarget == null) { return; }

			// Lock type setting
			if (doorTarget.alwaysLock && !doorTarget.neverLock)
			{
				lockType = LockType.AlwaysLocked;
			}
			else if (doorTarget.neverLock && !doorTarget.alwaysLock)
			{
				lockType = LockType.AlwaysUnlocked;
			}
			else if (!doorTarget.alwaysLock && !doorTarget.neverLock)
			{
				lockType = LockType.RandomlyLock;
			}
		}

		private void OnDisable()
		{
			if (doorTarget == null) return;
			ResetTransforms();

			EditorSceneManager.sceneSaving -= OnSceneSaving;
		}

		private void OnSceneSaving(Scene scene, string path)
		{
			ResetTransforms();
		}

		// Scene and string is for the events
		private void ResetTransforms()
		{
			if (doorTarget == null || doorTarget.doorHinge == null) { return; }
			doorTarget.doorHinge.transform.localRotation = Quaternion.Euler(Vector3.zero);

			if(doorTarget.slidingDoor) doorTarget.doorHinge.transform.localPosition = Vector3.zero;
		}

		public override void OnInspectorGUI()
		{
			// Toolbar GUI
			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Door Proxy", "Prefs" });

			switch (currentToolbarButton)
			{
				case 0:
					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Door Options", EditorStyles.boldLabel);
					doorType = (DoorType) EditorGUILayout.EnumPopup("Door Type", doorType);
					EditorGUILayout.Space();
					EditorGUILayout.PropertyField(serializedObject.FindProperty("doorHinge"), new GUIContent("Door Hinge", "The hinge of the door, (is basically just the pivot point!"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("partnerDoor"), new GUIContent("Partner Door", "Used for double doors"));
					GUILayout.EndVertical();

					if (doorType == DoorType.Hinged)
					{
						serializedObject.FindProperty("slidingDoor").boolValue = false;
						GUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Hinged Options", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("reverse"), new GUIContent("Invert Rotation", "Inverts the max door angle"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("maxDoorAngle"), new GUIContent("Max Door Angle", "The max angle the door can rotate to"));
						serializedObject.FindProperty("maxDoorAngle").intValue = EditorGUILayout.IntSlider(doorTarget.maxDoorAngle, 0, 270);
						EditorGUILayout.PropertyField(serializedObject.FindProperty("startOpenPercent"), new GUIContent("Start Open Percent", "Open percentage at the start of round"));
						serializedObject.FindProperty("startOpenPercent").floatValue = EditorGUILayout.Slider(doorTarget.startOpenPercent, 0.0f, 1.0f);
						GUILayout.EndVertical();
					}
					else
					{
						serializedObject.FindProperty("slidingDoor").boolValue = true;
						GUILayout.BeginVertical("Box");
						EditorGUILayout.LabelField("Sliding Options", EditorStyles.boldLabel);
						//EditorGUILayout.PropertyField(serializedObject.FindProperty("slidingDoor"), new GUIContent("Use Sliding Door", "Changes the door to a sliding door"));
						EditorGUILayout.PropertyField(serializedObject.FindProperty("slideDistance"), new GUIContent("Sliding Distance", "Max distance at which the sliding door can go to"));
						EditorGUILayout.HelpBox("The pivot point on the hinge for sliding doors must always be 0, 0, 0 for sliding doors to correctly work", MessageType.Info);
						GUILayout.EndVertical();
					}

					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Lock Options", EditorStyles.boldLabel);
					lockType = (LockType) EditorGUILayout.EnumPopup("Lock Type", lockType);

					// Lock type setting
					if (lockType == LockType.AlwaysLocked) { serializedObject.FindProperty("alwaysLock").boolValue = true; serializedObject.FindProperty("neverLock").boolValue = false; }
					else if (lockType == LockType.AlwaysUnlocked) { serializedObject.FindProperty("alwaysLock").boolValue = false; serializedObject.FindProperty("neverLock").boolValue = true; }
					else { serializedObject.FindProperty("alwaysLock").boolValue = false; serializedObject.FindProperty("neverLock").boolValue = false; }

					EditorGUILayout.PropertyField(serializedObject.FindProperty("canLockPick"), new GUIContent("Can Lock Pick", "Does what it says"));
					GUILayout.EndVertical();

					// Door Options
					EditorGUILayout.BeginVertical("box");
					{
						Preferences.showDoorPath = EditorGUILayout.Toggle("Show Door Path? ", Preferences.showDoorPath);
					}
					EditorGUILayout.EndVertical();

					if (doorTarget.doorHinge == null) EditorGUILayout.HelpBox("Please assign a door hinge", MessageType.Error);
					break;

				case 1:
					Preferences.InspectorGUIPreferences();
					break;
			}

			serializedObject.ApplyModifiedProperties();
		}

		GameObject oldHinge;

		private void OnSceneGUI()
		{
			lineColor = Color.white;

			if (doorTarget.doorHinge == null && oldHinge != null)
			{
				oldHinge.transform.localPosition = hingeLocalPos;
				oldHinge.transform.localRotation = Quaternion.Euler(0, 0, 0);
				return;
			}

			if (doorTarget.doorHinge != null && cachedTransform == false)
			{
				hingeLocalPos = doorTarget.doorHinge.transform.localPosition;
			}

			if (doorTarget.doorHinge == null) { return; }

			if (oldHinge != doorTarget.doorHinge) { oldHinge = doorTarget.doorHinge; }

			if (Preferences.renderBall)
			{
				lineColor.a = Preferences.alphaBall;
				Handles.color = lineColor;
				Handles.SphereHandleCap(0, doorTarget.doorHinge.transform.position, doorTarget.doorHinge.transform.rotation, 0.0625f, EventType.Repaint);
			}

			Vector3 target = doorTarget.transform.position;
			EditorTools.StaticRenderWorldGUI(doorTarget.slidingDoor ? ("Door Slide Distance " + doorTarget.slideDistance) : ("Door Max Angle " + doorTarget.maxDoorAngle), target + new Vector3(0, 1, 0), EditorTools.labelSceneGUI, 1);
			EditorTools.StaticRenderWorldGUI("Door Hinge Point", target, EditorTools.labelSceneGUI, 1);

			if (Preferences.showDoorPath)
			{
				if (Preferences.renderLines)
				{
					lineColor.a = Preferences.alphaLines;
					Handles.color = lineColor;

					// If not Sliding Door
					if (!doorTarget.slidingDoor)
					{
						for (int i = 0; i < 8; i++)
						{
							Handles.DrawWireArc(new Vector3(doorTarget.doorHinge.transform.position.x, doorTarget.transform.position.y, doorTarget.doorHinge.transform.position.z), doorTarget.transform.up, doorTarget.transform.right, doorTarget.maxDoorAngle * (!doorTarget.reverse ? 1 : -1), 0.625f / 5 * i);
						}
						Handles.color = new Color(1, 0, 0, 0.125f);
						Handles.DrawSolidArc(new Vector3(doorTarget.doorHinge.transform.position.x, doorTarget.transform.position.y, doorTarget.doorHinge.transform.position.z), doorTarget.transform.up, doorTarget.transform.right, doorTarget.maxDoorAngle * (!doorTarget.reverse ? 1 : -1), 1f);
					}
					// If sliding door
					else
					{
						Handles.DrawLine(doorTarget.transform.position, doorTarget.doorHinge.transform.position);
					}
				}

				if (!doorTarget.slidingDoor)
				{
					if (doorTarget.doorHinge.transform.localPosition != hingeLocalPos) doorTarget.doorHinge.transform.localPosition = hingeLocalPos;
					doorTarget.doorHinge.transform.localRotation = Quaternion.Euler(0, doorTarget.maxDoorAngle * (!doorTarget.reverse ? 1 : -1), 0);
				}
				else
				{
					if (doorTarget.doorHinge.transform.localRotation != Quaternion.Euler(0, 0, 0)) doorTarget.doorHinge.transform.localRotation = Quaternion.Euler(0, 0, 0);
					doorTarget.doorHinge.transform.localPosition = doorTarget.slideDistance;
				}
			}
			else
			{
				ResetTransforms();
			}
		}
	}
}

#endif