#region Copyright
// **********************************************************************
// Copyright (C) 2023 
//
// Script Name :		GameWorldEditorConfig.cs
// Author Name :		YunShu
// Create Time :		2023/03/27 11:24:33
// Description :
// **********************************************************************
#endregion

using UnityEngine;

namespace YunFramework.Editor
{
    [CreateAssetMenu(fileName = "GameWorldEditorConfig", menuName ="GameWorld/EditorConfig")]
    public class GameWorldEditorConfig : ScriptableObject
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        [Header("命名空间")]
        public string m_namespace;

        /// <summary>
        /// 公司名称
        /// </summary>
        [Header("公司名称")]
        public string m_companyName;

        /// <summary>
        /// 作者
        /// </summary>
        [Header("作者")]
        public string m_authorName;

        /// <summary>
        /// 版本叠加
        /// </summary>
        [Header("不需要增长置0")]
        public Vector3 versionStacking;

    }
}