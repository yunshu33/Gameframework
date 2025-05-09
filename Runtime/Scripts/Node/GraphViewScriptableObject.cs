using System;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Node
{
    [CreateAssetMenu(menuName = "LJVoyage/GraphView/ScriptableObject")]
    
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