#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		ToolMagazine.cs
// Author Name :		YunShu
// Create Time :		2022/05/12 15:07:34
// Description :        工具集
// **********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yun.Util
{
    /// <summary>
    /// 工具集
    /// </summary>
    public static class ToolMagazine
    {
        /// <summary>
        /// 无重复随机数
        /// </summary>
        /// <param name="max">最大值 不包含 </param>
        /// <param name="min">最小值 包含</param>
        /// <param name="count">数量</param>
        /// <returns>随机数哈希表</returns>

        public static HashSet<int> GetNoRepeatRandom(int max , int min ,int count )
        {
            if (count > max - min)
            {
                return null;
            }

            HashSet<int> list = new HashSet<int>();

            for (int i = 0; i < count; )
            {
                if (list.Add(Random.Range(min,max)))
                {
                    i++;
                }
            }
            return list;
        }
    }
}
