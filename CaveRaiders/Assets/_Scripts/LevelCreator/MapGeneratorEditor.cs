using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create New Map")) {
            ((MapGenerator)target).CreateNewMap();
        }
        
        if (GUILayout.Button("Validate Map")) {
            ((MapGenerator)target).ValidateMap();
        }

        if (GUILayout.Button("Generate Map")) {
            ((MapGenerator)target).GenerateMap();
        }
    }
}