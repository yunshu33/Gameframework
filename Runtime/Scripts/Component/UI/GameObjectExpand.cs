#region Copyright
// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		GameObjectExpand.cs
// Author Name :		YunShu
// Create Time :		2023/09/22 13:53:36
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  LJVoyage.Game
{
    public static class GameObjectExpand 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GameObject SetActiveOptimize(this GameObject gameObject, bool value)
        {
            if (gameObject.activeSelf != value)
                gameObject.SetActive(value);

            return gameObject;
        }
    }
}