using UnityEngine;
public class MirrorReflection : MonoBehaviour
{
	public bool m_DisablePixelLights = true;
	public int m_TextureSize = 256;
	public float m_ClipPlaneOffset = 0.07f;
	public LayerMask m_ReflectLayers = -1;
}
