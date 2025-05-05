using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.GameEditor
{
    public class DialogueSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private DialogueGraphViewEditorWindow window;

        private DialogueGraphView graphView;

        public List<DialogueNodeBase> nodes = new();

        private DialogueGraphViewEditorWindow editorWindow;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("创建节点"))
            };

            //先创建 一个组 SearchTreeGroupEntry  在创建条目  SearchTreeEntry  自动向下匹配

            var treePaths = new List<TreePath>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in GetTypesOrNothing(assembly))
                {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(DialogueNodeBase)))
                    {
                        var attr =
                            type.GetCustomAttribute(typeof(IsDialogueNodeAttribute), false) as IsDialogueNodeAttribute;

                        treePaths.Add(new TreePath()
                        {
                            path = attr?.GetPath(),
                            type = type,
                        });
                    }
                }
            }
            // treePaths = TreePath.Sort(treePaths);

            treePaths.Sort((A, B) =>
            {
                for (var i = 0; i < A.path.Length; i++)
                {
                    if (i >= B.path.Length)
                        return 1;
                    var value = String.Compare(A.path[i], B.path[i], StringComparison.Ordinal);
                    if (value != 0)
                    {
                        // Make sure that leaves go before nodes
                        if (A.path.Length != B.path.Length && (i == A.path.Length - 1 || i == B.path.Length - 1))
                            return A.path.Length < B.path.Length ? -1 : 1;
                        return value;
                    }
                }

                return 0;
            });

            var groups = new List<string>();

            foreach (var path in treePaths)
            {
                // `createIndex` represents from where we should add new group entries from the current entry's group path.
                var createIndex = int.MaxValue;

                // Compare the group path of the current entry to the current group path.
                for (var i = 0; i < path.path.Length - 1; i++)
                {
                    var group = path.path[i];
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
                for (var i = createIndex; i < path.path.Length - 1; i++)
                {
                    var group = path.path[i];
                    groups.Add(group);
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(group)) { level = i + 1 });
                }

                // Finally, add the actual entry.
                tree.Add(new SearchTreeEntry(new GUIContent(path.path.Last()))
                    { level = path.path.Length, userData = path.type });
            }


            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var type = SearchTreeEntry.userData as Type;

            var instance = Activator.CreateInstance(type);

            var node = instance as DialogueNodeBase;

            var windowMousePosition = context.screenMousePosition - editorWindow.position.position;

            node.position = graphView.contentViewContainer.WorldToLocal(windowMousePosition);

            node.SetPosition(new Rect(node.position, new Vector2(500, 500)));

            graphView.AddElement(node);

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

        internal void SetConfig(DialogueGraphView dialogueGraphView,
            DialogueGraphViewEditorWindow dialogueGraphViewEditorWindow)
        {
            this.graphView = dialogueGraphView;
            this.editorWindow = dialogueGraphViewEditorWindow;
        }

        class TreePath
        {
            public string[] path;
            public Type type;

        }
    }
}