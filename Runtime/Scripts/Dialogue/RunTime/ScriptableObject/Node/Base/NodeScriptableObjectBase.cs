
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Graph
{
    public class NodeScriptableObjectBase : ScriptableObject
    {
        public string guid;

        public Vector2 position;


        public List<PortData> inputs = new List<PortData>();

        public List<PortData> outputs = new List<PortData>();
    }
}