#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		AssemblyLister.cs
// Author Name :		YunShu
// Create Time :		2022/07/26 13:50:43
// Description :
// **********************************************************************
#endregion


using System;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace LJVoyage.Game.Editor.Tools
{
    public static class AssemblyLister
    {
        [MenuItem("YunTools/List Player Assemblies in Console")]
        public static void PrintAssemblyNames()
        {
            UnityEngine.Debug.Log("== Player Assemblies ==");
            Assembly[] playerAssemblies =
                CompilationPipeline.GetAssemblies(AssembliesType.Editor);

            foreach (var assembly in playerAssemblies)
            {
                Debug.Log(assembly.name);
            }
        }
    }
}