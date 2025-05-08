using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LJVoyage.Game.Editor.Dialogue
{
    /// <summary>
    /// 允许连接的类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ConnectAttribute : Attribute
    {
        /// <summary>
        /// 允许连接的类型
        /// </summary>
        public List<Type> types = new List<Type>();

        /// <summary>
        /// 类型
        /// </summary>
        /// <param name="types"></param>
        public ConnectAttribute(params Type[] types)
        {
            this.types.AddRange(types);
        }
    }
}