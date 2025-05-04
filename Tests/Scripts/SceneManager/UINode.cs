using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yun.Framework
{
    /// <summary>
    /// ui 树结构  节点
    /// </summary>
    public class UINode
    {
        /// <summary>
        /// ui名称
        /// </summary>
        public string uiName;

        /// <summary>
        /// ui 的父uiNode
        /// </summary>
        public UINode fatherNode;
    }
}

