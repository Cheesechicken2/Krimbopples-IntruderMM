using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliGun : MonoBehaviour
{
    public Transform shootTransform;

    public LayerMask shouldShootMask;

    public SightSensor sightSensor;

    public SpawnProjectile spawnProjectile;

    public bool on;

    public float checkInterval = 1.3f;

    public bool shootIntruders;

    public bool shootGuards;

    public float targetingRotationSpeed = 20f;

    public int shotsPerSalvo = 5;

    public float fireRate = 0.1f;

}
