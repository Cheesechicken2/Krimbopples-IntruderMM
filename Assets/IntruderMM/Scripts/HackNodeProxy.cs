using UnityEngine;
using UnityEngine.Events;

public class HackNodeProxy : MonoBehaviour
{
	public string nodeName = "A1";
	public float hackTime = 3.0f;
	public bool playDefaultHackSounds = true;
    public UnityEvent hackEvent;
    public float emissiveIntensity = 2.0f;
}
