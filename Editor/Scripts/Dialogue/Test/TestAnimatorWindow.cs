using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LJVoyage.Game.Editor.Dialogue
{
    public class TestAnimatorWindow : EditorWindow
    {
        //菜单
        [MenuItem("SKFramework/Tools/Animation Clip Previewer")]
        private static void Open()
        {
            //打开窗口
            GetWindow<TestAnimatorWindow>("Animation Clip Previewer").Show();
        }

        private Scene previewScene;

        private Scene presentScene;

        private void OnGUI()
        {
            //NewPreviewScene

            if (GUILayout.Button("打开预览场景"))
            {
                presentScene = SceneManager.GetActiveScene();

                PreviewRenderUtility utility = new PreviewRenderUtility();


                utility.AddSingleGO(null);

                utility.BeginPreview(new Rect(0, 0, 10, 10), GUIStyle.none);



                utility.EndPreview();

            }
        }

        private void OnDestroy()
        {
            //  EditorSceneManager.ClosePreviewScene(previewScene);
            Debug.Log(presentScene.path);

            EditorSceneManager.OpenScene(presentScene.path);

            Debug.Log("关闭预览场景");
        }
    }
}