using UnityEngine;
using System;


public class AnimateColor : MonoBehaviour
{
	Renderer myRenderer;
	public Color myColor;

	public void Start()
	{
		myRenderer = GetComponent<Renderer>();
	}

	public void Update()
	{
		if (myRenderer.material.color != myColor)
			myRenderer.material.color = myColor;
	}
}