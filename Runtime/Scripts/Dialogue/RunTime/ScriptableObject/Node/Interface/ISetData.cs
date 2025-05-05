using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game
{


     public interface ISetData<out T> where T : NodeScriptableObjectBase
     {
          NodeScriptableObjectBase Data { get; set; }
     }
}