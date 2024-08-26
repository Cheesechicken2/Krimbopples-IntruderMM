using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityMods : MonoBehaviour
{
	public float shadowDistance = 300;

    public bool forceShadowMaskMode;

    public ShadowmaskMode shadowMaskMode;

    private static QualityMods main;

    void Awake()
	{
		QualitySettings.shadowDistance = shadowDistance;
	}
}
