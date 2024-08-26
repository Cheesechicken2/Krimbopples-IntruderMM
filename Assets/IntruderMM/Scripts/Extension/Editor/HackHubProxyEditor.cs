#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(HackHubProxy)), CanEditMultipleObjects]
	public class HackHubProxyEditor : UnityEditor.Editor
	{
		HackHubProxy hackHubTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		private void OnEnable()
		{
			hackHubTarget = (HackHubProxy) target;
		}

		public override void OnInspectorGUI()
		{

			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Hack Hub Proxy", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Hack Hub Proxy", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.hackGoal)), new GUIContent("Hack Goal", "How many times this hub needs to be hacked"), true);
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.nodes)), new GUIContent("Nodes", "How many times this hub needs to be hacked"), true);
						EditorGUI.indentLevel--;
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.autoGrabNodesFromChildren)), new GUIContent("Auto Grab Nodes from Children", "Automatically grab nodes from children"), true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.autoNameChildNodes)), new GUIContent("Auto Name Child Nodes", "Automatically name child nodes"), true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.baseNodeName)), new GUIContent("Base Node Name", "The name the hub has"), true);
					}
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					{
						EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.hackCompleteActivator)), new GUIContent("Hack Complete Activator", "Gets called when the hack hub has been hacked (Gone Green)"), true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.hackCompleteEvent)), new GUIContent("Hack Complete Event", "Gets called when the hack hub has been hacked (Gone Green)"), true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.roundResetEvent)), new GUIContent("Round Reset Event", "Gets called on round reset"), true);
						EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HackHubProxy.showHackCompleteTransmitWarning)), new GUIContent("Hack Complete Transmit Warning", "Shows transmitting light when hacking and there is a hack complete event"), true);
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
			if (!hackHubTarget.autoGrabNodesFromChildren) EditorTools.DrawLinesToObjects(hackHubTarget.nodes, "Terminal ", Color.cyan, hackHubTarget, 1f);
			else EditorTools.DrawLinesToObjects(hackHubTarget.GetComponentsInChildren<HackNodeProxy>(), "Terminal ", Color.cyan, hackHubTarget, 1f);
		}
	}
}

#endif
