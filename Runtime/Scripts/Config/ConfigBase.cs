using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJVoyage.Game.Runtime
{


    public class ConfigBase : ScriptableObject
    {
        [SerializeField]
        [Header("是否自动初始化")]
        private bool auto = false;

        public bool Auto { get => auto; private set => auto = value; }

        
    }

}