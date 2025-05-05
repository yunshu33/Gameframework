using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    /// <summary>
    /// 对话节点特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IsDialogueNodeAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string description;

        /// <summary>
        /// 路径
        /// </summary>
        public string path;

        /// <summary>
        /// 下标
        /// </summary>
        public int index;

        public IsDialogueNodeAttribute(string path)
        {
            this.path = path;
        }

        public string[] GetPath()
        {
            return path.Split('/');
        }
    }
}