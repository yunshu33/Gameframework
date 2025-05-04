using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Yun.NodeGraphView.Runtime.Node;

namespace Yun.NodeGraphView.Runtime
{
    [CreateAssetMenu(menuName = "Y/GraphView/ScriptableObject")]
    
    [Serializable]
    public class GraphViewScriptableObject : ScriptableObject
    {
       
        [SerializeField]
        private List<NodeBase> nodes;

      
        [SerializeField]
        private List<EdgeInfo> edgeInfos;

        public List<NodeBase> Nodes
        {
            get => nodes;
            set => nodes = value;
        }

        public List<EdgeInfo> EdgeInfos
        {
            get => edgeInfos;
            set => edgeInfos = value;
        }
    }
}