using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LampsController))]
public class LampsControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LampsController myTarget = (LampsController)target;
        if (GUILayout.Button("Znajdź światła"))
        {
            myTarget.FindAllLights();
        }
    }
}
