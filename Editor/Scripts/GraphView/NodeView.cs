
using LJVoyage.Game.Runtime.Node;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private readonly NodeBase node;

        public NodeBase Node => node;

        public NodeView( NodeBase node)
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