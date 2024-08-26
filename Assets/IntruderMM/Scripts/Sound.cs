using System.Collections;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    public float volume = 1.0f;
    public float pitchMin = 0.0f;
    public float pitchMax = 0.0f;
}
