#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		ABBuild.cs
// Author Name :		YunShu
// Create Time :		2022/07/27 11:49:52
// Description :
// **********************************************************************
#endregion


using System;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.GameEditor
{
	public class ABBuild : Editor
	{
        [MenuItem("YunTools/ABBuild")]
        private static void DefaultTool()
        {

            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (var item in names)
            {

                Debug.Log(item);
            }
        }
	}
}