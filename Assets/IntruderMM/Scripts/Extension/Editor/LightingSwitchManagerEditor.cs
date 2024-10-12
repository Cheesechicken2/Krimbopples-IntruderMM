using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightingSwitchManager))]
public class LightingSwitchManagerEditor : Editor
{
    private GUIStyle buttonStyle;
    private GUIStyle headerStyle;

    private Texture2D buttonNormalTexture;
    private Font customFont;

    public override void OnInspectorGUI()
    {
        LightingSwitchManager manager = (LightingSwitchManager)target;

        InitializeStyles();

        EditorGUILayout.LabelField("Lighting Switch Manager", headerStyle);

        EditorGUILayout.Space();

        manager.currentState = EditorGUILayout.IntField("Current State", manager.currentState);

        SerializedProperty lightingSwitchGroupsProp = serializedObject.FindProperty("lightingSwitchGroups");
        EditorGUILayout.PropertyField(lightingSwitchGroupsProp, true);

        SerializedProperty objectsToSetAsStaticProp = serializedObject.FindProperty("objectsToSetAsStatic");
        EditorGUILayout.PropertyField(objectsToSetAsStaticProp, true);

        SerializedProperty reflectionProbesProp = serializedObject.FindProperty("reflectionProbes");
        EditorGUILayout.PropertyField(reflectionProbesProp, true);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Actions", headerStyle);

        if (GUILayout.Button("Update Lighting Switch Groups", buttonStyle))
        {
            manager.UpdateLightingSwitchGroups();
            EditorUtility.SetDirty(manager);
        }

        if (GUILayout.Button("Reset States", buttonStyle))
        {
            manager.ResetStates();
            EditorUtility.SetDirty(manager);
        }

        serializedObject.ApplyModifiedProperties();
        Repaint();
    }

    private void InitializeStyles()
    {
        buttonNormalTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/IntruderMM/Scripts/Extension/Editor/GUI/button.png");
        customFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/IntruderMM/Scripts/Extension/Editor/GUI/Font/ShareTechMono-Regular.ttf");

        if (buttonNormalTexture != null)
        {
            buttonStyle = new GUIStyle(GUI.skin.button)
            {
                normal = { background = buttonNormalTexture },
                font = customFont
            };
        }
        else
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
        }

        headerStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            font = customFont,
            alignment = TextAnchor.MiddleCenter
        };
    }
}
