#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Assets.IntruderMM.Editor
{
	[CustomEditor(typeof(ObjectTagger)), CanEditMultipleObjects]
	public class ObjectTaggerEditor : UnityEditor.Editor
	{
		ObjectTagger taggerTarget;
		string[] tags = new[]
		{"Untagged",
		"PrefabPools",
		"Enemy",
		"Door",
		"Elevator",
		"Glass",
		"Slope",
		"Stairs",
		"Metal",
		"AIPath",
		"Slippery",
		"SuperSlippery",
		"Water",
		"Dirt",
		"Carpet",
		"Movable",
		"Tire",
		"ThickMetal",
		"Deathzone",
		"MainLevel",
		"Destructible",
		"Ladder",
		"Mover",
		"SteepSlope" };

		string[] layers = new[]
		{ "Default",
		"TransparentFX",
		"Ignore Raycast",
		"Water",
		"UI",
		"Doors",
		"MyCharGraphics",
		"MyFPGraphics",
		"CharControllers",
		"OPFPGraphics",
		"DoorTrigger",
		"Glass",
		"Screen",
		"MyStandHitbox",
		"IgnorePlayer",
		"IgnoreBullet",
		"Hitbox",
		"Projectile",
		"Special",
		"Rooms",
		"Lights",
		"Plants",
		"Terrain",
		"Terrain2",
		"OnlyHitLevel",
		"IgnoreViewCast",
		"IgnoreMeshMerge"};

		string oldTag = null;
		string oldLayer = null;
		int tagsChoiceIndex = 0;
		int layersChoiceIndex = 0;
		private void OnEnable()
		{
			taggerTarget = (ObjectTagger)target;
		}

		public override void OnInspectorGUI()
		{
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.LabelField("Tags and Layers List", EditorStyles.boldLabel);
			tagsChoiceIndex = EditorGUILayout.Popup("Object Tag List", tagsChoiceIndex, tags);
			layersChoiceIndex = EditorGUILayout.Popup("Object Layer List", layersChoiceIndex, layers);
			EditorGUILayout.EndVertical();

			if (oldTag != taggerTarget.objectTag)
			{
				tagsChoiceIndex = tags.Contains(taggerTarget.objectTag) ? System.Array.IndexOf(tags, taggerTarget.objectTag) : 0;
				oldTag = taggerTarget.objectTag;
			}

			if (oldLayer != taggerTarget.objectLayer)
			{
				layersChoiceIndex = layers.Contains(taggerTarget.objectLayer) ? System.Array.IndexOf(layers, taggerTarget.objectLayer) : 0;
				oldLayer = taggerTarget.objectLayer;
			}

			serializedObject.FindProperty("objectTag").stringValue = tags[tagsChoiceIndex];
			serializedObject.FindProperty("objectLayer").stringValue = layers[layersChoiceIndex];

			serializedObject.ApplyModifiedProperties();
		}
	}

}

#endif