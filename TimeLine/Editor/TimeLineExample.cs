
using LJVoyage.Game;
using UnityEngine;

using UnityEditor;

public class TimeLineExample : EditorWindow
{
    [MenuItem("Window/Time Line Example")]
    public static void ShowExample()
    {
        var window = GetWindow<TimeLineExample>();

        window.titleContent = new GUIContent("Time Line Example");
    }

    private void CreateGUI()
    {
        var view = new TwoPaneView
        {
            Orientation = E_Orientation.Vertical,
            MinDimension = 150,
            MaxDimension = 300
        };

        rootVisualElement.Add(view);
    }
}