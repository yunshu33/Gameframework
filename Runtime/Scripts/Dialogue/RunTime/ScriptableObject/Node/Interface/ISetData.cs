using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime.Graph
{
     public interface ISetData<out T> where T : NodeScriptableObjectBase
     {
          NodeScriptableObjectBase Data { get; set; }
     }
}