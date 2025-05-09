

namespace LJVoyage.Game.Runtime.Node
{
    [Node(nodeName = nameof(TestNode),tag = nameof(LJVoyage.Game))]
    public class TestNode : NodeBase
    { 
        [In]
        public int A;

        [In]
        public int B;

        [Out]
        public int C;
        
        public override object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}