using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LJVoyage.Game
{
    [Serializable]
    public class EdgeInfo
    {
        public string guid;

        public Info output = new Info();

        public Info input = new Info();
    }

    [Serializable]
    public class Info
    {
        [HideInInspector] public string nodeGuid;
        public string portFieldName;
    }
}