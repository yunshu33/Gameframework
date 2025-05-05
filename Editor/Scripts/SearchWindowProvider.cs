using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LJVoyage.Game.Node;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor
{
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private NodeGraph graph;

        private EditorWindow window;

        public void Initialize(EditorWindow window, NodeGraph graph)
        {
            this.window = window;
            this.graph = graph;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("创建节点"))
            };

            //先创建 一个组 SearchTreeGroupEntry  在创建条目  SearchTreeEntry  自动向下匹配

            var pathTrees = new List<PathTree>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in GetTypesOrNothing(assembly))
                {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(NodeBase)))
                    {
                        var attr = type.GetCustomAttribute(typeof(NodeAttribute), false) as NodeAttribute;

                        pathTrees.Add(new PathTree
                        {
                            path = attr?.GetPath(),
                            name = attr?.nodeName,
                            type = type,
                        });
                    }
                }
            }
            // treePaths = TreePath.Sort(treePaths);

            pathTrees.Sort((A, B) =>
            {
                for (var i = 0; i < A.path?.Length; i++)
                {
                    if (i >= B.path?.Length)
                        return 1;
                    if (B?.path != null)
                    {
                        var value = String.Compare(A?.path[i], B?.path[i], StringComparison.Ordinal);
                        if (value != 0)
                        {
                            // 确保叶子节点在节点之前
                            if (A?.path.Length != B?.path.Length &&
                                (i == A?.path.Length - 1 || i == B?.path.Length - 1))
                                return A?.path.Length < B?.path.Length ? -1 : 1;
                            return value;
                        }
                    }
                }

                return 0;
            });

            var groups = new List<string>();

            foreach (var pathTree in pathTrees)
            {
                // `createIndex` represents from where we should add new group entries from the current entry's group path.
                var createIndex = int.MaxValue;

                // Compare the group path of the current entry to the current group path.
                for (var i = 0; i < pathTree.path?.Length - 1; i++)
                {
                    var group = pathTree.path[i];
                    if (i >= groups.Count)
                    {
                        // The current group path matches a prefix of the current entry's group path, so we add the
                        // rest of the group path from the currrent entry.
                        createIndex = i;
                        break;
                    }

                    if (groups[i] != group)
                    {
                        // A prefix of the current group path matches a prefix of the current entry's group path,
                        // so we remove everyfrom from the point where it doesn't match anymore, and then add the rest
                        // of the group path from the current entry.
                        groups.RemoveRange(i, groups.Count - i);
                        createIndex = i;
                        break;
                    }
                }

                // Create new group entries as needed.
                // If we don't need to modify the group path, `createIndex` will be `int.MaxValue` and thus the loop won't run.
                for (var i = createIndex; i < pathTree.path?.Length - 1; i++)
                {
                    var group = pathTree.path[i];
                    groups.Add(group);
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(group)) { level = i + 1 });
                }

                // Finally, add the actual entry.
                tree.Add(new SearchTreeEntry(new GUIContent(pathTree.name))
                    { level = pathTree.path?.Length ?? 1, userData = pathTree.type });
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            // window to graph position
            var windowRoot = window.rootVisualElement;
            
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent,
                context.screenMousePosition - window.position.position);
            
            var graphMousePosition = graph.contentViewContainer.WorldToLocal(windowMousePosition);

            var type = SearchTreeEntry.userData as Type;

            var node = graph.CreateNode(type, graphMousePosition);

            graph.nodeBases.Add(node);

            return true;
        }

        public IEnumerable<Type> GetTypesOrNothing(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch
            {
                return Enumerable.Empty<Type>();
            }
        }

        class PathTree
        {
            public string name;
            public string[] path;
            public Type type;
        }
    }
}