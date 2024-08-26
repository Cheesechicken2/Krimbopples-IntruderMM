using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(TuningProxy))]
public class TuningProxyEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Type tempType = TypeUtility.GetTypeByName("TuningProxy");
		MethodInfo theMethod = tempType.GetMethod("CheckJSON");
		object[] userParameters = new object[0];

		var myScript = target;
		if (GUILayout.Button("Check JSON formatting"))
		{
			theMethod.Invoke(myScript, userParameters);
		}
	}

	// Stashing this here for now so we can make this code DLL-able
	static class TypeUtility
	{
		public static Type GetTypeByName(string name)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (Type type in assembly.GetTypes())
				{
					if (type.Name == name)
						return type;
				}
			}
			return null;
		}
	}
}

