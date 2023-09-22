#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		SystemBase.cs
// Author Name :		YunShu
// Create Time :		2023/09/18 11:58:17
// Description :
// **********************************************************************

#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorldFramework.RunTime
{
    public abstract class SystemBase : MonoBehaviour , ISystem
    {
        protected GameWorld gameWorld;

        protected virtual void Awake()
        {
            gameWorld = GameWorld.Instance;

            gameWorld.AddSystem(this);
        }

        /// <summary>
        /// 在 Awake 中调用
        /// </summary>
        public virtual void InitConfig()
        {
            
        }
    }
}