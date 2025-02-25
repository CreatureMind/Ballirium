using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Platform))]
[CanEditMultipleObjects]
public class PlatformSetButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Platform plat = (Platform)target;

        if (GUILayout.Button("Set Start Position"))
        {
            plat.startPosition = plat.transform.position;
        }

        if (GUILayout.Button("Set End Position"))
        {
            plat.endPosition = plat.transform.position;
        }
    }
}