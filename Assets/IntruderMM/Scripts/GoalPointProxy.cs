using UnityEngine;
using System;

[System.Serializable]
public class GoalPointProxy : MonoBehaviour
{
    public Activator goalCompleteActivator;
    public bool allowFallingCapture = false;
    public bool customTransform;
    public bool customMesh;
}