#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorNote : MonoBehaviour
{
	public string note;
	public string warning;
	public string error;
}

[CustomEditor(typeof(EditorNote))]
public class EditorNoteEditor : Editor
{
	EditorNote _target;
	private void OnEnable()
	{
		_target = (EditorNote)target;
	}
	public override void OnInspectorGUI()
	{
		if (!string.IsNullOrEmpty(_target.note)) EditorGUILayout.HelpBox(_target.note, MessageType.Info);
		if (!string.IsNullOrEmpty(_target.warning)) EditorGUILayout.HelpBox(_target.warning, MessageType.Warning);
		if (!string.IsNullOrEmpty(_target.error)) EditorGUILayout.HelpBox(_target.error, MessageType.Error);
		EditorGUILayout.HelpBox("Be sure to visit the Superboss Games Discord server for any help!", MessageType.None);
	}
}
#endif