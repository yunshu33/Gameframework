#region Copyright
// **********************************************************************
// Copyright (C) 2023 TanXun
//
// Script Name :		LoadScene.cs
// Author Name :		Yunshu
// Create Time :		2023/04/19 08:27:38
// Description :
// **********************************************************************
#endregion

using GameWorldFramework.RunTime.Module;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameWorldFramework.RunTime.Utility
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        [AssetBundleName]
        string sceneAbPath;

        [SerializeField]
        [SceneName]
        string sceneName;
        
        public void Loaded()
        {
           // GameWorld.Instance.GetModule<AssetBundleModule>().GetAssetBundleHandle(sceneAbPath,gameObject);

            SceneManager.LoadScene(sceneName);

        }

    }
}
    

