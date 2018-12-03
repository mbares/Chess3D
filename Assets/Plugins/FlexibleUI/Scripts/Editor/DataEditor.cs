using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlexibleUIData), true)]
public class DataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isEditor;

        if (GUILayout.Button("Apply changes")) {
            FlexibleUI[] elements = FindObjectsOfType<FlexibleUI>();
            for (int i = 0; i < elements.Length; i++) {
                elements[i].OnSkinUI();
                EditorUtility.SetDirty(elements[i]);
            }
        }
    }
}
