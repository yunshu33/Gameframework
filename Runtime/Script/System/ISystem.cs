#region Copyright
// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		ISystem.cs
// Author Name :		YunShu
// Create Time :		2023/09/08 14:16:06
// Description :
// **********************************************************************
#endregion


using UnityEngine;

namespace GameWorldFramework.RunTime
{
    public interface ISystem
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        public void InitConfig();

        public GameObject GameObject { get; }
    }
}