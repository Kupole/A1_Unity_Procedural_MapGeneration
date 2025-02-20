using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class Editor_MapFunctions : Editor
{
    private SerializedObject serializedMap;
    private SerializedProperty mapDataProperty;

    private void OnEnable()
    {
        serializedMap = new SerializedObject(target);
        mapDataProperty = serializedMap.FindProperty("data"); // Ensure "data" exists in Map
    }

    public override void OnInspectorGUI()
    {
        serializedMap.Update();

        DrawDefaultInspector();

        Map map = (Map)target;

        if (GUILayout.Button("Generate"))
        {
            map.Generate();
        }

        // Detect changes in the Map_Data component
        if (serializedMap.hasModifiedProperties)
        {
            serializedMap.ApplyModifiedProperties();
            if (mapDataProperty != null)
            {
                map.Generate(); // Auto-generate map when Map_Data values change
            }
        }
    }
}
