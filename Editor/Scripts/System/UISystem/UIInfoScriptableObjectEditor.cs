#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		UIInfoScriptableObjectEditor.cs
// Author Name :		YunShu
// Create Time :		2023/11/15 16:32:40
// Description :
// **********************************************************************

#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YunFramework.RunTime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.GameEditor
{
    [CustomEditor(typeof(UIInfoScriptableObject))]
    public class UIInfoScriptableObjectEditor : UnityEditor.Editor
    {
        private VisualElement inspector;

        private void OnEnable()
        {
           var so =  serializedObject.targetObject as UIInfoScriptableObject;
           
           so.Reset();
           
        }


        public override void OnInspectorGUI()
        {

            if (serializedObject.hasModifiedProperties)
            {
                inspector?.MarkDirtyRepaint();
            }
            
            base.OnInspectorGUI();
        }


        public override VisualElement CreateInspectorGUI()
        {
            inspector = new VisualElement();

            inspector.name = nameof(inspector);

            inspector.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
            inspector.style.width = new StyleLength(new Length(100, LengthUnit.Percent));

            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/YGameFramework/Editor/Script/System/UISystem/UIInfoScriptableObject.uxml");

            uxml.CloneTree(inspector);

            var listview = inspector.Q<ListView>();

            listview.headerTitle = "Infos";

            listview.onItemsChosen += ints => Debug.Log(nameof(listview.onItemsChosen));

            // Return the finished inspector UI
            return inspector;
        }
    }
}