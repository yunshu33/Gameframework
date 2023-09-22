#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		NewBehaviourScript.cs
// Author Name :		YunShu
// Create Time :		2022/11/29 15:28:52
// Description :
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorldFramework.RunTime.Utility
{
    public static class RandomHelper
    {
        /// <summary>
        /// 获得无重复随机数 （min,max]
        /// </summary>
        /// <param name="max">最大值 不包含</param>
        /// <param name="min">最小值 包含</param>
        /// <param name="count">数量</param>
        /// <returns>随机数哈希表</returns>
        public static HashSet<int> GetNoRepeatRandom(int max, int min, int count)
        {
            HashSet<int> list = new HashSet<int>();

            for (int i = 0; i < count;)
            {
                if (list.Add(Random.Range(min, max)))
                {
                    i++;
                }
            }
            return list;
        }
    }
}
