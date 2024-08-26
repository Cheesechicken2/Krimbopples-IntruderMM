using UnityEngine;
using System;

public class SetTagsAndLayers : MonoBehaviour
{
	[ExecuteInEditMode()]

	public bool tagAndLayerNow = false;
	public bool untagCurrentObjects = true;

	public void TagAndLayerAll()
	{
		GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

		int objectsTagged = 0;
		foreach (GameObject g in gameObjects)
		{
			string needTag = "";
			string needLayer = "";

			objectsTagged = 0;

			if (g.tag != "Untagged")
			{
				needTag = g.tag;
			}

			if (LayerMask.LayerToName(g.layer) != "Default")
			{
				needLayer = LayerMask.LayerToName(g.layer);
			}

			if (needTag != "" || needLayer != "")
			{
				objectsTagged++;

				ObjectTagger ot = g.GetComponent<ObjectTagger>();

				if (ot == null)
					ot = g.AddComponent<ObjectTagger>();

				ot.objectTag = needTag;
				ot.objectLayer = needLayer;

				if (untagCurrentObjects)
				{
					g.tag = "Untagged";
					g.layer = 0;
				}
			}
		}

		Debug.Log("ObjectTagger set on " + objectsTagged + " scene objects");
	}
	public void Update()
	{

		if (tagAndLayerNow)
		{
			tagAndLayerNow = false;
			TagAndLayerAll();
		}

	}
}
