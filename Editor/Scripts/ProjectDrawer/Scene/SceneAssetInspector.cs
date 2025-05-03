using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace YunFramework.Editor
{

    [CanEditMultipleObjects, CustomEditor(typeof(SceneAsset))]
    public class SceneAssetInspector : UnityEditor.Editor
    {

       

        string path;

        AssetImporter assetImporter;
        private void OnEnable()
        {
            for (int i = 0; i < targets.Length; ++i)
            {
                path = AssetDatabase.GetAssetPath(targets[i]);

            }

            assetImporter = AssetImporter.GetAtPath(path);

            

            
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
            base.OnInspectorGUI();

            bool enabled = GUI.enabled;

            GUI.enabled = true;

            EditorGUILayout.LabelField("描述" );

            GUI.changed = false;

            assetImporter.userData = EditorGUILayout.TextArea(assetImporter.userData, GUILayout.MinHeight(40));

            GUI.enabled = enabled;
        }
    }
}