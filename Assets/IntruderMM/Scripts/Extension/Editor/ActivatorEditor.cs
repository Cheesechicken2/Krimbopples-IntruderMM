#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(Activator)), CanEditMultipleObjects]
	public class ActivatorEditor : UnityEditor.Editor
	{
		/// <summary>Custom editor target</summary>
		private Activator activatorTarget;

		/// <summary>Current activator tab group</summary>
		private int currentToolbarButton;

		/// <summary>Calls when object is selected with activator componenet on it</summary>
		private void OnEnable()
		{
			activatorTarget = (Activator) target;
		}

		/// <summary>Calls when object is deselected that had an activator componenet on it</summary>
		private void OnDisable()
		{

		}

		/// <summary>InspectorGUI for Activator</summary>
		public override void OnInspectorGUI()
		{
			if (activatorTarget == null) { return; }

			// Toolbar GUI
			currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Trigger", "Actions", "Options", "Prefs" });
			switch (currentToolbarButton)
			{
				case 0:
					// Triggers
					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Triggers", EditorStyles.boldLabel);
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.triggerByUse)), new GUIContent("On Use", "Gets triggered player interacts with it"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.triggerByShoot)), new GUIContent("On Shot", "Gets triggered when an associated collider get shot"));
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.triggerByExplosion)), new GUIContent("On Explosion", "Gets triggered when an associated collider gets hit from a explosion"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.triggerByEnter)), new GUIContent("On Enter", "Gets triggered when a associated trigger collider gets invoked"));
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.triggerByRagdollEnter)), new GUIContent("On Ragdoll Enter", "Gets triggered when a associated trigger collider get invoked from a player ragdolling into it"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.activateAfterTime)), new GUIContent("After Time", "Gets Automatically triggered each round after set time"));
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.HelpBox("Trigger Types, you can use multiple! Some require an associated collider of any sort to correctly invoke", MessageType.Info);
					GUILayout.EndVertical();

					// Trigger Options
					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Trigger Options", EditorStyles.boldLabel);
					EditorGUI.BeginDisabledGroup(!activatorTarget.triggerByExplosion);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.explosionMustBeDirectHit)), new GUIContent("Explosion Must Be Direct Hit", "Only triggers if Explosion was a direct hit. Requires Trigger On Explosion to be true to use"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.explosionTriggerDistance)), new GUIContent("Explosion Trigger Distance", "Max distance for explosion trigger. Requires Trigger On Explosion to be true to use"));
					EditorGUI.EndDisabledGroup();
					EditorGUILayout.Space();
					EditorGUI.BeginDisabledGroup(!activatorTarget.triggerByShoot);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.hp)), new GUIContent("Health", "Everytime the associated collider gets shot the Health will go down by one. Requires Trigger On Shot to be true to use"));
					EditorGUI.EndDisabledGroup();
					EditorGUILayout.HelpBox("Trigger Options, Some require a trigger type to be on to change values", MessageType.Info);
					EditorGUILayout.EndVertical();
					break;

				case 1:
					// Actions
					EditorGUI.indentLevel = 0;
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Activate Events", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.activateEvent)), new GUIContent("Activate Event", "Run these events on activate"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.resetEvent)), new GUIContent("Reset Event", "Run these events on round reset"), true);
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Animation Actions", EditorStyles.boldLabel);
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.objectsToAnimate)), new GUIContent("Objects to Animate", "Animates all objects in list"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.objectsToStop)), new GUIContent("Objects to Stop Animation", "Stops animations on all objects in list"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.animationNames)), new GUIContent("Animation Names", "Animation name to play (index linked)"), true);
					EditorGUILayout.EndVertical();

					EditorGUI.indentLevel = 0;
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Object Actions", EditorStyles.boldLabel);
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.objectsToEnable)), new GUIContent("Objects to Enable", "Enables all objects in list"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.objectsToDisable)), new GUIContent("Objects to Disable", "Disables all objects in list"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.randomObjectsToEnable)), new GUIContent("Random Object to Enable", "Randomly enables object in list"), true);
					EditorGUILayout.EndVertical();

					EditorGUI.indentLevel = 0;
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Door Actions", EditorStyles.boldLabel);
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.doorsToUnlock)), new GUIContent("Doors to Unlock", "Unlocks door"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.doorsToLock)), new GUIContent("Doors to Lock", "Locks door"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.customDoorsToUnlock)), new GUIContent("Custom Doors to Unlock", "Unlocks door"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.customDoorsToLock)), new GUIContent("Custom Doors to Lock", "Locks door"), true);
					EditorGUILayout.EndVertical();

					EditorGUI.indentLevel = 0;
					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Goal Action", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.goal)), new GUIContent("Is Goal", "When activated the round will end"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.goalMessage)), new GUIContent("Goal Message", "Message that gets displayed when round ends from this activator"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.goalWinner)), new GUIContent("Goal Team Winner", "The winning team on round end from goal"), true);
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Send Message Action", EditorStyles.boldLabel);
					EditorGUILayout.LabelField(new GUIContent("Send Message to Player", "Sends a message through the chat box only to the player who invoked the Activator"));
					serializedObject.FindProperty(nameof(Activator.localMessage)).stringValue = EditorGUILayout.TextArea(activatorTarget.localMessage, GUILayout.Height(32));
					EditorGUILayout.LabelField(new GUIContent("Send Message to All Players", "Sends a message through the chat box to all players"));
					serializedObject.FindProperty(nameof(Activator.allMessage)).stringValue = EditorGUILayout.TextArea(activatorTarget.allMessage, GUILayout.Height(32));
					EditorGUILayout.EndVertical();

					EditorGUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Teleport Action", EditorStyles.boldLabel);
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.possibleTeleportDestinations)), new GUIContent("Possible Teleport Locations", "Teleports the player invoker to one of the points in this array"), true);
					EditorGUI.indentLevel = 0;

					EditorGUILayout.EndVertical();

					EditorGUILayout.HelpBox("Actions, What you assign here is what happens when the activator gets triggered", MessageType.Info);
					break;

				case 2:
					// Options
					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Basic Options", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.activatorTeam)), new GUIContent("Activator Team", "Only triggers if Explosion was a direct hit. Requires Trigger On Explosion to be true to use"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.delayTime)), new GUIContent("Delay", "How long (in seconds) until the Activator does it's \"action\" after being activated via use/shot/whatever"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.resetDelay)), new GUIContent("Reset Delay", "How long (in seconds) until the Activator resets on round restart"), true);
					EditorGUILayout.EndVertical();

					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Redo Options", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.canRedo)), new GUIContent("Can Redo", "Can redo the trigger and if given, after a set time."), true);
					EditorGUI.BeginDisabledGroup(!activatorTarget.canRedo);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.redoTime)), new GUIContent("Redo Wait Time", "How long (in seconds) until the activator can be reactivated"), true);
					EditorGUI.EndDisabledGroup();
					EditorGUILayout.EndVertical();

					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Interactable Options", EditorStyles.boldLabel);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.enabledByDefault)), new GUIContent("Visible By Default", "If checked, the object will be visible and interactable. If unchecked, it can be enabled later into a round by another activator"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.enabledAfterTime)), new GUIContent("Visible After Time", "How long (in seconds) until the activator enables itself. Once enabled, it will be visible and interactable"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.allowActivateDelay)), new GUIContent("Interactable After Time", "How long (in seconds) until the activator is interactable after it is reset or made visible."), true);
					EditorGUILayout.EndVertical();
					EditorGUILayout.HelpBox("Options, Options that change the way the activator works", MessageType.Info);

					GUILayout.BeginVertical("Box");
					EditorGUILayout.LabelField("Item Options", EditorStyles.boldLabel);
					EditorGUI.indentLevel = 1;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.neededItems)), new GUIContent("Needed Items", "Needed Items"), true);
					EditorGUI.indentLevel = 0;
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.dontHaveAllItemsMessage)), new GUIContent("Don't have items message", "Don't have items message"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.useUpItems)), new GUIContent("Use up items", "Use up items"), true);
                    EditorGUILayout.EndVertical();

                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Experimental Options", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.localOnly)), new GUIContent("Clientsided Activator / LocalMe", "Display only on your player's screen."), true);
					GUI.enabled = false;
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.disableMeInThisMode)), new GUIContent("DisableMeInThisMode", " Don't Know what this does. "), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Activator.DebugMe)), new GUIContent("Debug Me", "Doesn't Work."), true);
					GUI.enabled = true;
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.HelpBox("DebugMe doesn't work, and I dont know what DisableMe does.", MessageType.Info);
                    break;
				case 3:
					Preferences.InspectorGUIPreferences();
					break;
			}
			serializedObject.ApplyModifiedProperties();
		}

		private void OnSceneGUI()
		{
			RenderSceneGUI(activatorTarget, 1);
		}

		public static void RenderSceneGUI(Activator target, float alpha)
		{
			if (target == null) { return; }
			GUI.color = new Color(1, 1, 1, alpha);
			if (Preferences.drawEnable) EditorTools.DrawLinesToObjects(target.objectsToEnable, "Enable ", Color.green, target, alpha);
			if (Preferences.drawDisable) EditorTools.DrawLinesToObjects(target.objectsToDisable, "Disable ", Color.red, target, alpha);
			if (Preferences.drawRandomObjectsToEnable) EditorTools.DrawLinesToObjects(target.randomObjectsToEnable, "Randomly Enable ", Color.cyan, target, alpha);
			if (Preferences.drawTeleportLocations) EditorTools.DrawLinesToObjects(target.possibleTeleportDestinations, "Teleport Destination ", Color.magenta, target, alpha);
			if (Preferences.drawCustomDoorsLock) EditorTools.DrawLinesToObjects(target.customDoorsToLock, "Lock ", Color.yellow, target, alpha);
			if (Preferences.drawCustomDoorsToUnlock) EditorTools.DrawLinesToObjects(target.customDoorsToUnlock, "Unlock ", Color.blue, target, alpha);
			if (Preferences.drawCustomDoorsLock) EditorTools.DrawLinesToObjects(target.doorsToLock, "Lock ", Color.yellow, target, alpha);
			if (Preferences.drawCustomDoorsToUnlock) EditorTools.DrawLinesToObjects(target.doorsToUnlock, "Unlock ", Color.blue, target, alpha);
			if (Preferences.drawToAnimate) EditorTools.DrawLinesToObjects(target.objectsToAnimate, "Animate ", Color.blue, target, alpha);
			if (Preferences.drawToStopAnimate) EditorTools.DrawLinesToObjects(target.objectsToStop, "Stop Animation ", Color.yellow, target, alpha);
		}
	}
}

#endif
