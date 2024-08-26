using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : MonoBehaviour
{
    public float verticalFOV = 74f;

    public float horizontalFOV;

    public float belowEyesFOV = 90f;

    public float finalBelowEyesFOV = 90f;

    public bool autoSetHorizontalFOV = true;

    public Transform eyes;

    public Camera eyeCam;

    public Transform debugTransform;

    private Color _seenColor = new Color(0f, 0f, 1f, 0.5f);

    private Color _notSeenColor = new Color(1f, 0f, 0f, 0.5f);

    private Color _vertFovColor = new Color(0f, 1f, 0f, 0.05f);

    public Color arcColor = Color.green;

    public float arcDist = 3f;

    private bool targetSeen;

    public float textY;

    public bool verticalFOVBasedOnDistance;

    public bool horizontalFOVBasedOnDistance;

    public bool belowEyesFOVBasedOnDistance;

    public AnimationCurve verticalFOVDistanceCurve;

    public AnimationCurve horizontalFOVDistanceCurve;

    public AnimationCurve belowEyesFOVDistanceCurve;

    public float targetDistance;

}
