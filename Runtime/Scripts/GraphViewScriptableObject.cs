using System;
using System.Collections.Generic;
using LJVoyage.Game.Node;
using UnityEngine;

namespace LJVoyage.Game
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