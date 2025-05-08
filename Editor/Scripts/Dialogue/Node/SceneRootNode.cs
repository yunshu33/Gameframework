using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace LJVoyage.Game.Editor.Dialogue
{
    [IsDialogueNode("场景/场景根节点")]
    public class SceneRootNode : DialogueNodeBase, ISetData<SceneRootData>
    {
        public string sceneName = string.Empty;

        public string sceneTag = string.Empty;


        private TextField showTextField;

        private TextField tagTextField;

        private DialoguePort tagPort;

        public SceneRootNode()
        {
            title = "场景根节点";
        }

        public override Port CreateInputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {

            var port = InstantiatePort(orientation, direction, capacity, type) as DialoguePort;

            showTextField = new TextField();

            port.connectCallback = ConnectCallBack;

            port.disconnectCallback = DisconnectCallback;

            showTextField.isReadOnly = true;

            showTextField.style.width = 80;

            port.Add(showTextField);

            inputContainer.Add(port);

            return port;

        }

        private void DisconnectCallback(object arg1, string arg2)
        {
            showTextField.value = string.Empty;

            title = "场景根节点";
        }

        private void TagValueChangeCallback(ChangeEvent<string> evt)
        {
            tagPort.userData = evt.newValue;

            tagPort.UserDataValueChanged(tagPort.guid);
        }

        private void ConnectCallBack(object arg1, string arg2)
        {
            showTextField.value = arg1.ToString();

            title = $" 当前场景 {arg1}";

        }

        public override Port CreateOutputPort(Orientation orientation, Direction direction, Port.Capacity capacity,
            Type type)
        {
            tagPort = InstantiatePort(orientation, direction, capacity, type) as DialoguePort;

            tagTextField = new TextField();
            tagTextField.RegisterValueChangedCallback(TagValueChangeCallback);

            var a = new Label();

            // a.text = "标签:";

            tagTextField.style.width = 100;

            tagTextField.labelElement.style.minWidth = 30;
            tagTextField.labelElement.style.width = 30;

            tagTextField.label = "标签:";

            tagPort.Add(tagTextField);

            tagPort.Add(a);

            outputContainer.Add(tagPort);

            tagPort.userData = tagTextField.value;

            return tagPort;

        }

        public override NodeScriptableObjectBase SaveData()
        {
            if (Data is null)
            {
                Data = ScriptableObject.CreateInstance<SceneRootData>();

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