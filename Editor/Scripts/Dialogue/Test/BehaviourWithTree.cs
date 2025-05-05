using System.Collections.Generic;
using System;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    public class BehaviourWithTree : MonoBehaviour, ISerializationCallbackReceiver
    {
        // 在运行时使用的 Node 类。
        //此类位于 BehaviourWithTree 类的内部，不会被序列化。
        public class Node
        {
            public string interestingValue = "value";
            public List<Node> children = new List<Node>();
        }

        // 我们将用于序列化的 Node 类。
        [Serializable]
        public struct SerializableNode
        {
            public string interestingValue;
            public int childCount;
            public int indexOfFirstChild;
        }

        //用于运行时树表示的根节点。不序列化。
        Node root = new Node();

        //这是我们提供给 Unity 进行序列化的字段。
        public List<SerializableNode> serializedNodes;

        public void OnBeforeSerialize()
        {
            //Unity 即将读取 serializedNodes 字段的内容。
            // 现在必须"及时"将正确的数据写入该字段。
            if (serializedNodes == null) serializedNodes = new List<SerializableNode>();
            if (root == null) root = new Node();
            serializedNodes.Clear();
            AddNodeToSerializedNodes(root);
            // 现在 Unity 可自由地序列化这个字段，我们应该在稍后反序列化时
            // 找回预期的数据。
        }

        void AddNodeToSerializedNodes(Node n)
        {
            var serializedNode = new SerializableNode()
                {
                    interestingValue = n.interestingValue,
                    childCount = n.children.Count,
                    indexOfFirstChild = serializedNodes.Count + 1
                }
                ;
            serializedNodes.Add(serializedNode);
            foreach (var child in n.children)
                AddNodeToSerializedNodes(child);
        }

        public void OnAfterDeserialize()
        {
            //Unity 刚刚将新数据写入 serializedNodes 字段。
            //让我们用这些新值填充我们的实际运行时数据。
            if (serializedNodes.Count > 0)
            {
                ReadNodeFromSerializedNodes(0, out root);
            }
            else
                root = new Node();
        }

        int ReadNodeFromSerializedNodes(int index, out Node node)
        {
            var serializedNode = serializedNodes[index];
            //将反序列化的数据传输到内部 Node 类
            Node newNode = new Node()
                {
                    interestingValue = serializedNode.interestingValue,
                    children = new List<Node>()
                }
                ;
            // 以深度优先的方式读取树，因为这正是我们写入树的方式。
            for (int i = 0; i != serializedNode.childCount; i++)
            {
                Node childNode;
                index = ReadNodeFromSerializedNodes(++index, out childNode);
                newNode.children.Add(childNode);
            }

            node = newNode;
            return index;
        }

        // 此 OnGUI 在 Game 视图中绘制出节点树，其中包含用于添加新节点作为子项的按钮。
        void OnGUI()
        {
            if (root != null)
                Display(root);
        }

        void Display(Node node)
        {
            GUILayout.Label("Value: ");
            // 允许修改节点的"有用值"。
            node.interestingValue = GUILayout.TextField(node.interestingValue, GUILayout.Width(200));
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            foreach (var child in node.children)
                Display(child);
            if (GUILayout.Button("Add child"))
                node.children.Add(new Node());
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}