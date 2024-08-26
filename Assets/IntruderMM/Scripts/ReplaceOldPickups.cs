#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

public class ReplaceOldPickups : MonoBehaviour
{
	public static ReplaceOldPickups main;
	public ItemProxy[] itemProxies;
	[SerializeField] public ReplacePickupsList itemContainer;

#if UNITY_EDITOR

	[MenuItem("Intruder/Utilities/Replace Old Pickup Proxies")]
	public static void ReplaceAll()
	{

		int replaceCount = 0;
		GameObject go = new GameObject();
		main = go.AddComponent<ReplaceOldPickups>();

		main.itemProxies = main.itemContainer.itemProxies;

		PickupProxy[] pickupProxies = FindObjectsOfType<PickupProxy>();

		for (int i = 0; i < pickupProxies.Length; i++)
		{
			int pickupIndex = (int)pickupProxies[i].pickupType;

			if (pickupIndex >= 0 && pickupIndex < main.itemProxies.Length)
			{
				if (pickupProxies[i].pickupItem == null)
				{
					pickupProxies[i].pickupItem = main.itemProxies[pickupIndex];
					pickupProxies[i].pickupType = PickupType.Custom;
					EditorUtility.SetDirty(pickupProxies[i]);
					replaceCount++;
				}
			}
		}

		DestroyImmediate(go);

		EditorSceneManager.MarkAllScenesDirty();
		EditorUtility.DisplayDialog("Pickups Replaced", replaceCount + " pickups fixed!", "Great!");
	}

#endif

}