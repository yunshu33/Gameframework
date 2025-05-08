
using UnityEngine;

namespace LJVoyage.Game.Editor.Dialogue
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


