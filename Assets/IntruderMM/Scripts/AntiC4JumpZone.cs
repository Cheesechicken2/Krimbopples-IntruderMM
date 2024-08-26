using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiC4JumpZone : MonoBehaviour
{
    [HideInInspector]
    public bool isAFirstZone = false;
    public List<AntiC4JumpZone> requiredFirstZone = new List<AntiC4JumpZone>();

    protected void Awake()
    {
        if (requiredFirstZone != null)
        {
            foreach (var zone in requiredFirstZone)
            {
                zone.isAFirstZone = true;
            }
        }

        if (TryGetComponent(out Collider coll))
        {
            coll.isTrigger = true;
        }
    }
}
