using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor.Dialogue
{
    public class DialogueGraphViewEditorWindow : EditorWindow
    {

        DialogueGraphView graphView;

        static DialogueGraphViewEditorWindow instance;

        DialogueGraphViewData data;


         [MenuItem("LJVoyage/对话编辑器")]
        static void Init()
        {
            instance = GetWindow<DialogueGraphViewEditorWindow>();

            instance.titleContent.text = "对话编辑器";

            instance.Show();

        }

        [OnOpenAsset]
        static bool OnOpenAsset(int instanceID, int line, int column)
        {

            if (Selection.activeObject is DialogueGraphViewData)
            {

                Init();
                return true;
            }
            else
            {
                return false;
            }

        }

        private void OnEnable()
        {

            data = Selection.activeObject as DialogueGraphViewData;

            graphView = new DialogueGraphView(this, data)
            {
                name = "Dialogue Graph"
            };

            // 让graphView铺满整个Editor窗口
            graphView.StretchToParentSize();

            // 把它添加到EditorWindow的可视化Root元素下面
            rootVisualElement.Add(graphView);

        }

        private void OnDisable()
        {

            rootVisualElement.Remove(graphView);

        }


    }
}