using System;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Yun.NodeGraphView.Runtime.Node
{
    [Serializable]
    public class NodeBase : ScriptableObject, ICloneable
    {
        [HideInInspector] public string guid;

        public NodeBase()
        {
            guid = Guid.NewGuid().ToString();
        }

        public NodeBase(string guid)
        {
            this.guid = guid;
        }

        [HideInInspector] public Vector2 position;

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }

        public static NodeBase DeserializeNode(string type, string json)
        {
            var instance = CreateInstance(type) as NodeBase;

            JsonUtility.FromJsonOverwrite(json, instance);

            return instance;
        }
    }
}