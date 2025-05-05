using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.GameEditor
{
    public class DialogueGraphView : GraphView
    {

        /// <summary>
        /// 搜索 树
        /// </summary>
        private DialogueSearchWindowProvider _searchWindowProvider;

        private EditorWindow editorWindow;

        private DialogueGraphViewData data;

        private string Path;

        public DialogueGraphView(DialogueGraphViewEditorWindow editorWindow, DialogueGraphViewData data)
        {
            this.editorWindow = editorWindow;
            this.data = data;

            var a = EditorGUIUtility.Load("Assets/UIToolkit/Dialogue/Editor/Uss/GridBackgroundUSS.uss");

            Debug.Log(a is null);

            Path = Selection.assetGUIDs[0];

            InitToolbar();


            var styleSheet = a as StyleSheet;

            if (styleSheet != null) styleSheets.Add(styleSheet);

            this.graphViewChanged += GraphViewChangeCallback;

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            // 允许拖拽Content
            this.AddManipulator(new ContentDragger());

            // 允许Selection里的内容
            this.AddManipulator(new SelectionDragger());

            // GraphView允许进行框选
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(new FreehandSelector());

            //背景 这个需要在 uss/styleSheet 中定义GridBackground类来描述类型
            var grid = new GridBackground();

            Insert(0, grid);

            grid.StretchToParentSize();

            _searchWindowProvider = ScriptableObject.CreateInstance<DialogueSearchWindowProvider>();

            _searchWindowProvider.SetConfig(this, editorWindow);

            nodeCreationRequest = (e) =>
            {
                SearchWindow.Open(new SearchWindowContext(e.screenMousePosition), _searchWindowProvider);
            };

            InitNodeData(data);

        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        private void InitToolbar()
        {
            var toolbar = new Toolbar();

            var button = new ToolbarButton(SaveData);

            button.text = "保存";

            toolbar.Add(button);

            Add(toolbar);
        }

        private void SaveData()
        {
            foreach (var item in nodes.ToList())
            {
                var node = (DialogueNodeBase)item;

                var data = node.SaveData();

                this.data.nodes.Add(data);

                //创建父子关系
                AssetDatabase.AddObjectToAsset(data, Path);

                AssetDatabase.SaveAssets();

                //更新
                AssetDatabase.ImportAsset(Path);
            }

        }

        private GraphViewChange GraphViewChangeCallback(GraphViewChange graphViewChange)
        {
            if (!editorWindow.titleContent.text.Contains('*'))
            {
                editorWindow.titleContent.text += '*';
            }

            return graphViewChange;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {


            var portBase = startPort as DialoguePort;

            var list = ports.ToList().Where(
                endPort => endPort.direction != startPort.direction &&
                           endPort.node != startPort.node
            ).ToList();

            if (portBase is null || portBase.types.Count == 0)
            {
                return list;
            }
            else
            {
                return list.Where(
                    endPort => portBase.types.Contains(endPort.node.GetType())
                ).ToList();
            }

        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {

            base.BuildContextualMenu(evt);
        }


        private void InitNodeData(DialogueGraphViewData data)
        {

            var nodeTypes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {

                foreach (var type in assembly.GetTypes())
                {

                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(DialogueNodeBase)))
                    {
                        var attr =
                            type.GetCustomAttribute(typeof(IsDialogueNodeAttribute), false) as IsDialogueNodeAttribute;

                        nodeTypes.Add(type);
                    }
                }
            }


            foreach (var item in data.nodes)
            {

            }
        }


    }
}