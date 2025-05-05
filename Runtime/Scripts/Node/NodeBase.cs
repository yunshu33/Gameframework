using System;
using UnityEngine;

namespace LJVoyage.Game.Node
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