using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using Yun.NodeGraphView.Runtime;

namespace LJVoyage.GameEditor
{
    public class GraphViewWindow : EditorWindow
    {
        static GraphViewWindow instance;

        NodeGraph graph;

        private GraphViewScriptableObject scriptableObject;


    

        static void Init(GraphViewScriptableObject obj)
        {
            if (instance == null)
            {
                instance = GetWindow<GraphViewWindow>();
            }

            if (instance.scriptableObject == null)
            {
                instance.scriptableObject = obj; 
            }
           
            instance.titleContent.text = nameof(GraphViewWindow); 

            instance.Show(); 
        }

        private void OnEnable()
        {
            instance = this;

            if (scriptableObject == null  )
            {
                if (Selection.activeObject is GraphViewScriptableObject)
                {
                    scriptableObject = Selection.activeObject as GraphViewScriptableObject;
                }
                else
                {
                    throw new NullReferenceException();
                }
                
            }
            
            graph = new NodeGraph(this, scriptableObject);

            // 让graphView铺满整个Editor窗口
            graph.StretchToParentSize();

            // 把它添加到EditorWindow的可视化Root元素下面
            rootVisualElement.Add(graph);
        }


        [OnOpenAsset]
        public static bool OpenAsset(int instanceID, int line, int column)
        {
            if ( EditorUtility.InstanceIDToObject(instanceID)  is GraphViewScriptableObject)
            {
                Init(Selection.activeObject as GraphViewScriptableObject);

                return true;
            }

            return false;
        }
    }
}