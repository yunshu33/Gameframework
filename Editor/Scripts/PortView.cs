using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Yun.NodeGraphView.Editor
{
    public class PortView : Port
    {
        private readonly FieldInfo fieldInfo;

        public FieldInfo FieldInfo => fieldInfo;
        
        protected PortView(Orientation portOrientation, Direction portDirection, Capacity portCapacity,
            FieldInfo fieldInfo) :
            base(portOrientation, portDirection, portCapacity, fieldInfo.FieldType)
        {
            this.fieldInfo = fieldInfo;
        }

        public static PortView Create<TEdge>(Orientation orientation, Direction direction, Capacity capacity,
            FieldInfo fieldInfo) where TEdge : Edge, new()
        {
            var listener = new EdgeConnectorListener();

            var port = new PortView(orientation, direction, capacity, fieldInfo)
            {
                m_EdgeConnector = new EdgeConnector<TEdge>(listener)
            };

            port.AddManipulator(port.m_EdgeConnector);

            return port;
        }

        
    }
}