using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 5;
    public int energyDamage = 0;
    public int balanceDamage = 0;
    public float damageInterval = 2.0f;
    public bool blurOnHit = true;
    public Sound hurtSoundPrefab;
}
