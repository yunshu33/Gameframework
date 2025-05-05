#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		FrameworkConfig.cs
// Author Name :		YunShu
// Create Time :		2022/05/11 11:55:05
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game
{
    public class FrameworkConfig 
    {
        /// <summary>
        /// 是否打印框架调试信息
        /// </summary>
        public static bool isLog= true;


        /// <summary>
        /// 控制台打印
        /// </summary>
        /// <param name="massage"></param>
        public static void Log(string massage)
        {
            if (isLog)
            {
                Debug.Log(massage);
            }
        }
    }
}