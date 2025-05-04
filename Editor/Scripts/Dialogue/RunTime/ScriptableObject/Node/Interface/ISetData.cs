using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetData<out T> where T : NodeScriptableObjectBase
{
     NodeScriptableObjectBase Data { get; set; }
}
