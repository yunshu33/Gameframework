using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yun.Framework;

namespace LJVoyage.Game.Test
{
    /// <summary>
    /// 场景 树结构  节点
    /// </summary>
    public class SceneNode
    {
        /// <summary>
        /// 场景名称
        /// </summary>
        public string sceneName;

        /// <summary>
        /// Scene 的父Scene 存储以供 回溯
        /// </summary>
        public SceneNode fatherNode;

        /// <summary>
        /// 可拓展  ：单个场景 UI 层级管理 每次切换场景需要读取自生
        /// </summary>
        public UINode uiNode;
    }
}