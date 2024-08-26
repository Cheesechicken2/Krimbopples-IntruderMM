using UnityEngine;
using UnityEditor;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(CompassDirection))]
	public class CompassDirectionEditor : UnityEditor.Editor
	{

		CompassDirection compassDirectionTarget;

		private void OnEnable()
		{
			compassDirectionTarget = (CompassDirection)target;
		}
		public override void OnInspectorGUI()
		{
			EditorGUILayout.HelpBox("Z Axis = North", MessageType.None);
		}

		private void OnSceneGUI()
		{
			Handles.ArrowHandleCap(0, compassDirectionTarget.transform.position, Quaternion.Euler(compassDirectionTarget.transform.localRotation.eulerAngles), 1, EventType.Repaint);
		}
	}
}
