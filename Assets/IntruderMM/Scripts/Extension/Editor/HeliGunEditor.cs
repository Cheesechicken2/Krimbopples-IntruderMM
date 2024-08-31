#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor
{
    [CustomEditor(typeof(HeliGun)), CanEditMultipleObjects]
    public class HeliGunEditor : UnityEditor.Editor
    {
        /// <summary>Custom editor target</summary>
        private HeliGun heliGunTarget;

        /// <summary>Current heliGun tab group</summary>
        private int currentToolbarButton;

        /// <summary>Calls when object is selected with heliGun component on it</summary>
        private void OnEnable()
        {
            heliGunTarget = (HeliGun)target;
        }

        /// <summary>InspectorGUI for HeliGun</summary>
        public override void OnInspectorGUI()
        {
            if (heliGunTarget == null) { return; }

            // Toolbar GUI
            currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "Settings", "Shooting", "Targeting" });
            switch (currentToolbarButton)
            {
                case 0:
                    // Settings
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.on)), new GUIContent("On", "Is the HeliGun active?"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.checkInterval)), new GUIContent("Check Interval", "Time interval (in seconds) between target checks"));
                    EditorGUILayout.EndVertical();
                    break;

                case 1:
                    // Shooting
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Shooting", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.shootTransform)), new GUIContent("Shoot Transform", "Transform from where the gun will shoot"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.shouldShootMask)), new GUIContent("Target Layer Mask", "Layers that the HeliGun should shoot at"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.spawnProjectile)), new GUIContent("Spawn Projectile", "The stuff that should be controlling shooting out??"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.sightSensor)), new GUIContent("Sight Sensor", "How the gun sees"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.shootIntruders)), new GUIContent("Shoot Intruders", "Should the HeliGun shoot intruders?"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.shootGuards)), new GUIContent("Shoot Guards", "Should the HeliGun shoot guards?"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.shotsPerSalvo)), new GUIContent("Shots Per Salvo", "Number of shots fired per salvo"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.fireRate)), new GUIContent("Fire Rate", "Time between shots within a salvo (in seconds)"));

                    EditorGUILayout.HelpBox("Fire rate should always be never more than 2! The lower the better!", MessageType.Info);
                    EditorGUILayout.EndVertical();
                    break;

                case 2:
                    // Targeting
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Targeting", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(HeliGun.targetingRotationSpeed)), new GUIContent("Targeting Rotation Speed", "Speed at which the HeliGun rotates to target"));
                    EditorGUILayout.EndVertical();
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            RenderSceneGUI(heliGunTarget, 1);
        }

        public static void RenderSceneGUI(HeliGun target, float alpha)
        {
            if (target == null) { return; }
        }
    }
}

#endif
