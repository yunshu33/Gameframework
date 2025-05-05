#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		SceneRouterExample.cs
// Author Name :		YunShu
// Create Time :		2022/07/07 15:58:05
// Description :
// **********************************************************************
#endregion


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.RunTime.Utility;

namespace LJVoyage.Game.Test
{
    public class SceneRouterExample : MonoBehaviour
    {
        [SceneName]
        public string toSceneName;
    }
}