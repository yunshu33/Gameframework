using System;
using UnityEditor.Experimental.GraphView;
using Yun.NodeGraphView.Runtime;

namespace LJVoyage.GameEditor
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