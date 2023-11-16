#region Copyright

// **********************************************************************
// Copyright (C) 2023 HeJing
//
// Script Name :		UIInfoScripttable.cs
// Author Name :		YunShu
// Create Time :		2023/11/13 10:43:26
// Description :
// **********************************************************************

#endregion


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GameWorldFramework.RunTime
{
    [CreateAssetMenu(menuName = nameof(GameWorldFramework) + "/" + nameof(UIInfoScriptableObject))]
    public class UIInfoScriptableObject : ScriptableObject
    {
        public List<UIInfo> m_Infos;

        public List<UIInfo> Infos => m_Infos;
        
        private void GetUIPrefabsInfo()
        {
            if (m_Infos != null)
            {
                m_Infos.Clear();
            }
            else
            {
                m_Infos = new List<UIInfo>();
            }

            var guids = AssetDatabase.FindAssets("t:prefab");

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                var info = AssetDatabase.LoadAssetAtPath<UIPanelInfo>(AssetDatabase.GUIDToAssetPath(guid));

                if (info != null)
                {
                    var bundleName = AssetDatabase.GetImplicitAssetBundleName(path);
                    
                    m_Infos.Add(new UIInfo() { path = path ,guid = guid,bundleName = bundleName});
                    
                }
            }
        }

        public void Reset()
        {
#if UNITY_EDITOR
            GetUIPrefabsInfo();
            EditorUtility.SetDirty(this);
           
#endif
        }
    }

    [Serializable]
    public class UIInfo
    {
        public string path;
        public string guid;
        public string bundleName;
    }
}