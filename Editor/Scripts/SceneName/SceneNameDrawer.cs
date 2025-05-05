#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		SceneNameDrawer.cs
// Author Name :		YunShu
// Create Time :		2022/07/06 16:25:54
// Description :
// **********************************************************************
#endregion


using System;

using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
namespace LJVoyage.GameEditor
{

    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class SceneNameDrawer : PropertyDrawer
    {

        public string[] Names
        {
            get
            {
                return GetAllSceneName();
            }
        }

        /// <summary>
        /// 获得所有场景名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllSceneName()
        {
            var list = new List<string>();
            foreach (var editorBuildSettingsScene in EditorBuildSettings.scenes)
            {
                if (editorBuildSettingsScene.enabled)
                {
                    string name = editorBuildSettingsScene.path.Substring(editorBuildSettingsScene.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);

                    list.Add(name);
                }
            }
            return list.ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] names = Names;

            ///初始值为空bug
            if (property.stringValue == "")
            {


                if (names.Length > 0)
                {
                    property.stringValue = names[0];

                }
                else
                {
                    EditorGUI.Popup(position, property.displayName, 0, new string[] { "" });
                }

                return;
            }


            if (names.Length > 0 && property.stringValue is not null)
            {
                int num = Array.IndexOf(names, property.stringValue);
                if (num == -1)
                {
                    if (names.Length > 0)
                    {
                        property.stringValue = names[0];

                    }
                    else
                    {
                        EditorGUI.Popup(position, property.displayName, 0, new string[] { "" });
                    }
                }
                else
                {
                    num = EditorGUI.Popup(position, property.displayName, num, names);
                    property.stringValue = names[num];
                }

                return;
            }
            if (names.Length == 0)
            {
                EditorGUI.Popup(position, property.displayName, 0, new string[] { "" });
                return;
            }
            base.OnGUI(position, property, label);
        }
    }

}
#endif