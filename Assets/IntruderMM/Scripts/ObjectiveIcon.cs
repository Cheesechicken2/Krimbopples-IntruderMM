using System;
using UnityEngine;
using UnityEngine.UI;

public enum ObjectiveIconType
{
    Briefcase,
    CapturePoint,
    ComputerHackNode
}

[Serializable]
public class ObjectiveIcon
{
    public ObjectiveIconType objectiveIconType;
    public Vector2 position;
    private Image iconImage;
}