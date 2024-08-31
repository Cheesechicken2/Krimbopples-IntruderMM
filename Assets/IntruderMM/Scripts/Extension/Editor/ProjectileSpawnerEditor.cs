#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Assets.IntruderMM.Editor
{
    [CustomEditor(typeof(SpawnProjectile)), CanEditMultipleObjects]
    public class SpawnProjectileEditor : UnityEditor.Editor
    {
        /// <summary>Custom editor target</summary>
        private SpawnProjectile spawnProjectileTarget;

        /// <summary>Serialized properties for SpawnProjectile</summary>
        private SerializedProperty projectileNameProp;
        private SerializedProperty projProp;
        private SerializedProperty speedProp;
        private SerializedProperty playSoundProp;
        private SerializedProperty spawnSoundProp;
        private SerializedProperty fireRateProp;
        private SerializedProperty canFireProp;
        private SerializedProperty autoAddPhotonViewProp;
        private SerializedProperty myTransformProp;
        private SerializedProperty myAudioProp;
        private SerializedProperty currentAmmoProp;
        private SerializedProperty defaultAmmoProp;
        private SerializedProperty animationProp;
        private SerializedProperty forwardOffsetProp;
        private SerializedProperty canHitOwnerProp;
        private SerializedProperty useLocalPosProp;

        /// <summary>Current SpawnProjectile tab group</summary>
        private int currentToolbarButton;

        // List of projectile names
        private readonly string[] projectileNames = new string[]
        {
            "BananaPeel",
            "BananaRifleBullet",
            "BloonDrone",
            "BloonGun",
            "BoxingGlovePunch",
            "ChairProjectile",
            "CSGrenade",
            "Decoy",
            "DecoyCrouch",
            "ExplosionDamage",
            "FlubberProjectile",
            "Grenade",
            "GrenadeShortFuse",
            "Headshot",
            "HeadshotPistol",
            "HeadshotShotgun",
            "HeadshotSniper",
            "LaserSensor",
            "Mortar",
            "MortarExplosionDamage",
            "Pistol2Bullet",
            "PistolBullet",
            "RemoteCharge",
            "Rocket",
            "RocketDamage",
            "ShotgunBullet",
            "ShotgunPellet",
            "ShotgunPelletHeadshot",
            "ShotgunPelletsMulti",
            "SMGBullet",
            "SmokeGrenade",
            "SniperBullet",
            "Snowball",
            "SnowballExplosionDamage"
        };

        private int selectedProjectileIndex;

        /// <summary>Called when the editor is enabled</summary>
        private void OnEnable()
        {
            spawnProjectileTarget = (SpawnProjectile)target;

            // Cache serialized properties
            projectileNameProp = serializedObject.FindProperty(nameof(SpawnProjectile.projectileName));
            projProp = serializedObject.FindProperty(nameof(SpawnProjectile.proj));
            speedProp = serializedObject.FindProperty(nameof(SpawnProjectile.speed));
            playSoundProp = serializedObject.FindProperty(nameof(SpawnProjectile.playSound));
            spawnSoundProp = serializedObject.FindProperty(nameof(SpawnProjectile.spawnSound));
            fireRateProp = serializedObject.FindProperty(nameof(SpawnProjectile.fireRate));
            canFireProp = serializedObject.FindProperty(nameof(SpawnProjectile.canFire));
            autoAddPhotonViewProp = serializedObject.FindProperty(nameof(SpawnProjectile.autoAddPhotonView));
            myTransformProp = serializedObject.FindProperty(nameof(SpawnProjectile.myTransform));
            myAudioProp = serializedObject.FindProperty(nameof(SpawnProjectile.myAudio));
            currentAmmoProp = serializedObject.FindProperty(nameof(SpawnProjectile.currentAmmo));
            defaultAmmoProp = serializedObject.FindProperty(nameof(SpawnProjectile.defaultAmmo));
            animationProp = serializedObject.FindProperty(nameof(SpawnProjectile.animation));
            forwardOffsetProp = serializedObject.FindProperty(nameof(SpawnProjectile.forwardOffset));
            canHitOwnerProp = serializedObject.FindProperty(nameof(SpawnProjectile.canHitOwner));
            useLocalPosProp = serializedObject.FindProperty(nameof(SpawnProjectile.useLocalPos));

            // Initialize the selected index
            selectedProjectileIndex = Mathf.Max(0, System.Array.IndexOf(projectileNames, spawnProjectileTarget.projectileName));
        }

        /// <summary>InspectorGUI for SpawnProjectile</summary>
        public override void OnInspectorGUI()
        {
            if (spawnProjectileTarget == null) { return; }

            serializedObject.Update();

            // Toolbar GUI
            currentToolbarButton = GUILayout.Toolbar(currentToolbarButton, new string[] { "General", "Sound", "Ammo", "Transform" });
            switch (currentToolbarButton)
            {
                case 0:
                    // General Tab
                    DrawGeneralTab();
                    break;

                case 1:
                    // Sound Tab
                    DrawSoundTab();
                    break;

                case 2:
                    // Ammo Tab
                    DrawAmmoTab();
                    break;

                case 3:
                    // Transform Tab
                    DrawTransformTab();
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the General tab in the inspector</summary>
        private void DrawGeneralTab()
        {
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);

            // Dropdown for projectile name
            selectedProjectileIndex = EditorGUILayout.Popup("Projectile Name", selectedProjectileIndex, projectileNames);
            projectileNameProp.stringValue = projectileNames[selectedProjectileIndex];

            EditorGUILayout.PropertyField(speedProp, new GUIContent("Speed", "Speed at which the projectile will be launched"));
            EditorGUILayout.PropertyField(canFireProp, new GUIContent("Can Fire", "Is the projectile allowed to be fired?"));
            EditorGUILayout.HelpBox("Fire rate should always be never more than 2! The lower the better! (again)", MessageType.Info);
            EditorGUILayout.PropertyField(fireRateProp, new GUIContent("Fire Rate", "Rate of fire (in seconds) between each shot"));
            EditorGUILayout.PropertyField(autoAddPhotonViewProp, new GUIContent("Auto Add Photon View", "Automatically add PhotonView if missing"));
            EditorGUILayout.PropertyField(forwardOffsetProp, new GUIContent("Forward Offset", "Offset of the projectile from the spawn point"));
            EditorGUILayout.PropertyField(canHitOwnerProp, new GUIContent("Can Hit Owner", "Can the projectile hit its owner?"));
            EditorGUILayout.PropertyField(useLocalPosProp, new GUIContent("Use Local Position", "Should the projectile be spawned at local position?"));

            GUILayout.EndVertical();
        }

        /// <summary>Draws the Sound tab in the inspector</summary>
        private void DrawSoundTab()
        {
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Sound Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(playSoundProp, new GUIContent("Play Sound", "Should the spawn sound be played?"));
            EditorGUILayout.PropertyField(spawnSoundProp, new GUIContent("Spawn Sound", "Sound to be played when the projectile is spawned"));
            EditorGUILayout.PropertyField(myAudioProp, new GUIContent("Audio Source", "AudioSource component for playing sounds"));

            GUILayout.EndVertical();
        }

        /// <summary>Draws the Ammo tab in the inspector</summary>
        private void DrawAmmoTab()
        {
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Ammo Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(currentAmmoProp, new GUIContent("Current Ammo", "Current ammunition count"));
            EditorGUILayout.PropertyField(defaultAmmoProp, new GUIContent("Default Ammo", "Default ammunition count (use -1 for infinite)"));

            GUILayout.EndVertical();
        }

        /// <summary>Draws the Transform tab in the inspector</summary>
        private void DrawTransformTab()
        {
            GUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Junk Settings??", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(myTransformProp, new GUIContent("Transform", "Transform from where the projectile will be spawned"));
            EditorGUILayout.PropertyField(animationProp, new GUIContent("Animation", "Animation associated with the projectile spawn"));

            GUILayout.EndVertical();
        }
    }
}

#endif
