using System;
using System.Collections.Generic;
using LJVoyage.Game.Runtime.Graph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



namespace LJVoyage.Game.Editor.Dialogue
{

    [IsDialogueNode("LJVoyage/Scene/场景列表")]
    public class SceneListNode : DialogueNodeBase, ISetData<SceneListData>
    {

        public List<string> names;

        public SceneListNode()
        {
            title = "场景列表";
        }

        public override Port CreateInputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {
            return null;
        }

        public override Port CreateOutputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {

            names = GetAllSceneName();
            type = typeof(string);

            foreach (var item in names)
            {

                var port = InstantiatePort(orientation, direction, capacity, type);

                port.title = item.ToString();

                port.portName = item.ToString();

                port.userData = item.ToString();

                outputContainer.Add(port);


            }

            return null;
        }

        public override NodeScriptableObjectBase SaveData()
        {
            if (Data is null)
            {
                Data = ScriptableObject.CreateInstance<SceneListData>();

                Data.position = GetPosition().position;

            }
            else
            {
                Data.position = GetPosition().position;
            }

            return Data;
        }



        /// <summary>
        /// 获得所有场景名称
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllSceneName()
        {
            var list = new List<string>();
            foreach (var item in AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" }))
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(item);

                string name = scenePath.Substring(scenePath.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);

                list.Add(name);
            }

            return list;
        }

       
    }
}