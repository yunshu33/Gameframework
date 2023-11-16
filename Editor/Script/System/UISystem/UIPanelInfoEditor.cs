#region Copyright
// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		UIPanelInfoEditor.cs
// Author Name :		YunShu
// Create Time :		2023/11/13 11:34:06
// Description :
// **********************************************************************
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using GameWorldFramework.RunTime;
using UnityEditor;
using UnityEngine;

namespace GameWorldFramework.Editor
{
    [CustomEditor(typeof(UIPanelInfo))]
    public class UIPanelInfoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            
        }

      
    }
}