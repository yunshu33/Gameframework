#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		UIInfo.cs
// Author Name :		YunShu
// Create Time :		2023/11/14 16:41:54
// Description :
// **********************************************************************

#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YunFramework.RunTime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEngine.UIElements.PopupWindow;

namespace YunFramework.Editor
{
    // IngredientDrawer
    [CustomPropertyDrawer(typeof(UIInfo))]
    public class UIInfoEditor : PropertyDrawer
    {
        private float propertyHeight = EditorGUIUtility.singleLineHeight;
        public VisualTreeAsset m_InspectorXML;
        private List<FieldInfo> fields;

        public UIInfoEditor()
        {
            fields = new List<FieldInfo>();

            fields = typeof(UIInfo).GetFields().ToList();
        }

       

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var popup = new PopupWindow
            {
                text = property.propertyPath
            };

            var pathProperty =  property.FindPropertyRelative("path");
            var pathPropertyField = new PropertyField(pathProperty, "Path");
            pathPropertyField.SetEnabled(false);
            
            popup.Add(pathPropertyField);
            
            var bundleName = property.FindPropertyRelative("bundleName");

            if (!String.IsNullOrEmpty(bundleName.stringValue))
            {
                var bundleNamePropertyField = new PropertyField(bundleName, "BundleName");
                
                bundleNamePropertyField.SetEnabled(false);
            
                popup.Add(bundleNamePropertyField);
               
            }

            var toggle = new Toggle();
            popup.Add(toggle);
            var button = new Button();
            
            popup.Add(button);
            
            button.clicked += () => { Debug.Log("clicked");};
            
            return popup;
        }
    }
}