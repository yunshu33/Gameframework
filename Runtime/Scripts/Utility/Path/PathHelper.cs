using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Utility
{
    public static class PathHelper
    {
        /// <summary>
        /// 获得相对路径
        /// E:/XJC/My/Yun/Assets/YunTools/PrefabTest/002.prefab
        /// Application.dataPath
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="relativeRoot">相对于 的 路径</param>
        /// <returns></returns>
        public static string GetRelativePath(string absolutePath, string relativeRoot = null)
        {

            if (string.IsNullOrEmpty(relativeRoot))
                relativeRoot = Application.dataPath;

            var url = new Uri(absolutePath);

            var assetUrl = new Uri(relativeRoot);

            Uri relativeUri = assetUrl.MakeRelativeUri(url);

            return relativeUri.ToString();
        }

    }
}