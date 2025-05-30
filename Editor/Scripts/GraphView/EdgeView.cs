using System;
using LJVoyage.Game.Runtime;
using UnityEditor.Experimental.GraphView;

namespace LJVoyage.Game.Editor.Graph
{
    public class EdgeView : Edge
    {
        public EdgeInfo edgeInfo;

        public EdgeView()
        {
            edgeInfo = new EdgeInfo
            {
                guid = Guid.NewGuid().ToString()
            };
        }

        public EdgeView(EdgeInfo info)
        {
            this.edgeInfo = info;
        }
    }
}