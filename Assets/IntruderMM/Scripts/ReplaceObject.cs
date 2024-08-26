using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[System.Serializable]
public class ReplaceObject : MonoBehaviour
{
    public GameObject[] objectArray;
    public bool isPrefab = true;
    public GameObject objectPrefab;

    [ContextMenu("Random rotate")]
    public void RandomRotate()
    {
        for (int i = 0; i < objectArray.Length; i++)
        {
            Vector3 tmp_cs1 = objectArray[i].transform.eulerAngles;
            tmp_cs1.y = UnityEngine.Random.Range(0, 359);
            objectArray[i].transform.eulerAngles = tmp_cs1;
        }
    }

    [ContextMenu("Replace now")]
    public void ReplaceNow()
    {
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (isPrefab)
            {
                GameObject clone = PrefabUtility.InstantiatePrefab(objectPrefab) as GameObject;

                clone.transform.position = objectArray[i].transform.position;
                clone.transform.rotation = objectArray[i].transform.rotation;
                clone.transform.localScale = objectArray[i].transform.localScale;
                clone.transform.SetParent(objectArray[i].transform.parent);
            }
            else
            {
                GameObject op = Instantiate(objectPrefab, objectArray[i].transform.position, objectArray[i].transform.rotation);
                op.transform.localScale = objectArray[i].transform.localScale;
                op.transform.SetParent(objectArray[i].transform.parent);
            }
        }
    }
}

#endif
