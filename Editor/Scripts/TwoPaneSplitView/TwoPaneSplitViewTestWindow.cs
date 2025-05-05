using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game
{
    class TwoPaneSplitViewTestWindow : EditorWindow
    {
        [MenuItem("Yun/UIElements/TwoPaneSplitViewTest")]
        static void ShowWindow()
        {
            var window = GetWindow<TwoPaneSplitViewTestWindow>();
            window.titleContent = new GUIContent("TwoPaneSplitViewTest");
            window.Show();
        }

        private void OnEnable()
        {
            var root = rootVisualElement;
            
            var xmlAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Yun/UIElements/Test/TwoPaneSplitView/Uxml/TwoPaneSplitViewTestWindow.uxml");
            xmlAsset.CloneTree(root);
        }
    }
}