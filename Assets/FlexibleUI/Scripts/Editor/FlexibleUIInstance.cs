using UnityEngine;
using UnityEditor;

public class FlexibleUIInstance : Editor
{
    private static GameObject clickedObject;

    [MenuItem("GameObject/Flexible UI/Button", priority = 0)]
    public static void CreateButton()
    {
        Create("Button");
    }

    [MenuItem("GameObject/Flexible UI/Text", priority = 0)]
    public static void CreateText()
    {
        Create("Text");
    }

    [MenuItem("GameObject/Flexible UI/Panel", priority = 0)]
    public static void CreatePanel()
    {
        Create("Panel");
    }

    [MenuItem("GameObject/Flexible UI/Progress Bar", priority = 0)]
    public static void CreateProgressBar()
    {
        Create("ProgressBar");
    }

    private static GameObject Create(string objectName)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(objectName));
        instance.name = objectName;
        clickedObject = Selection.activeObject as GameObject;
        if (clickedObject != null) {
            instance.transform.SetParent(clickedObject.transform, false);
        }

        return instance;
    }
}
