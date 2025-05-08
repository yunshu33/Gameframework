using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor.Dialogue
{
    [IsDialogueNode("对话/内容和选择")]
    public class DialogueAndSelectNode : DialogueNodeBase, ISetData<DialogueAndSelectData>
    {
        private TextField dialogueTextField;

        private Dictionary<string, TextField> textFieldDic = new Dictionary<string, TextField>();

        public DialogueAndSelectNode()
        {
            title = "内容和选择";

            dialogueTextField = new TextField();
            dialogueTextField.multiline = true;
            dialogueTextField.style.minHeight = 50;

            contentContainer.Add(dialogueTextField);

            var toolBar = new Toolbar();
            var add = new ToolbarButton(AddOutputPort);
            add.text = "+";

            add.tooltip = "添加";

            add.style.width = 20;

            var subtract = new ToolbarButton(SubOutputPort);
            subtract.text = "-";

            subtract.style.width = 20;
            subtract.tooltip = "移除最后一个";

            toolBar.style.minWidth = 100;

            toolBar.style.flexDirection = FlexDirection.RowReverse;

            toolBar.Add(add);

            toolBar.Add(subtract);

            outputContainer.Add(toolBar);
        }

        private void SubOutputPort()
        {
            if (textFieldDic.Count > 0)
            {
                textFieldDic.Remove(textFieldDic.LastOrDefault().Key);

                var port = outputContainer.Children().ElementAt(outputContainer.childCount - 2) as DialoguePort;

                var conns = port.connections.ToList();

                for (int i = 0; i < conns.Count; i++)
                {
                    var input = conns[i].input;
                    input.Disconnect(conns[i]);
                    port.Disconnect(conns[i]);

                    conns[0].parent.Remove(conns[0]);

                    input.node.RefreshExpandedState();
                    input.node.RefreshPorts();
                }

                port.node.RefreshExpandedState();
                port.node.RefreshPorts();

                outputContainer.RemoveAt(outputContainer.childCount - 2);
            }
        }

        private void AddOutputPort()
        {
            var port = CreateOutputPort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null);
            outputContainer.Insert(outputContainer.childCount - 2, port);
        }

        public override Port CreateInputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {
            type = typeof(string);

            var port = InstantiatePort(orientation, direction, capacity, type) as DialoguePort;


            port.portName = "根节点";

            inputContainer.Add(port);

            return port;
        }

        public override Port CreateOutputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {
            return null;
        }

        public override NodeScriptableObjectBase SaveData()
        {
            if (Data is null)
            {
                Data = ScriptableObject.CreateInstance<DialogueAndSelectData>();

                Data.position = GetPosition().position;
            }
            else
            {
                Data.position = GetPosition().position;
            }

            return Data;
        }
    }
}