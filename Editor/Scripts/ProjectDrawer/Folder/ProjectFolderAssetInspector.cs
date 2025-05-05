using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.WSA;

namespace LJVoyage.GameEditor
{

    [CanEditMultipleObjects, CustomEditor(typeof(DefaultAsset))]
    public class ProjectFolderAssetInspector : UnityEditor.Editor
    {
        private string path = string.Empty;

        private AssetImporter assetImporter;

        private bool isFolder = false;

        void OnEnable()
        {

            path = AssetDatabase.GetAssetPath(targets[0]);

            isFolder = AssetDatabase.IsValidFolder(path);

            if (isFolder)
            {
                assetImporter = AssetImporter.GetAtPath(path);
            }
            else
            {
                path = string.Empty;
            }

        }

        private void OnDestroy()
        {
            if (GUI.changed && assetImporter != null)
            {
                assetImporter?.SaveAndReimport();
            }
            
        }

        public override void OnInspectorGUI()
        {

            if (isFolder)
            {
                DrawFolder();
            }

            base.OnInspectorGUI();

        }

        private void DrawFolder()
        {

            bool enabled = GUI.enabled;

            GUI.enabled = true;

            EditorGUILayout.PrefixLabel("描述");

            assetImporter.userData = EditorGUILayout.TextArea(assetImporter.userData, GUILayout.MinHeight(48));

            GUI.enabled = enabled;

        }
    }
}