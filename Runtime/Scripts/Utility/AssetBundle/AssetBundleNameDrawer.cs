#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		AssetBundleNameDraw.cs
// Author Name :		YunShu
// Create Time :		2022/12/06 10:45:03
// Description :
// **********************************************************************
#endregion



using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

namespace YunFramework.RunTime.Utility
{
    [CustomPropertyDrawer(typeof(AssetBundleNameAttribute))]
    public class AssetBundleNameDrawer : PropertyDrawer
    {
        public string[] Names
        {
            get
            {
                
                return AssetDatabase.GetAllAssetBundleNames();
            }
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


            if (names.Length > 0 && property.stringValue != null)
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
# endif