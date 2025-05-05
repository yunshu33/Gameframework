#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		VerifyFileMD5.cs
// Author Name :		YunShu
// Create Time :		2022/05/12 10:32:34
// Description :        testMd5 验证
// **********************************************************************
#endregion


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    public class VerifyFileMD5 : EditorWindow
    {
        [MenuItem("YunTools/Test/VerifyFileMD5")]
        static void Init()
        {
            
            VerifyFileMD5 window = (VerifyFileMD5)GetWindow(typeof(VerifyFileMD5));
            window.Show();
            window.minSize = new Vector2(200, 300);

        }
        private void OnGUI()
        {
            ToolMagazine.GetDragFilePath(this, AnalysisPath);
        }
        /// <summary>
        /// 打印文件md5值
        /// </summary>
        /// <param name="str"></param>
        private void AnalysisPath(string[] str)
        {
            if (str[0].Split('.').Length > 1)
            {
                FileStream file = new FileStream(str[0], FileMode.Open);

                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] retVal = md5.ComputeHash(file);

                file.Close();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < retVal.Length; i++)
                {

                    sb.Append(retVal[i].ToString("x2"));

                }
                Debug.Log(sb.ToString());
            }
        }
    }
}