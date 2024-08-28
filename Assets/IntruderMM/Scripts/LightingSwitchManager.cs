using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitchManager : MonoBehaviour
{
    public int currentState;

    public LightingSwitchGroup[] lightingSwitchGroups;

    public static LightingSwitchManager main;

    public List<GameObject> objectsToSetAsStatic;

    public ReflectionProbe[] reflectionProbes;
}
