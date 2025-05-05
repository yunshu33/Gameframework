
using UnityEngine;

namespace LJVoyage.GameEditor
{

    public class ChildScriptableObject : ScriptableObject
    {
        [SerializeField] string str;

        void OnEnable()
        {
            name = "New ChildScriptableObject";
        }


    }

}


