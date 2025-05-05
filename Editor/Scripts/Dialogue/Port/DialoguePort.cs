using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class DialoguePort : Port
    {
        public List<Type> types = new List<Type>();

        public string guid = string.Empty;

        /// <summary>
        /// 连接时调用
        ///  object : userData  string : input.guid
        /// </summary>
        public Action<object, string> connectCallback;

        /// <summary>
        /// 断开连接调用
        ///  object : userData  string : input.guid
        /// </summary>
        public Action<object, string> disconnectCallback;

        protected DialoguePort(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type) :
            base(portOrientation, portDirection, portCapacity, type)
        {
            guid = Guid.NewGuid().ToString();
        }

        public override void Connect(Edge edge)
        {
            var input = edge.input as DialoguePort;

            var output = edge.output as DialoguePort;

            if (input == null || output == null)
                throw new Exception("端口未继承 DialoguePort");

            // 数据向下流动  只需要 调用 接收方 的回调
            if (edge.output.userData != null && input.connectCallback != null)
                input.connectCallback(output.userData, input.guid);

            // output.SetUserData(edge.input.userData,output.guid);
            base.Connect(edge);
        }

        public override void Disconnect(Edge edge)
        {
            var input = edge.input as DialoguePort;

            var output = edge.output as DialoguePort;

            if (input == null || output == null)
                throw new Exception("端口未继承 DialoguePort");

            //只需要调用接收方 的回调
            if (edge.output.userData != null && input.disconnectCallback != null)
                input.disconnectCallback(edge.output.userData, input.guid);


            base.Disconnect(edge);
        }


        public void UserDataValueChanged(string guid)
        {
            foreach (var item in connections)
            {
                var output = item.output as DialoguePort;
                var input = item.input as DialoguePort;
                if (output == null || input == null)
                    throw new Exception($"{item.GetType()} 未继承 DialoguePort");

                input.connectCallback(output.userData, input.guid);
            }
        }

        public new static DialoguePort Create<TEdge>(Orientation orientation, Direction direction, Capacity capacity,
            Type type) where TEdge : Edge, new()
        {
            DefaultEdgeConnectorListener listener = new DefaultEdgeConnectorListener();

            DialoguePort port = new DialoguePort(orientation, direction, capacity, type)
            {
                m_EdgeConnector = new EdgeConnector<TEdge>(listener)
            };

            port.AddManipulator(port.m_EdgeConnector);

            return port;
        }

        public class DefaultEdgeConnectorListener : IEdgeConnectorListener
        {
            private GraphViewChange m_GraphViewChange;

            private List<Edge> m_EdgesToCreate;

            private List<GraphElement> m_EdgesToDelete;

            public DefaultEdgeConnectorListener()
            {
                m_EdgesToCreate = new List<Edge>();
                m_EdgesToDelete = new List<GraphElement>();
                m_GraphViewChange.edgesToCreate = m_EdgesToCreate;
            }

            public void OnDropOutsidePort(Edge edge, Vector2 position)
            {
            }

            public void OnDrop(GraphView graphView, Edge edge)
            {
                m_EdgesToCreate.Clear();
                m_EdgesToCreate.Add(edge);
                m_EdgesToDelete.Clear();
                if (edge.input.capacity == Capacity.Single)
                {
                    foreach (Edge connection in edge.input.connections)
                    {
                        if (connection != edge)
                        {
                            m_EdgesToDelete.Add(connection);
                        }
                    }
                }

                if (edge.output.capacity == Capacity.Single)
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
}