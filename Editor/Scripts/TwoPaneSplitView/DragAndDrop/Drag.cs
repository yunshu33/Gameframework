
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class Drag : EditorWindow
    {
        [MenuItem("LJVoyage/UIElements/Drag")]
        public static void ShowExample()
        {
            var wnd = GetWindow<Drag>();
            wnd.titleContent = new GUIContent("Drag");
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var box = new Box
            {
                style =
                {
                    width = 100,
                    height = 100,
                    backgroundColor = new StyleColor(Color.red)
                }
            };

            var manipulator = new DragAndDropManipulator(box);

            box.AddManipulator(manipulator);
            root.Add(box);

            box.style.position = Position.Absolute;

            // Foldout
            var g = new GroupBox();

            var a = new Toggle();
            var i = new TextField();
            var b = new Box
            {
                style =
                {
                    width = 100,
                    height = 100,
                    backgroundColor = new StyleColor(Color.blue),
                   
                },
                transform =
                {
                    position = new Vector3(100,100,0)
                }
            };

            root.Add(b);

            var dragAndDropManipulator = new DragAndDropManipulator(b);
        }
    }
}

