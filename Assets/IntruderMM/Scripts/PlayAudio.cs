using UnityEngine;
using System;

public class PlayAudio : MonoBehaviour
{
	public AudioClip[] clips;

	public void PlayCurrent()
	{
		GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
	}

	public void Play(AudioClip clip)
	{
		GetComponent<AudioSource>().PlayOneShot(clip);
	}

	public void PlayClip(int index)
	{

		GetComponent<AudioSource>().PlayOneShot(clips[index]);
	}
}