using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace LJVoyage.Game.Editor.Graph
{
    public class EdgeConnectorListener : IEdgeConnectorListener
    {
        private readonly GraphViewChange m_GraphViewChange;

        private readonly List<Edge> m_EdgesToCreate;

        private readonly List<GraphElement> m_EdgesToDelete;

        public GraphView graphView;

        public EdgeConnectorListener()
        {
            m_EdgesToCreate = new List<Edge>();
            m_EdgesToDelete = new List<GraphElement>();
            m_GraphViewChange.edgesToCreate = m_EdgesToCreate;
        }

        /// <summary>
        /// 在空白空间放置边缘时调用。
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="position"></param>
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }

        /// <summary>
        /// 在端口放置新的边缘时调用。
        /// </summary>
        /// <param name="graphView"></param>
        /// <param name="edge"></param>
        public void OnDrop(GraphView graphView, Edge edge)
        {
            m_EdgesToCreate.Clear();
            m_EdgesToCreate.Add(edge);
            m_EdgesToDelete.Clear();
            if (edge.input.capacity == Port.Capacity.Single)
            {
                foreach (Edge connection in edge.input.connections)
                {
                    if (connection != edge)
                    {
                        m_EdgesToDelete.Add(connection);
                    }
                }
            }

            if (edge.output.capacity == Port.Capacity.Single)
            {
                foreach (Edge connection2 in edge.output.connections)
                {
                    if (connection2 != edge)
                    {
                        m_EdgesToDelete.Add(connection2);
                    }
                }
            }

            if (m_EdgesToDelete.Count > 0)
            {
                graphView.DeleteElements(m_EdgesToDelete);
            }

            List<Edge> edgesToCreate = m_EdgesToCreate;
            if (graphView.graphViewChanged != null)
            {
                edgesToCreate = graphView.graphViewChanged(m_GraphViewChange).edgesToCreate;
            }

            foreach (Edge item in edgesToCreate)
            {
                graphView.AddElement(item);
                edge.input.Connect(item);
                edge.output.Connect(item);
            }
        }
    }
}