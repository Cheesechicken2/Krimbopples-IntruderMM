using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRenderIfBehind : MonoBehaviour
{
    public List<MeshRenderer> renderers = new List<MeshRenderer>();
    public bool spectatorOnly = true;
}
