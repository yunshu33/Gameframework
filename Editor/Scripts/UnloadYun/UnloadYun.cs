#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		UnloadYun.cs
// Author Name :		YunShu
// Create Time :		2022/05/11 09:53:07
// Description :    卸载Yun
// **********************************************************************
#endregion


using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    public class UnloadYun : UnityEditor.Editor
	{
        /// <summary>
        /// 卸载YunTools
        /// </summary>
        [MenuItem("YunTools/UnloadYun/UnloadYunTools")]
        private static void UnloadYunTools()
        {
            UnloadFile("Assets/YunTools");
        }

        /// <summary>
        /// 卸载YunFarmework
        /// </summary>
        [MenuItem("YunTools/UnloadYun/UnloadYunFarmework")]
        private static void UnloadYunFarmework()
        {
            UnloadFile("Assets/YunFramework");
        }

        /// <summary>
        /// 卸载  CompleteYun 
        /// </summary>
        [MenuItem("YunTools/UnloadYun/UnloadCompleteYun")]
        private static void UnloadCompleteYun()
        {
            UnloadYunFarmework();
            UnloadYunTools();
        }
        /// <summary>
        /// 卸载文件夹
        /// </summary>
        /// <param name="path"></param>
        private static void UnloadFile(string path)
        {
            //删除文件夹
            Directory.Delete(path, true);
            //不删除meta unity 会自动创建一个空文件夹
            File.Delete(path +".meta");
            Debug.LogWarning(path + ":文件已卸载");
            //刷新资源
            AssetDatabase.Refresh();
        }

    }
}