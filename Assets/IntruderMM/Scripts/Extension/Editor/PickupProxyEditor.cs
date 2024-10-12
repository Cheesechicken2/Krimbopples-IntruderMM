using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PickupProxy))]
public class PickupProxyEditor : Editor
{
    private GUIStyle buttonStyle;
    private GUIStyle boxStyle;
    private GUIStyle labelStyle;
    private GUIStyle textAreaStyle;
    private GUIStyle headerStyle;
    private GUIStyle selectedLabelStyle;  // New style for the selected type label

    private Vector2 scrollPosition;  // For scrolling
    private Font customFont;

    private Dictionary<PickupType, ItemProxy> pickupTypeToProxyMap; // Maps PickupType to ItemProxy

    private void SetStyles()
    {
        if (buttonStyle == null)
        {
            // Load textures
            Texture2D buttonTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/button.png", typeof(Texture2D));
            Texture2D buttonHoverTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/buttonHover.png", typeof(Texture2D));
            Texture2D buttonSelectTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/buttonSelect.png", typeof(Texture2D));
            Texture2D boxTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/box.png", typeof(Texture2D));

            // Load custom font
            customFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/IntruderMM/Scripts/Extension/Editor/GUI/Font/ShareTechMono-Regular.ttf");

            // Button style
            buttonStyle = new GUIStyle(GUI.skin.button)
            {
                normal = { background = buttonTexture },
                hover = { background = buttonHoverTexture },
                active = { background = buttonSelectTexture },
                fontSize = 14,
                font = customFont,
                alignment = TextAnchor.MiddleCenter,
                fixedHeight = 40,
                fixedWidth = 200
            };

            // Box style
            boxStyle = new GUIStyle(EditorStyles.helpBox)
            {
                normal = { background = boxTexture },
                padding = new RectOffset(10, 10, 10, 10),
                margin = new RectOffset(0, 0, 10, 10)
            };

            // Label style
            labelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                font = customFont,
                alignment = TextAnchor.MiddleLeft
            };

            // TextArea style
            textAreaStyle = new GUIStyle(EditorStyles.textArea)
            {
                fontSize = 14,
                font = customFont,
                wordWrap = true
            };

            // Header style
            headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                font = customFont,
                alignment = TextAnchor.MiddleCenter
            };


            selectedLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                font = customFont,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.green }  
            };
        }
    }

    private void OnEnable()
    {

        InitializePickupTypeToProxyMap();
    }

    private void InitializePickupTypeToProxyMap()
    {
        pickupTypeToProxyMap = new Dictionary<PickupType, ItemProxy>
        {
            { PickupType.SniperRifle, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SniperRifleProxy.asset", typeof(ItemProxy)) },
            { PickupType.SniperAmmox5, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SniperRifleAmmoProxy.asset", typeof(ItemProxy)) },
            { PickupType.Banana, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BananaProxy.asset", typeof(ItemProxy)) },
            { PickupType.RedDot, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/RedDotProxy.asset", typeof(ItemProxy)) },
            { PickupType.SMG, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SMGProxy.asset", typeof(ItemProxy)) },
            { PickupType.SMGAmmox30, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SMGAmmoProxy.asset", typeof(ItemProxy)) },
            { PickupType.BoxingGloves, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BoxingGlovesProxy.asset", typeof(ItemProxy)) },
            { PickupType.Shield, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/ShieldProxy.asset", typeof(ItemProxy)) },
            { PickupType.LaserSensor, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/LaserSensorProxy.asset", typeof(ItemProxy)) },
            { PickupType.Shotgun, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/ShotgunProxy.asset", typeof(ItemProxy)) },
            { PickupType.ShotgunAmmox6, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/ShotgunAmmoProxy.asset", typeof(ItemProxy)) },
            { PickupType.Pistol2, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/ShrikePistolProxy.asset", typeof(ItemProxy)) },
            { PickupType.Pistol, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SilencedPistolProxy.asset", typeof(ItemProxy)) },
            { PickupType.PistolAmmox15, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/PistolAmmoProxy.asset", typeof(ItemProxy)) },
            { PickupType.BloonCam, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BloonCamProxy.asset", typeof(ItemProxy)) },
            { PickupType.BloonGun, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BloonGunProxy.asset", typeof(ItemProxy)) },
            { PickupType.Grenade, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/GrenadeProxy.asset", typeof(ItemProxy)) },
            { PickupType.RemoteCharge, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/RemoteChargeProxy.asset", typeof(ItemProxy)) },
            { PickupType.Binoculars, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BinocularsProxy.asset", typeof(ItemProxy)) },
            { PickupType.SmokeGrenade, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SmokeGrenadeProxy.asset", typeof(ItemProxy)) },
            { PickupType.CSGrenade, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/CSGrenadeProxy.asset", typeof(ItemProxy)) },
            { PickupType.CardboardDecoy, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/CutoutProxy.asset", typeof(ItemProxy)) },
            { PickupType.BushCamo, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BushCamoProxy.asset", typeof(ItemProxy)) },
            { PickupType.SnowballLauncher, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SnowballLauncherProxy.asset", typeof(ItemProxy)) },
            { PickupType.BananaRifle, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BananaRifleProxy.asset", typeof(ItemProxy)) },
            { PickupType.BananaRifleAmmo, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/BananaRifleAmmoProxy.asset", typeof(ItemProxy)) },
            { PickupType.Medkit, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/MedkitProxy.asset", typeof(ItemProxy)) },
            { PickupType.SnowballLauncherAmmo, (ItemProxy)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Prefabs/ItemProxies/SnowballLauncherAmmoProxy.asset", typeof(ItemProxy)) },
            // Add others here if i forgot
         };
    }

    public override void OnInspectorGUI()
    {
        SetStyles();
        serializedObject.Update();

        PickupProxy proxy = (PickupProxy)target;

        EditorGUILayout.LabelField($"Selected Type: {proxy.pickupType}", selectedLabelStyle);


        EditorGUILayout.LabelField("Pickup Types", headerStyle);

        // Begin scroll view
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(400));  // Adjust the height to make the list scrollable

        // Display buttons in a grid format
        DisplayPickupTypeGrid(proxy);

        EditorGUILayout.EndScrollView();  // End scroll view

        // Draw other settings in a styled box
        EditorGUILayout.BeginVertical(boxStyle);
        EditorGUILayout.LabelField("Other Settings", headerStyle);

        // Display message about the proxy to replace
        if (pickupTypeToProxyMap.TryGetValue(proxy.pickupType, out ItemProxy proxyItem))
        {
            EditorGUILayout.LabelField($"Psst! Replace this with {proxyItem.name}", labelStyle);
        }
        else
        {
            EditorGUILayout.LabelField("Psst! Replace this with an item from the selected type", labelStyle);
        }

        // Pickup Item Field (manual selection)
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pickupItem"), new GUIContent("Pickup Item"));

        // Pickup Message
        SerializedProperty pickupMessageProp = serializedObject.FindProperty("pickupMessage");
        pickupMessageProp.stringValue = EditorGUILayout.TextArea(pickupMessageProp.stringValue, textAreaStyle, GUILayout.MinHeight(60));

        // Ammo fields
        EditorGUILayout.IntSlider(serializedObject.FindProperty("addedAmmo"), -1, 100, "Added Ammo");
        EditorGUILayout.IntSlider(serializedObject.FindProperty("loadedAmmo"), -1, 100, "Loaded Ammo");

        // Respawn Time
        SerializedProperty respawnTimeProp = serializedObject.FindProperty("respawnTime");
        respawnTimeProp.floatValue = EditorGUILayout.Slider("Respawn Time", respawnTimeProp.floatValue, -1f, 120f);

        // Activator activation
        EditorGUILayout.PropertyField(serializedObject.FindProperty("activatorToActivate"), new GUIContent("Activator To Activate"));

        // Teams allowed
        EditorGUILayout.PropertyField(serializedObject.FindProperty("teamsAllowed"), new GUIContent("Teams Allowed"));

        EditorGUILayout.EndVertical();  // End box

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayPickupTypeGrid(PickupProxy proxy)
    {
        // Define grid dimensions
        int itemsPerRow = 4;
        int buttonWidth = 200; // Width of each button
        int buttonHeight = 40; // Height of each button

        // Start vertical layout
        EditorGUILayout.BeginVertical();

        // Start grid layout
        EditorGUILayout.BeginHorizontal();

        int index = 0;
        foreach (PickupType pickupType in (PickupType[])System.Enum.GetValues(typeof(PickupType)))
        {
            if (index > 0 && index % itemsPerRow == 0)
            {
                // Move to the next row
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
            }

            // Create button
            if (GUILayout.Button(pickupType.ToString(), buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                // Update the selected pickup type
                proxy.pickupType = pickupType;
                Debug.Log($"Selected PickupType: {pickupType}");
            }

            index++;
        }

        // End last row
        EditorGUILayout.EndHorizontal();

        // End vertical layout
        EditorGUILayout.EndVertical();
    }
}