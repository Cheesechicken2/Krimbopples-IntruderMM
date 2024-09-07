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

    private Vector2 scrollPosition;
    private Font customFont;

    private Dictionary<PickupType, ItemProxy> pickupTypeToProxyMap;

    private void SetStyles()
    {
        if (buttonStyle == null)
        {
            Texture2D buttonTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/button.png", typeof(Texture2D));
            Texture2D buttonHoverTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/buttonHover.png", typeof(Texture2D));
            Texture2D buttonSelectTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/buttonSelect.png", typeof(Texture2D));
            Texture2D boxTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/IntruderMM/Scripts/Extension/Editor/GUI/box.png", typeof(Texture2D));

            customFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/IntruderMM/Scripts/Extension/Editor/GUI/Font/ShareTechMono-Regular.ttf");

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

            boxStyle = new GUIStyle(EditorStyles.helpBox)
            {
                normal = { background = boxTexture },
                padding = new RectOffset(10, 10, 10, 10),
                margin = new RectOffset(0, 0, 10, 10)
            };

            labelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                font = customFont,
                alignment = TextAnchor.MiddleLeft
            };


            textAreaStyle = new GUIStyle(EditorStyles.textArea)
            {
                fontSize = 14,
                font = customFont,
                wordWrap = true
            };


            headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 16,
                font = customFont,
                alignment = TextAnchor.MiddleCenter
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


        EditorGUILayout.LabelField("Pickup Types", headerStyle);


        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(400));  


        DisplayPickupTypeGrid(proxy);

        EditorGUILayout.EndScrollView();


        EditorGUILayout.BeginVertical(boxStyle);
        EditorGUILayout.LabelField("Other Settings", headerStyle);


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

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayPickupTypeGrid(PickupProxy proxy)
    {
        int itemsPerRow = 4;
        int buttonWidth = 200; // Width of each button
        int buttonHeight = 40; // Height of each button

        EditorGUILayout.BeginVertical();


        EditorGUILayout.BeginHorizontal();

        int index = 0;
        foreach (PickupType pickupType in (PickupType[])System.Enum.GetValues(typeof(PickupType)))
        {
            if (index > 0 && index % itemsPerRow == 0)
            {

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
            }


            if (GUILayout.Button(pickupType.ToString(), buttonStyle, GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {

                if (pickupTypeToProxyMap.TryGetValue(pickupType, out ItemProxy proxyItem))
                {
                    proxy.pickupItem = proxyItem;

                    RefreshMesh(proxy);
                }
                proxy.pickupType = pickupType;
                Debug.Log($"Selected PickupType: {pickupType}, Updated Pickup Item: {proxy.pickupItem}");
            }

            index++;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void RefreshMesh(PickupProxy proxy)
    {
        MeshFilter meshFilter = proxy.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            if (proxy.pickupItem != null && proxy.pickupItem.pickupMesh != null)
            {
                meshFilter.mesh = proxy.pickupItem.pickupMesh;
            }
            else
            {
                meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
            }
        }

        MeshRenderer meshRenderer = proxy.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = proxy.gameObject.AddComponent<MeshRenderer>();
        }

    }
}