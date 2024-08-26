using UnityEngine;
using System.Collections;

public class ScrollingUVs : MonoBehaviour
{
	public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
	public string textureName = "_MainTex";
	private Renderer renderero;

	Vector2 uvOffset = Vector2.zero;
	void Start()
	{
		renderero = GetComponent<Renderer>();
	}
	void LateUpdate()
	{
		if (renderero == null)
			return;

		uvOffset += (uvAnimationRate * Time.deltaTime);
		if (renderero.enabled)
		{
			renderero.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
		}
	}
}