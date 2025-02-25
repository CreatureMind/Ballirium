using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Platform))]
[RequireComponent(typeof(Rigidbody))]
// [CanEditMultipleObjects]
public class PlatformSetButtons : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Platform plat = (Platform)target;

        if (GUILayout.Button("Set Start Position"))
        {
            plat.SetStartPos();
            Debug.Log("Set Start Position " + plat.transform.position);
        }

        if (GUILayout.Button("Set End Position"))
        {
            plat.SetEndPos();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}