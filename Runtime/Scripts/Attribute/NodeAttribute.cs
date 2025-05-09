using System;

namespace LJVoyage.Game.Runtime
{
    public class NodeAttribute : Attribute
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string nodeName;

        /// <summary>
        /// 标签
        /// </summary>
        public string tag;

        /// <summary>
        /// 路径
        /// </summary>
        public string path;

        public string[] GetPath()
        {
            if (path == null)
                return null;
            
            return path.Split('/');
        }
    }
}