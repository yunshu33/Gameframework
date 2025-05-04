#region Copyright
// **********************************************************************
// Copyright (C) 2022 HeJing
//
// Script Name :		HotFixDllList.cs
// Author Name :		YunShu
// Create Time :		2022/07/26 11:51:40
// Description :
// **********************************************************************
#endregion


using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Yun.Tools
{
	[CreateAssetMenu(fileName = "HotFixDllList", menuName = "CreateHotFixDllList")]
	public class HotFixDllList : ScriptableObject
	{
		[Header("热更新dll列表")]
		[SerializeField]
		public List<HotFixDll> hotFixDlls;
    }
    [System.Serializable]
    public class HotFixDll {
		public AssemblyDefinitionAsset dll;
	}

}