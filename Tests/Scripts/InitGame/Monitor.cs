#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		Monitor.cs
// Author Name :		YunShu
// Create Time :		2022/06/30 14:02:06
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Yun.Framework
{
    /// <summary>
    /// 监听
    /// </summary>
    public class Monitor : MonoSingleton<Monitor>
    {

        protected override void Start()
        {
            base.Start();
            switchingScenesDestroy = true;
        }

        void Update()
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("singleton");
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            InitFramework.Container.Remove<Monitor>();
        }
        public void Test()
        {
            Debug.Log("Monitor");
        }

    }
}