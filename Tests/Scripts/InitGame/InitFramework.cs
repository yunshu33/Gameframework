#region Copyright
using System.Net.Mime;
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		InitFramework.cs
// Author Name :		YunShu
// Create Time :		2022/05/11 11:37:28
// Description :        初始化 框架
// **********************************************************************
#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yun.Util;
using UnityEngine.SceneManagement;

namespace Yun.Framework
{
    /// <summary>
    /// 初始化框架
    /// </summary>
    public class InitFramework : MonoBehaviour
    {
        public static IOCContainer Container = new();

      

        [SerializeField]
        [SceneName]
        private string sceneName;

        private void Awake()
        {
           _= InitLogSystem.Instance;

        }

        private void Update()
        {
            if (false)
            {
               //加载场景
               // SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                ///卸载场景
               // SceneManager.UnloadSceneAsync("InitFramework");
            }

            if (Input.anyKey)
            {

                Debug.Log("测试LogSystem");
                
            }
        }
    }
}