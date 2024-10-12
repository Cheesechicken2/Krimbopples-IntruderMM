using System.Collections.Generic;
using UnityEngine;

public class LightingSwitchManager : MonoBehaviour
{
    public int currentState;
    public LightingSwitchGroup[] lightingSwitchGroups;
    public static LightingSwitchManager main;
    public List<GameObject> objectsToSetAsStatic;
    public ReflectionProbe[] reflectionProbes;

    public void UpdateLightingSwitchGroups()
    {
        Debug.Log("UpdateLightingSwitchGroups called");
    }


    public void ResetStates()
    { 
        Debug.Log("ResetStates called");
    }
}
