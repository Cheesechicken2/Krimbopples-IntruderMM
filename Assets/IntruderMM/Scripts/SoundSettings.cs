using UnityEngine;
using System;

public enum VoiceDistance
{
	Normal = 0,
	Short = 1
}

public enum StepDistance
{
	Normal = 0,
	Short = 1
}

public class SoundSettings : MonoBehaviour
{
	public VoiceDistance voiceDistance;
	public StepDistance stepDistance;
}