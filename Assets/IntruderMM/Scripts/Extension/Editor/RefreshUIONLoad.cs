using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class RefreshUIONLoad
{
    static RefreshUIONLoad()
    {
        EditorApplication.update += RefreshUI;
    }

    private static void RefreshUI()
    {

        EditorApplication.update -= RefreshUI;


        foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
        {
            window.Repaint();
        }
    }
}
