using System;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Graph
{
    [CreateAssetMenu(menuName = "Dialogue/GraphView", fileName = "GraphView")]
    public class DialogueGraphViewData : ScriptableObject
    {
        public List<ConnectEdge> connectEdges = new List<ConnectEdge>();

        public List<NodeScriptableObjectBase> nodes = new List<NodeScriptableObjectBase>();
    }

    [System.Serializable]
    public struct InputNode
    {
        public int guid;
        public ConnectEdge edge;
    }

    /// <summary>
    /// 连接线
    /// </summary>
    [Serializable]
    public class ConnectEdge
    {
        public string fromPortGuid;
        public string toPortGuid;
    }

    [System.Serializable]
    public class PortData
    {
        public string guid;
        public string fromNodeGuid;
        public string toNodeGuid;
    }
}