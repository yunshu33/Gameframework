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


using UnityEngine;

namespace LJVoyage.Game
{
    public abstract class SystemBase : MonoBehaviour, ISystem
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
        public abstract void InitConfig();

        public GameObject GameObject => gameObject;
    }
}