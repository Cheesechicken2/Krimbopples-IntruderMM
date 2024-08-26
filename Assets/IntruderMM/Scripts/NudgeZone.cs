using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NudgeZone : MonoBehaviour
{
    [Header("Nudge Settings")]
    [Tooltip("Nudge speed")]
    public float speed = 10f;

    [Tooltip("How quickly to accelerate to max speed")]
    public float lerpSpeed = 10f;

    [Header("Ragdoll Settings")]
    [Tooltip("Speed applied to ragdoll objects")]
    public float ragSpeed = 100f;

    [Tooltip("Torque applied to ragdoll objects")]
    public Vector3 ragTorque;

    [Tooltip("(REQUIRED) Assign a transform here with the blue Z-axis (forward) facing the direction you want to nudge")]
    public Transform directionTransform;

    [Tooltip("(OPTIONAL) Assign a transform for the ragdoll direction")]
    public Transform ragDirectionTransform;

    [Header("Options")]
    [Tooltip("Will nudge the opposite direction if the character is behind the Direction Transform, useful for thin objects like the tops of doors")]
    public bool twoWay = false;

    [Tooltip("(OPTIONAL) Two Way will use this transform forward direction if behind the Direction Transform")]
    public Transform twoWayDirectionTransform;

    [Tooltip("(OPTIONAL) weeeeeee")]
    public bool setYVelocity;

    [Tooltip("Allows player movement during the nudge")]
    public bool allowPlayerMovement;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (directionTransform == null)
        {
            return;
        }

        float speedVectorSize = speed * 0.25f;

        Gizmos.color = UnityEngine.Color.magenta;
        Gizmos.DrawRay(directionTransform.position, directionTransform.forward * speedVectorSize);

        if (twoWay)
        {
            Gizmos.color = UnityEngine.Color.yellow;

            if (twoWayDirectionTransform)
            {
                Gizmos.DrawRay(twoWayDirectionTransform.position, twoWayDirectionTransform.forward * speedVectorSize);
            }
            else
            {
                Gizmos.DrawRay(directionTransform.position, -directionTransform.forward * speedVectorSize);
            }
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(NudgeZone))]
public class NudgeZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NudgeZone nudgeZone = (NudgeZone)target;

        // Draw the default inspector with all fields
        DrawDefaultInspector();

        // Conditional display of fields
        if (nudgeZone.twoWay)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("twoWayDirectionTransform"));
        }

        if (nudgeZone.setYVelocity)
        {
            EditorGUILayout.HelpBox("Set Y Velocity is enabled. Ensure that the appropriate behavior is implemented.", MessageType.Info);
        }

        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
