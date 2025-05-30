#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		AssetBundle.cs
// Author Name :		YunShu
// Create Time :		2022/07/26 11:37:54
// Description :
// **********************************************************************
#endregion


using System;
using System.Collections.Generic;
using LJVoyage.Game.Editor.Tools;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.Game.Editor.HotFix
{
	public class ReadAsset : UnityEditor.Editor
	{
        [MenuItem("YunTools/ReadScriptableObject")]
        private static void ReadAssetObject()
        {
            var asset = AssetDatabase.LoadAssetAtPath<HotFixDllList>(@"Assets/YunFramework/Resources/ScriptableObject/HotFixDllList.asset");
            foreach (var item in asset.hotFixDlls)
            {
                Debug.Log(item.dll);
            }
        }


	}
}