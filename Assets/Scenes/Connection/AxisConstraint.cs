using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AxisConstraint))]
class ConnectLineHandleExampleScript : Editor
{
    const float lineLength = 10.0f;

    void OnSceneGUI()
    {
        AxisConstraint axisConstraint = target as AxisConstraint;

        Vector3 direction = axisConstraint.direction;
        Vector3 position = axisConstraint.transform.position;

        Handles.color = Color.green;
        Handles.DrawLine(position - direction * lineLength * 0.5f, position + direction * lineLength * 0.5f);
    }
}

public class AxisConstraint : MonoBehaviour
{
    public Vector3 direction
    {
        get
        {
            return transform.forward;
        }
    }
}
