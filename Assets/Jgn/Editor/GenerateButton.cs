using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GenerateButton : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager generator = (GameManager)target;
        if (GUILayout.Button("Generate Cubes"))
        {
            generator.Generate();
        }
    }
    
}
