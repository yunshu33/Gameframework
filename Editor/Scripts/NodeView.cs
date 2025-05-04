using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Yun.NodeGraphView.Runtime.Node;

namespace Yun.NodeGraphView.Editor
{
    public class NodeView : Node
    {
        private readonly NodeBase node;

        public NodeBase Node => node;

        public NodeView(NodeBase node)
        {
            this.node = node;
            
        }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            
            
            
            base.BuildContextualMenu(evt);
        }


        public override void UpdatePresenterPosition()
        {
            base.UpdatePresenterPosition();
            node.position = GetPosition().position;
        }
    }
}