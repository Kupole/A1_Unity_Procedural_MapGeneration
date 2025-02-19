using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class Editor_MapFunctions : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Map map = (Map)target;
        if (GUILayout.Button("Generate"))
        {
            map.Generate(); // Ensure Map has a public Generate method
        }
    }
}

