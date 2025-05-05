using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace LJVoyage.Game.Editor
{
    [InitializeOnLoad]
    public static class ProjectDrawer
    {
        static ProjectDrawer()
        {
            EditorApplication.projectWindowItemOnGUI -= OnItemCallback;

            EditorApplication.projectWindowItemOnGUI += OnItemCallback;
        }

        private static void OnItemCallback(string guid, Rect selectionRect)
        {

            if (guid == string.Empty)
            {
                return;
            }

            string path = AssetDatabase.GUIDToAssetPath(guid);

            AssetImporter import = AssetImporter.GetAtPath(path);

            //   EditorGUIUtility

            GUI.Label(selectionRect, new GUIContent(string.Empty, import.userData));
        }
    }
}