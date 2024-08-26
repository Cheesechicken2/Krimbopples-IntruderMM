using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public string projectileName;

    public GameObject proj;

    public int speed = 1000;

    public bool playSound;

    public Sound spawnSound;

    public float fireRate;

    public bool canFire = true;

    public bool autoAddPhotonView;

    public Transform myTransform;

    public AudioSource myAudio;

    public int currentAmmo = 1;

    public int defaultAmmo = -1;

    public Animation animation;

    public float forwardOffset = 1f;

    public bool canHitOwner;

    public bool useLocalPos;
}

