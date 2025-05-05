using System;
using System.Collections.Generic;
using System.Linq;
using LJVoyage.Game.Node;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class NodeGraph : GraphView
    {
        private readonly EditorWindow window;

        private readonly GraphViewScriptableObject scriptableObject;


        public readonly List<NodeBase> nodeBases;

        private readonly List<EdgeInfo> edgeInfos;

        public NodeGraph(EditorWindow window, GraphViewScriptableObject scriptableObject)
        {
            this.window = window;

            serializeGraphElements = SerializeGraphElementsCallback;
            unserializeAndPaste = UnserializeAndPaste;

            canPasteSerializedData = CanPasteSerializedDataCallback;

            this.scriptableObject = scriptableObject;

            nodeBases = scriptableObject.Nodes;

            edgeInfos = scriptableObject.EdgeInfos;

            RegisterCallback<KeyDownEvent>(KeyDownCallback);
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            graphViewChanged += GraphViewChangeCallback;

            // 允许拖拽Content
            this.AddManipulator(new ContentDragger());
            // 允许Selection里的内容
            this.AddManipulator(new SelectionDragger());
            // GraphView允许进行框选
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(new FreehandSelector());

            //this.AddManipulator(new ClickSelector());
            //背景 这个需要在 uss/styleSheet 中定义GridBackground类来描述类型
            // var grid = new GridBackground();
            //
            // Insert(0, grid);
            //
            // grid.StretchToParentSize();

            var searchWindowProvider = ScriptableObject.CreateInstance<SearchWindowProvider>();


            searchWindowProvider.Initialize(window, this);

            foreach (var node in nodeBases)
            {
                CreateNode(node);
            }

            foreach (var info in edgeInfos)
            {
                CreateEdge(info);
            }

            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            Undo.undoRedoPerformed += OnUndoRedo;
        }

        protected virtual bool CanPasteSerializedDataCallback(string data)
        {
            var list = JsonConvert.DeserializeObject<List<(string json, string type )>>(data);

            return list != null && list.Count != 0;
        }

        protected virtual void UnserializeAndPaste(string operationname, string data)
        {
            var list = JsonConvert.DeserializeObject<List<(string json, string type )>>(data);

            foreach (var item in list)
            {
                var node =   CreateNode(item.type, Vector2.zero);
                
                JsonUtility.FromJsonOverwrite(item.json,node);

                node.guid = Guid.NewGuid().ToString();
                
                CreateNode(node);
                
                scriptableObject.Nodes.Add(node);
            }
        }

        protected virtual string SerializeGraphElementsCallback(IEnumerable<GraphElement> elements)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            List<(string json, string type )> json = new List<(string json, string type)>();


            foreach (var element in elements)
            {
                switch (element)
                {
                    case NodeView node:

                        json.Add(
                            new ValueTuple<string, string>(JsonUtility.ToJson(node.Node), node.Node.GetType().Name));

                        break;
                    case EdgeView edge:
                        break;
                }
            }

            var str = JsonConvert.SerializeObject(json, settings);


            return str;
        }

        private void CreateEdge(EdgeInfo info)
        {
            var fr = ports
                .FirstOrDefault(p =>
                {
                    if (p.node is not NodeView node)
                        return false;

                    if (p is not PortView port)
                        return false;

                    return port.direction == Direction.Output && node.Node.guid == info.output.nodeGuid &&
                           port.portName == info.output.portFieldName;
                });

            var to = ports.FirstOrDefault(p =>
            {
                if (p.node is not NodeView node)
                    return false;

                if (p is not PortView port)
                    return false;

                return port.direction == Direction.Input && node.Node.guid == info.input.nodeGuid &&
                       port.portName == info.input.portFieldName;
            });

            if (fr == null || to == null)
            {
                Debug.LogError("错误edge");

                return;
            }

            var edge = new EdgeView(info)
            {
                output = fr,
                input = to
            };


            AddElement(edge);

            edge.input.Connect(edge);
            edge.output.Connect(edge);

            edge.input.node.RefreshPorts();

            edge.output.node.RefreshPorts();
        }

        private void KeyDownCallback(KeyDownEvent evt)
        {
            switch (evt.keyCode)
            {
                case KeyCode.S when evt.ctrlKey:

                    AssetDatabase.SaveAssets();
                    evt.StopPropagation();
                    Save();
                    break;
            }
        }

        private void Save()
        {
            window.titleContent.text = nameof(GraphViewWindow);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }

        private void OnUndoRedo()
        {
            throw new NotImplementedException();
        }


        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var list = ports.Where(t =>
                    t.direction != startPort.direction && t.portType == startPort.portType &&
                    !t.connections.Any() && startPort.node != t.node)
                .ToList();

            return list;
        }


        private GraphViewChange GraphViewChangeCallback(GraphViewChange graphViewChange)
        {
            if (!window.titleContent.text.Contains('*'))
            {
                window.titleContent.text += '*';
            }

            if (graphViewChange.edgesToCreate != null)
            {
                OnEdgeCreate(graphViewChange.edgesToCreate);
            }

            if (graphViewChange.elementsToRemove != null)
            {
                OnElementsToRemove(graphViewChange.elementsToRemove);
            }

            return graphViewChange;
        }


        /// <summary>
        /// /移除元素
        /// </summary>
        /// <param name="elementsToRemove"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnElementsToRemove(List<GraphElement> elementsToRemove)
        {
            foreach (var element in elementsToRemove)
            {
                switch (element)
                {
                    case NodeView node:

                        nodeBases.Remove(node.Node);

                        AssetDatabase.RemoveObjectFromAsset(node.Node);

                        AssetDatabase.SaveAssets();

                        AssetDatabase.Refresh();

                        break;

                    case EdgeView edgeView:

                        edgeInfos.Remove(edgeView.edgeInfo);

                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// 创建 edge 回调 new出来的edge 不会 触发 graphViewChange Callback
        /// </summary>
        /// <param name="edges"></param>
        private void OnEdgeCreate(List<Edge> edges)
        {
            foreach (var edge in edges)
            {
                if (edge is not EdgeView edgeView)
                    continue;

                var info = edgeView.edgeInfo;

                if (edge.output is PortView output)
                {
                    info.output.nodeGuid = (output.node as NodeView)?.Node.guid;
                    info.output.portFieldName = output.FieldInfo.Name;
                }

                if (edge.input is PortView input)
                {
                    info.input.nodeGuid = (input.node as NodeView)?.Node.guid;
                    info.input.portFieldName = input.FieldInfo.Name;
                }

                edgeInfos.Add(info);
            }
        }

        public NodeBase CreateNode(NodeBase nodeBase)
        {
            var node = new NodeView(nodeBase);

            node.SetPosition(new Rect(nodeBase.position, new Vector2(500, 500)));

            var type = nodeBase.GetType();

            node.title = type.Name;

            CreateInPort(node, type);

            CreateOutPort(node, type);

            AddElement(node);

            return nodeBase;
        }

        private NodeBase CreateNode(string className, Vector2 position)
        {
            GraphViewChangeCallback(new GraphViewChange());

            // var obj = Activator.CreateInstance(type) as NodeBase;
            var obj = ScriptableObject.CreateInstance(className) as NodeBase;

            if (obj == null)
                throw new Exception();

            obj.name = className;

            AssetDatabase.AddObjectToAsset(obj, scriptableObject);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            if (obj == null)
                throw new NullReferenceException();

            obj.position = position;

            return CreateNode(obj);
        }

        public NodeBase CreateNode(Type type, Vector2 position)
        {
            return CreateNode(type.Name, position);
        }

        private NodeView CreateOutPort(NodeView node, Type type)
        {
            foreach (var fieldInfo in type.GetFields())
            {
                var first = fieldInfo.CustomAttributes.FirstOrDefault(attr =>
                    attr.AttributeType == typeof(OutAttribute));

                if (first == null) continue;

                var port = PortView.Create<EdgeView>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                    fieldInfo);
                port.portName = fieldInfo.Name;

                node.outputContainer.Add(port);
            }

            return node;
        }

        private NodeView CreateInPort(NodeView node, Type type)
        {
            foreach (var fieldInfo in type.GetFields())
            {
                var first = fieldInfo.CustomAttributes.FirstOrDefault(attr =>
                    attr.AttributeType == typeof(InAttribute));

                if (first != null)
                {
                    var port = PortView.Create<EdgeView>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                        fieldInfo);

                    port.portName = fieldInfo.Name;

                    node.inputContainer.Add(port);
                }
            }

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGraph()
        {
        }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);

            BuildSaveAssetContextualMenu(evt);
        }


        /// <summary>
        /// Add the Save Asset entry to the context menu
        /// </summary>
        /// <param name="evt"></param>
        protected virtual void BuildSaveAssetContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Save Asset", e =>
            {
                Save();
                AssetDatabase.SaveAssets();
            }, DropdownMenuAction.AlwaysEnabled);
        }
    }
}