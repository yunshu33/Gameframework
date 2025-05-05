#region Copyright
// **********************************************************************
// Copyright (C) 2022 TanXun
//
// Script Name :		SetPrefab.cs
// Author Name :		YunShu
// Create Time :		2022/12/12 13:03:51
// Description :
// **********************************************************************
#endregion



using System;
using System.IO;
using Codice.Client.Common;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LJVoyage.Game.Editor
{
    public class SetPrefab : UnityEditor.Editor
    {
        [MenuItem("Assets/YunTools/Prefab/为选中的预制体添加一个子预制体")]
        [MenuItem("YunTools/Prefab/为选中的预制体添加一个子预制体")]
        static void DefaultTool()
        {

            var filters = new string[] { "预制体文件", "prefab" };

            var selectionPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);

            var selectionPrefab = PrefabUtility.LoadPrefabContents(selectionPath);

            var scene = selectionPrefab.scene;

            var childPath = EditorUtility.OpenFilePanelWithFilters("子预制体", Path.GetFileName(selectionPath), filters);

            var childPrefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(PathHelper.GetRelativePath(childPath, Application.dataPath));

            PrefabUtility.InstantiatePrefab(childPrefabAsset, selectionPrefab.transform);

            PrefabUtility.SaveAsPrefabAssetAndConnect(selectionPrefab, selectionPath, InteractionMode.UserAction);

            PrefabUtility.UnloadPrefabContents(selectionPrefab);

            EditorSceneManager.CloseScene(scene, true);

        }
        [MenuItem("Assets/YunTools/Prefab/测试api",true)]
        [MenuItem("Assets/YunTools/Prefab/为选中的预制体添加一个子预制体", true)]
        [MenuItem("YunTools/Default/为选中的预制体添加一个子预制体", true)]
        static bool CheckDefaultTool()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(Selection.activeObject))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 测试api
        /// </summary>
         [MenuItem("Assets/YunTools/Prefab/测试api")]
        static void TestApi() {

            var  path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);

            var scene = EditorSceneManager.NewPreviewScene();

            var selectionPrefab = PrefabUtility.InstantiatePrefab(Selection.activeGameObject, scene) as GameObject;

            var gs =    PrefabUtility.FindAllInstancesOfPrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path));

            selectionPrefab.GetComponent<BoxCollider>().center = new Vector3(1,2,3);

            //应用到所有 预制体实例
            PrefabUtility.ApplyPrefabInstance(selectionPrefab, InteractionMode.UserAction);

            EditorSceneManager.ClosePreviewScene(scene);

            //PrefabUtility.ApplyObjectOverride(selectionPrefab, AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]),InteractionMode.UserAction);
        }
    }


}